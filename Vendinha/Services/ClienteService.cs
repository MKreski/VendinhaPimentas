using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Vendinha.Contexts;
using Vendinha.Models;

namespace Vendinha.Services;

// servico com as regras de negocio e crud dos clientes da vendinha
public class ClienteService
{
    private readonly VendinhaDbContext _context;

    public ClienteService(VendinhaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // cadastra um cliente novo depois de rodar todas as validacoes
    public Cliente CadastrarCliente(Cliente cliente)
    {
        ArgumentNullException.ThrowIfNull(cliente);

        ValidarENormalizarCliente(cliente);

        // confere no banco se esse cpf ja pertence a alguem
        var cpfDuplicado = _context.Clientes.Any(c =>
            c.Cpf == cliente.Cpf ||
            c.Cpf.Replace(".", "").Replace("-", "").Trim() == cliente.Cpf);

        if (cpfDuplicado)
        {
            throw new InvalidOperationException("Ja existe um cliente cadastrado com este CPF.");
        }

        _context.Clientes.Add(cliente);
        _context.SaveChanges();

        return cliente;
    }

    // busca um cliente pelo id ja trazendo as dividas pra idade e total funcionarem certinho
    public Cliente? BuscarPorId(int id)
    {
        return _context.Clientes
            .Include(c => c.Dividas)
            .FirstOrDefault(c => c.Id == id);
    }

    // busca um cliente pelo cpf (com ou sem mascara) trazendo as dividas junto
    public Cliente? BuscarPorCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            return null;
        }

        var cpfLimpo = LimparCpf(cpf);
        if (cpfLimpo.Length != 11)
        {
            return null;
        }

        return _context.Clientes
            .Include(c => c.Dividas)
            .FirstOrDefault(c =>
                c.Cpf == cpfLimpo ||
                c.Cpf.Replace(".", "").Replace("-", "").Trim() == cpfLimpo);
    }

    // lista os clientes paginados, filtrando por nome e ordenando do maior devedor pro menor
    public List<Cliente> ListarClientes(int pagina = 1, int tamanhoPagina = 10, string? termoBuscaNome = null)
    {
        if (pagina < 1)
        {
            pagina = 1;
        }

        if (tamanhoPagina < 1)
        {
            tamanhoPagina = 10;
        }

        IQueryable<Cliente> query = _context.Clientes
            .AsNoTracking()
            .Include(c => c.Dividas);

        // aplica filtro case-insensitive no nome caso tenha termo de busca
        if (!string.IsNullOrWhiteSpace(termoBuscaNome))
        {
            var termo = termoBuscaNome.Trim().ToLower();
            query = query.Where(c => c.NomeCompleto.ToLower().Contains(termo));
        }

        // ordena no banco de quem deve mais em aberto pra quem deve menos
        query = query
            .OrderByDescending(c => c.Dividas
                .Where(d => !d.SituacaoPaga)
                .Sum(d => d.Valor))
            .ThenBy(c => c.NomeCompleto);

        return query
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToList();
    }

    // atualiza os dados do cliente mantendo as mesmas validacoes do cadastro
    public Cliente AtualizarCliente(Cliente cliente)
    {
        ArgumentNullException.ThrowIfNull(cliente);

        var existente = _context.Clientes
            .Include(c => c.Dividas)
            .FirstOrDefault(c => c.Id == cliente.Id);

        if (existente is null)
        {
            throw new KeyNotFoundException($"Cliente com id {cliente.Id} nao encontrado.");
        }

        ValidarENormalizarCliente(cliente);

        // ignora o proprio registro na checagem de cpf duplicado
        var cpfDuplicado = _context.Clientes.Any(c =>
            c.Id != cliente.Id &&
            (c.Cpf == cliente.Cpf ||
             c.Cpf.Replace(".", "").Replace("-", "").Trim() == cliente.Cpf));

        if (cpfDuplicado)
        {
            throw new InvalidOperationException("Ja existe outro cliente cadastrado com este CPF.");
        }

        existente.NomeCompleto = cliente.NomeCompleto;
        existente.Cpf = cliente.Cpf;
        existente.DataNascimento = cliente.DataNascimento;
        existente.Email = cliente.Email;

        _context.SaveChanges();

        return existente;
    }

    // remove o cliente do banco (as dividas somem junto por causa do cascade)
    public void ExcluirCliente(int id)
    {
        var existente = _context.Clientes.FirstOrDefault(c => c.Id == id);

        if (existente is null)
        {
            throw new KeyNotFoundException($"Cliente com id {id} nao encontrado.");
        }

        _context.Clientes.Remove(existente);
        _context.SaveChanges();
    }

    // valida campos obrigatorios, cpf, data de nascimento e email, e ja limpa espacos
    private static void ValidarENormalizarCliente(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.NomeCompleto))
        {
            throw new ArgumentException("O nome completo do cliente e obrigatorio.", nameof(cliente));
        }

        cliente.NomeCompleto = cliente.NomeCompleto.Trim();

        if (cliente.NomeCompleto.Length > 150)
        {
            throw new ArgumentException("O nome completo pode ter no maximo 150 caracteres.", nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(cliente.Cpf))
        {
            throw new ArgumentException("O CPF do cliente e obrigatorio.", nameof(cliente));
        }

        if (!ValidarCpf(cliente.Cpf))
        {
            throw new ArgumentException("O CPF informado e invalido.", nameof(cliente));
        }

        // salva sempre so os 11 digitos pra evitar duplicidade por causa de mascara
        cliente.Cpf = LimparCpf(cliente.Cpf);

        var hoje = DateOnly.FromDateTime(DateTime.Today);
        if (cliente.DataNascimento == default || cliente.DataNascimento > hoje)
        {
            throw new ArgumentException("A data de nascimento e obrigatoria e nao pode ser futura.", nameof(cliente));
        }

        if (!string.IsNullOrWhiteSpace(cliente.Email))
        {
            var emailLimpo = cliente.Email.Trim();

            if (emailLimpo.Length > 150)
            {
                throw new ArgumentException("O e-mail pode ter no maximo 150 caracteres.", nameof(cliente));
            }

            if (!ValidarEmail(emailLimpo))
            {
                throw new ArgumentException("O e-mail informado nao possui um formato valido.", nameof(cliente));
            }

            cliente.Email = emailLimpo;
        }
        else
        {
            cliente.Email = null;
        }
    }

    // algoritmo oficial da receita federal pra validar os digitos verificadores do cpf
    public static bool ValidarCpf(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            return false;
        }

        var digitos = LimparCpf(cpf);

        if (digitos.Length != 11)
        {
            return false;
        }

        // barra cpfs com todos os numeros iguais tipo 11111111111
        if (digitos.Distinct().Count() == 1)
        {
            return false;
        }

        // calcula o primeiro digito verificador
        var soma = 0;
        for (var i = 0; i < 9; i++)
        {
            soma += (digitos[i] - '0') * (10 - i);
        }

        var resto = soma % 11;
        var primeiroDigito = resto < 2 ? 0 : 11 - resto;

        if ((digitos[9] - '0') != primeiroDigito)
        {
            return false;
        }

        // calcula o segundo digito verificador
        soma = 0;
        for (var i = 0; i < 10; i++)
        {
            soma += (digitos[i] - '0') * (11 - i);
        }

        resto = soma % 11;
        var segundoDigito = resto < 2 ? 0 : 11 - resto;

        return (digitos[10] - '0') == segundoDigito;
    }

    // tira pontos, tracos e qualquer caractere que nao seja numero
    private static string LimparCpf(string cpf)
    {
        return new string(cpf.Where(char.IsDigit).ToArray());
    }

    // confere formato basico usuario@dominio.tld usando regex e mailaddress
    private static bool ValidarEmail(string email)
    {
        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            return false;
        }

        return MailAddress.TryCreate(email, out var address) && address.Address == email;
    }
}
