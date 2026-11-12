using System.Globalization;
using System.Text;
using Vendinha.Contexts;
using Vendinha.Models;
using Vendinha.Services;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var culturaBr = new CultureInfo("pt-BR");

using var context = new VendinhaDbContext();

try
{
    // garante que o banco e as tabelas existam logo na entrada pro teste funcionar direto
    context.Database.EnsureCreated();
}
catch (Exception ex)
{
    Console.WriteLine("Nao foi possivel conectar ou inicializar o banco PostgreSQL.");
    Console.WriteLine($"Detalhes: {ex.Message}");
    Console.WriteLine("Confira se o PostgreSQL esta ativo e se a variavel VENDINHA_CONNECTION_STRING (ou string padrao) esta correta.");
    return;
}

var clienteService = new ClienteService(context);
var dividaService = new DividaService(context);
var pimentaService = new PimentaService(context);
var molhoService = new MolhoService(context, dividaService);

var executando = true;

while (executando)
{
    LimparTela();
    ExibirCabecalho();
    Console.WriteLine("[1] Listar Clientes (10 por pagina / maiores devedores)");
    Console.WriteLine("[2] Cadastrar Novo Cliente");
    Console.WriteLine("[3] Atualizar ou Excluir Cliente");
    Console.WriteLine("[4] Pendurar Compra de Molho de Pimenta (Nova Divida)");
    Console.WriteLine("[5] Ver Dividas de um Cliente e Marcar Pagamento");
    Console.WriteLine("[6] Gerenciar Tipagens de Pimentas");
    Console.WriteLine("[7] Gerenciar Catalogo de Molhos do Avo Bob");
    Console.WriteLine("[8] Relatorio Gerencial de Custos e Lucro da Vendinha");
    Console.WriteLine("[0] Sair");
    Console.WriteLine("============================================================");
    Console.Write("Escolha uma opcao: ");

    var opcao = Console.ReadLine()?.Trim();

    switch (opcao)
    {
        case "1":
            ExecutarListagemClientes(clienteService, culturaBr);
            break;

        case "2":
            ExecutarCadastroCliente(clienteService);
            break;

        case "3":
            ExecutarAtualizacaoOuExclusao(clienteService, culturaBr);
            break;

        case "4":
            ExecutarCadastroDivida(clienteService, dividaService, molhoService, culturaBr);
            break;

        case "5":
            ExecutarConsultaEQuitacaoDividas(clienteService, dividaService, culturaBr);
            break;

        case "6":
            ExecutarGerenciamentoPimentas(pimentaService, culturaBr);
            break;

        case "7":
            ExecutarGerenciamentoMolhos(molhoService, pimentaService, culturaBr);
            break;

        case "8":
            ExecutarRelatorioGerencial(molhoService, culturaBr);
            break;

        case "0":
            executando = false;
            Console.WriteLine("\nAte logo! Que os molhos do Avo Bob continuem ardendo forte.");
            break;

        default:
            Console.WriteLine("\nOpcao invalida. Tente novamente.");
            Pausar();
            break;
    }
}

static void ExibirCabecalho()
{
    Console.WriteLine("============================================================");
    Console.WriteLine("       VENDINHA DE MOLHOS DE PIMENTA DO AVO BOB");
    Console.WriteLine("     caderneta de vendas fiado & producao artesanal");
    Console.WriteLine("============================================================");
}

static void ExecutarListagemClientes(ClienteService clienteService, CultureInfo culturaBr)
{
    var pagina = 1;
    const int tamanhoPagina = 10;
    string? termoBusca = null;

    while (true)
    {
        LimparTela();
        ExibirCabecalho();
        Console.WriteLine($"LISTAGEM DE CLIENTES - Pagina {pagina}");
        Console.WriteLine($"Filtro por nome: {(string.IsNullOrWhiteSpace(termoBusca) ? "(nenhum)" : termoBusca)}");
        Console.WriteLine("------------------------------------------------------------");

        try
        {
            var clientes = clienteService.ListarClientes(pagina, tamanhoPagina, termoBusca);

            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente encontrado para esta pagina/filtro.");
            }
            else
            {
                foreach (var cliente in clientes)
                {
                    Console.WriteLine(
                        $"ID: {cliente.Id,-4} | {cliente.NomeCompleto,-25} | CPF: {FormatarCpf(cliente.Cpf)} | Idade: {cliente.Idade,2} anos | Divida Aberta: {cliente.TotalDividas.ToString("C2", culturaBr)}");
                }
            }

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("[P] Proxima pagina | [A] Pagina anterior | [F] Filtrar nome | [L] Limpar filtro | [V] Voltar");
            Console.Write("Opcao: ");
            var acao = Console.ReadLine()?.Trim().ToUpperInvariant();

            switch (acao)
            {
                case "P":
                    if (clientes.Count == tamanhoPagina)
                    {
                        pagina++;
                    }
                    else
                    {
                        Console.WriteLine("Voce ja esta na ultima pagina.");
                        Pausar();
                    }
                    break;

                case "A":
                    if (pagina > 1)
                    {
                        pagina--;
                    }
                    else
                    {
                        Console.WriteLine("Voce ja esta na primeira pagina.");
                        Pausar();
                    }
                    break;

                case "F":
                    Console.Write("Digite parte do nome para buscar: ");
                    termoBusca = Console.ReadLine()?.Trim();
                    pagina = 1;
                    break;

                case "L":
                    termoBusca = null;
                    pagina = 1;
                    break;

                case "V":
                case "0":
                    return;

                default:
                    Console.WriteLine("Comando nao reconhecido.");
                    Pausar();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro ao listar clientes: {ex.Message}");
            Pausar();
            return;
        }
    }
}

static void ExecutarCadastroCliente(ClienteService clienteService)
{
    LimparTela();
    ExibirCabecalho();
    Console.WriteLine("CADASTRAR NOVO CLIENTE");
    Console.WriteLine("------------------------------------------------------------");

    try
    {
        Console.Write("Nome completo: ");
        var nome = Console.ReadLine() ?? string.Empty;

        Console.Write("CPF (apenas numeros ou formatado): ");
        var cpf = Console.ReadLine() ?? string.Empty;

        Console.Write("Data de nascimento (dd/MM/aaaa): ");
        var dataTexto = Console.ReadLine();

        if (!TryParseData(dataTexto, out var dataNascimento))
        {
            throw new ArgumentException("Data de nascimento invalida. Use o formato dd/MM/aaaa.");
        }

        Console.Write("E-mail (opcional - pressione ENTER para pular): ");
        var email = Console.ReadLine();

        var novoCliente = new Cliente
        {
            NomeCompleto = nome,
            Cpf = cpf,
            DataNascimento = dataNascimento,
            Email = string.IsNullOrWhiteSpace(email) ? null : email
        };

        var cadastrado = clienteService.CadastrarCliente(novoCliente);

        Console.WriteLine("\nCliente cadastrado com sucesso!");
        Console.WriteLine($"ID: {cadastrado.Id} | Nome: {cadastrado.NomeCompleto} | CPF: {FormatarCpf(cadastrado.Cpf)} | Idade: {cadastrado.Idade} anos");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nErro ao cadastrar cliente: {ex.Message}");
    }

    Pausar();
}

static void ExecutarAtualizacaoOuExclusao(ClienteService clienteService, CultureInfo culturaBr)
{
    LimparTela();
    ExibirCabecalho();
    Console.WriteLine("ATUALIZAR OU EXCLUIR CLIENTE");
    Console.WriteLine("------------------------------------------------------------");

    try
    {
        Console.Write("Informe o ID ou CPF do cliente: ");
        var identificador = Console.ReadLine();
        var cliente = BuscarClientePorIdOuCpf(clienteService, identificador);

        if (cliente is null)
        {
            Console.WriteLine("\nCliente nao encontrado.");
            Pausar();
            return;
        }

        Console.WriteLine($"\nCliente selecionado: [{cliente.Id}] {cliente.NomeCompleto}");
        Console.WriteLine($"CPF: {FormatarCpf(cliente.Cpf)} | Nascimento: {cliente.DataNascimento:dd/MM/yyyy} ({cliente.Idade} anos)");
        Console.WriteLine($"E-mail: {cliente.Email ?? "(nao informado)"} | Divida em aberto: {cliente.TotalDividas.ToString("C2", culturaBr)}");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine("[1] Atualizar dados");
        Console.WriteLine("[2] Excluir cliente");
        Console.WriteLine("[0] Voltar");
        Console.Write("Escolha: ");
        var subOpcao = Console.ReadLine()?.Trim();

        switch (subOpcao)
        {
            case "1":
                Console.Write($"Nome completo [{cliente.NomeCompleto}] (ENTER mantem): ");
                var novoNome = Console.ReadLine();

                Console.Write($"CPF [{FormatarCpf(cliente.Cpf)}] (ENTER mantem): ");
                var novoCpf = Console.ReadLine();

                Console.Write($"Data de nascimento [{cliente.DataNascimento:dd/MM/yyyy}] (ENTER mantem): ");
                var novaDataTexto = Console.ReadLine();

                var novaData = cliente.DataNascimento;
                if (!string.IsNullOrWhiteSpace(novaDataTexto) && !TryParseData(novaDataTexto, out novaData))
                {
                    throw new ArgumentException("Data de nascimento invalida. Use o formato dd/MM/aaaa.");
                }

                Console.Write($"E-mail [{cliente.Email ?? "nenhum"}] (ENTER mantem, '-' limpa): ");
                var novoEmail = Console.ReadLine();

                string? emailFinal;
                if (string.IsNullOrWhiteSpace(novoEmail))
                {
                    emailFinal = cliente.Email;
                }
                else if (novoEmail.Trim() == "-")
                {
                    emailFinal = null;
                }
                else
                {
                    emailFinal = novoEmail;
                }

                var atualizado = clienteService.AtualizarCliente(new Cliente
                {
                    Id = cliente.Id,
                    NomeCompleto = string.IsNullOrWhiteSpace(novoNome) ? cliente.NomeCompleto : novoNome,
                    Cpf = string.IsNullOrWhiteSpace(novoCpf) ? cliente.Cpf : novoCpf,
                    DataNascimento = novaData,
                    Email = emailFinal
                });

                Console.WriteLine($"\nCliente '{atualizado.NomeCompleto}' atualizado com sucesso!");
                break;

            case "2":
                if (cliente.Dividas.Count > 0)
                {
                    Console.WriteLine($"\nAtencao: este cliente possui {cliente.Dividas.Count} registro(s) de divida (total em aberto: {cliente.TotalDividas.ToString("C2", culturaBr)}). Tudo sera removido em cascata.");
                }

                Console.Write("Confirma a exclusao definitiva? (S/N): ");
                var confirmacao = Console.ReadLine()?.Trim().ToUpperInvariant();

                if (confirmacao is "S" or "SIM")
                {
                    clienteService.ExcluirCliente(cliente.Id);
                    Console.WriteLine("\nCliente excluido com sucesso.");
                }
                else
                {
                    Console.WriteLine("\nExclusao cancelada.");
                }
                break;

            case "0":
                return;

            default:
                Console.WriteLine("\nOpcao invalida.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nErro na operacao: {ex.Message}");
    }

    Pausar();
}

static void ExecutarCadastroDivida(
    ClienteService clienteService,
    DividaService dividaService,
    MolhoService molhoService,
    CultureInfo culturaBr)
{
    LimparTela();
    ExibirCabecalho();
    Console.WriteLine("PENDURAR COMPRA DE MOLHO DE PIMENTA");
    Console.WriteLine("------------------------------------------------------------");

    try
    {
        Console.Write("Informe o ID ou CPF do cliente: ");
        var identificador = Console.ReadLine();
        var cliente = BuscarClientePorIdOuCpf(clienteService, identificador);

        if (cliente is null)
        {
            Console.WriteLine("\nCliente nao encontrado.");
            Pausar();
            return;
        }

        Console.WriteLine($"Cliente: {cliente.NomeCompleto} (CPF: {FormatarCpf(cliente.Cpf)})");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine("[1] Escolher molho do catalogo (calcula valor automatico)");
        Console.WriteLine("[2] Lancamento manual (descricao e valor livres)");
        Console.WriteLine("[0] Cancelar");
        Console.Write("Escolha o modo: ");
        var modo = Console.ReadLine()?.Trim();

        Divida novaDivida;

        switch (modo)
        {
            case "1":
                var catalogo = molhoService.ListarCatalogo();
                if (catalogo.Count == 0)
                {
                    Console.WriteLine("\nNenhum molho cadastrado no catalogo ainda. Cadastre um na opcao [7] ou use o lancamento manual.");
                    Pausar();
                    return;
                }

                Console.WriteLine("\nMolhos disponiveis:");
                foreach (var molho in catalogo)
                {
                    var tipo = molho is MolhoEspecialReserva reserva
                        ? $"Reserva ({reserva.TempoMaturacaoDias}d)"
                        : "Tradicional";
                    var shu = molho.PimentaBase?.NivelScoville ?? 0;

                    Console.WriteLine(
                        $"ID: {molho.Id,-3} | {molho.Nome,-28} | {tipo,-15} | {shu,9:N0} SHU | Preco: {molho.PrecoVenda.ToString("C2", culturaBr)}");
                }

                Console.Write("\nInforme o ID do molho escolhido: ");
                if (!int.TryParse(Console.ReadLine()?.Trim(), out var molhoId))
                {
                    throw new ArgumentException("ID do molho invalido.");
                }

                Console.Write("Quantidade de frascos: ");
                if (!int.TryParse(Console.ReadLine()?.Trim(), out var quantidadeFrascos) || quantidadeFrascos <= 0)
                {
                    throw new ArgumentException("Informe uma quantidade inteira de frascos maior que zero.");
                }

                Console.Write("Observacao opcional (ENTER usa o nome do molho automaticamente): ");
                var observacao = Console.ReadLine();

                novaDivida = dividaService.CadastrarDividaPorMolho(cliente.Id, molhoId, quantidadeFrascos, observacao);
                break;

            case "2":
                Console.Write("Quais molhos do Avo Bob o cliente pegou? (ENTER para descricao padrao): ");
                var descricao = Console.ReadLine();

                Console.Write("Valor da compra (R$): ");
                var valorTexto = Console.ReadLine();

                if (!TryParseDecimal(valorTexto, culturaBr, out var valor))
                {
                    throw new ArgumentException("Valor invalido. Informe um numero positivo (ex: 29,90).");
                }

                novaDivida = dividaService.CadastrarDivida(cliente.Id, valor, descricao);
                break;

            case "0":
                return;

            default:
                Console.WriteLine("\nOpcao invalida.");
                Pausar();
                return;
        }

        Console.WriteLine("\nCompra pendurada na caderneta com sucesso!");
        Console.WriteLine(
            $"Divida ID: {novaDivida.Id} | Qtd: {novaDivida.QuantidadeFrascos} frasco(s) | Molhos: {novaDivida.DescricaoMolhos} | Valor: {novaDivida.Valor.ToString("C2", culturaBr)} | Data: {novaDivida.DataCriacao:dd/MM/yyyy HH:mm}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nNao foi possivel pendurar a compra: {ex.Message}");
    }

    Pausar();
}

static void ExecutarConsultaEQuitacaoDividas(ClienteService clienteService, DividaService dividaService, CultureInfo culturaBr)
{
    LimparTela();
    ExibirCabecalho();
    Console.WriteLine("VER DIVIDAS E MARCAR PAGAMENTO");
    Console.WriteLine("------------------------------------------------------------");

    try
    {
        Console.Write("Informe o ID ou CPF do cliente: ");
        var identificador = Console.ReadLine();
        var cliente = BuscarClientePorIdOuCpf(clienteService, identificador);

        if (cliente is null)
        {
            Console.WriteLine("\nCliente nao encontrado.");
            Pausar();
            return;
        }

        Console.WriteLine($"\nCliente: {cliente.NomeCompleto} | Idade: {cliente.Idade} anos | Em aberto: {cliente.TotalDividas.ToString("C2", culturaBr)}");
        Console.WriteLine("------------------------------------------------------------");

        var dividas = dividaService.ListarDividasPorCliente(cliente.Id);

        if (dividas.Count == 0)
        {
            Console.WriteLine("Este cliente ainda nao possui dividas registradas.");
            Pausar();
            return;
        }

        foreach (var divida in dividas)
        {
            var situacao = divida.SituacaoPaga ? "PAGA" : "EM ABERTO";
            var dataPagto = divida.DataPagamento.HasValue
                ? divida.DataPagamento.Value.ToString("dd/MM/yyyy HH:mm")
                : "-";

            Console.WriteLine(
                $"ID: {divida.Id,-4} | {situacao,-9} | {divida.Valor.ToString("C2", culturaBr),-10} | Qtd: {divida.QuantidadeFrascos,-2} | Compra: {divida.DataCriacao:dd/MM/yyyy HH:mm} | Pagto: {dataPagto}");
            Console.WriteLine($"         Molhos: {divida.DescricaoMolhos}");
        }

        var dividaEmAberto = dividas.FirstOrDefault(d => !d.SituacaoPaga);

        if (dividaEmAberto is not null)
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.Write($"Deseja marcar a divida em aberto ID {dividaEmAberto.Id} ({dividaEmAberto.Valor.ToString("C2", culturaBr)}) como PAGA? (S/N): ");
            var confirmar = Console.ReadLine()?.Trim().ToUpperInvariant();

            if (confirmar is "S" or "SIM")
            {
                var quitada = dividaService.QuitarDivida(dividaEmAberto.Id);
                Console.WriteLine($"\nConta acertada! Divida ID {quitada.Id} quitada em {quitada.DataPagamento:dd/MM/yyyy HH:mm}.");
            }
            else
            {
                Console.WriteLine("\nPagamento nao realizado. A conta continua em aberto.");
            }
        }
        else
        {
            Console.WriteLine("\nTodas as dividas deste cliente ja estao quitadas!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nErro ao consultar/quitar divida: {ex.Message}");
    }

    Pausar();
}

static void ExecutarGerenciamentoPimentas(PimentaService pimentaService, CultureInfo culturaBr)
{
    while (true)
    {
        LimparTela();
        ExibirCabecalho();
        Console.WriteLine("GERENCIAR TIPAGENS DE PIMENTAS");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine("[1] Listar pimentas cadastradas");
        Console.WriteLine("[2] Cadastrar nova pimenta");
        Console.WriteLine("[3] Atualizar pimenta");
        Console.WriteLine("[4] Remover pimenta");
        Console.WriteLine("[0] Voltar ao menu principal");
        Console.Write("Escolha: ");
        var opcao = Console.ReadLine()?.Trim();

        try
        {
            switch (opcao)
            {
                case "1":
                    var pimentas = pimentaService.ListarPimentas();
                    Console.WriteLine("\n--- PIMENTAS DO AVO BOB ---");
                    if (pimentas.Count == 0)
                    {
                        Console.WriteLine("Nenhuma pimenta cadastrada ainda.");
                    }
                    else
                    {
                        foreach (var p in pimentas)
                        {
                            Console.WriteLine(
                                $"ID: {p.Id,-3} | {p.Nome,-20} | Especie: {p.EspecieOrigem,-22} | {p.NivelScoville,9:N0} SHU | Custo/g: {p.CustoPorGrama.ToString("C4", culturaBr)}");
                        }
                    }
                    Pausar();
                    break;

                case "2":
                    Console.Write("\nNome da pimenta (ex: Carolina Reaper): ");
                    var nome = Console.ReadLine() ?? string.Empty;

                    Console.Write("Especie de origem (ex: Capsicum chinense): ");
                    var especie = Console.ReadLine() ?? string.Empty;

                    Console.Write("Nivel Scoville (SHU - numero inteiro positivo): ");
                    if (!int.TryParse(Console.ReadLine()?.Trim(), out var scoville))
                    {
                        throw new ArgumentException("Nivel Scoville invalido. Informe um numero inteiro positivo.");
                    }

                    Console.Write("Custo por grama (R$/g - ex: 0,15): ");
                    if (!TryParseDecimal(Console.ReadLine(), culturaBr, out var custoPorGrama))
                    {
                        throw new ArgumentException("Custo por grama invalido.");
                    }

                    var criada = pimentaService.CadastrarPimenta(new Pimenta
                    {
                        Nome = nome,
                        EspecieOrigem = especie,
                        NivelScoville = scoville,
                        CustoPorGrama = custoPorGrama
                    });

                    Console.WriteLine($"\nPimenta '{criada.Nome}' (ID {criada.Id}) cadastrada com sucesso!");
                    Pausar();
                    break;

                case "3":
                    Console.Write("\nInforme o ID da pimenta para atualizar: ");
                    if (!int.TryParse(Console.ReadLine()?.Trim(), out var idAtualizar))
                    {
                        throw new ArgumentException("ID invalido.");
                    }

                    var atual = pimentaService.BuscarPorId(idAtualizar);
                    if (atual is null)
                    {
                        Console.WriteLine("Pimenta nao encontrada.");
                        Pausar();
                        break;
                    }

                    Console.Write($"Nome [{atual.Nome}] (ENTER mantem): ");
                    var novoNome = Console.ReadLine();

                    Console.Write($"Especie [{atual.EspecieOrigem}] (ENTER mantem): ");
                    var novaEspecie = Console.ReadLine();

                    Console.Write($"Nivel Scoville [{atual.NivelScoville}] (ENTER mantem): ");
                    var novoScovilleTexto = Console.ReadLine();
                    var novoScoville = atual.NivelScoville;
                    if (!string.IsNullOrWhiteSpace(novoScovilleTexto) &&
                        !int.TryParse(novoScovilleTexto.Trim(), out novoScoville))
                    {
                        throw new ArgumentException("Nivel Scoville invalido.");
                    }

                    Console.Write($"Custo/g [{atual.CustoPorGrama.ToString("N4", culturaBr)}] (ENTER mantem): ");
                    var novoCustoTexto = Console.ReadLine();
                    var novoCusto = atual.CustoPorGrama;
                    if (!string.IsNullOrWhiteSpace(novoCustoTexto) &&
                        !TryParseDecimal(novoCustoTexto, culturaBr, out novoCusto))
                    {
                        throw new ArgumentException("Custo por grama invalido.");
                    }

                    var atualizada = pimentaService.AtualizarPimenta(new Pimenta
                    {
                        Id = atual.Id,
                        Nome = string.IsNullOrWhiteSpace(novoNome) ? atual.Nome : novoNome,
                        EspecieOrigem = string.IsNullOrWhiteSpace(novaEspecie) ? atual.EspecieOrigem : novaEspecie,
                        NivelScoville = novoScoville,
                        CustoPorGrama = novoCusto
                    });

                    Console.WriteLine($"\nPimenta '{atualizada.Nome}' atualizada!");
                    Pausar();
                    break;

                case "4":
                    Console.Write("\nInforme o ID da pimenta para remover: ");
                    if (!int.TryParse(Console.ReadLine()?.Trim(), out var idRemover))
                    {
                        throw new ArgumentException("ID invalido.");
                    }

                    pimentaService.RemoverPimenta(idRemover);
                    Console.WriteLine("Pimenta removida com sucesso!");
                    Pausar();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Opcao invalida.");
                    Pausar();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro no gerenciamento de pimentas: {ex.Message}");
            Pausar();
        }
    }
}

static void ExecutarGerenciamentoMolhos(
    MolhoService molhoService,
    PimentaService pimentaService,
    CultureInfo culturaBr)
{
    while (true)
    {
        LimparTela();
        ExibirCabecalho();
        Console.WriteLine("CATALOGO DE MOLHOS DO AVO BOB");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine("[1] Listar catalogo (ardencia, custo, preco e margem)");
        Console.WriteLine("[2] Criar receita de Molho Tradicional");
        Console.WriteLine("[3] Criar receita de Molho Especial Reserva (barril)");
        Console.WriteLine("[0] Voltar ao menu principal");
        Console.Write("Escolha: ");
        var opcao = Console.ReadLine()?.Trim();

        try
        {
            switch (opcao)
            {
                case "1":
                    var catalogo = molhoService.ListarCatalogo();
                    Console.WriteLine("\n--- CATALOGO ARTESANAL ---");
                    if (catalogo.Count == 0)
                    {
                        Console.WriteLine("Nenhum molho cadastrado no catalogo ainda.");
                    }
                    else
                    {
                        foreach (var molho in catalogo)
                        {
                            var tipo = molho is MolhoEspecialReserva reserva
                                ? $"Especial Reserva ({reserva.TempoMaturacaoDias}d barril)"
                                : "Tradicional";
                            var nomePimenta = molho.PimentaBase?.Nome ?? "Desconhecida";
                            var scoville = molho.PimentaBase?.NivelScoville ?? 0;

                            Console.WriteLine(
                                $"ID: {molho.Id,-3} | {molho.Nome} [{tipo}]");
                            Console.WriteLine(
                                $"         Pimenta: {nomePimenta} ({scoville:N0} SHU) | Custo: {molho.CustoTotalProducao.ToString("C2", culturaBr)} | Venda: {molho.PrecoVenda.ToString("C2", culturaBr)} | Lucro: {molho.LucroUnitario.ToString("C2", culturaBr)} ({molho.MargemLucroPercentual:N1}%)");
                        }
                    }
                    Pausar();
                    break;

                case "2":
                case "3":
                    var ehReserva = opcao == "3";
                    var pimentas = pimentaService.ListarPimentas();
                    if (pimentas.Count == 0)
                    {
                        Console.WriteLine("\nCadastre ao menos uma pimenta na opcao [6] antes de criar um molho.");
                        Pausar();
                        break;
                    }

                    Console.WriteLine("\nPimentas disponiveis para a receita:");
                    foreach (var p in pimentas)
                    {
                        Console.WriteLine($"  ID: {p.Id,-3} | {p.Nome} ({p.NivelScoville:N0} SHU - {p.CustoPorGrama.ToString("C4", culturaBr)}/g)");
                    }

                    Console.Write("\nNome do molho: ");
                    var nomeMolho = Console.ReadLine() ?? string.Empty;

                    Console.Write("ID da pimenta base: ");
                    if (!int.TryParse(Console.ReadLine()?.Trim(), out var pimentaId))
                    {
                        throw new ArgumentException("ID da pimenta invalido.");
                    }

                    Console.Write("Gramas de pimenta por frasco: ");
                    if (!TryParseDecimal(Console.ReadLine(), culturaBr, out var gramas))
                    {
                        throw new ArgumentException("Quantidade em gramas invalida.");
                    }

                    Console.Write("Custo de embalagem e ingredientes (vidro, rotulo, vinagre) (R$): ");
                    if (!TryParseDecimal(Console.ReadLine(), culturaBr, out var custoEmbalagem))
                    {
                        throw new ArgumentException("Custo de embalagem/ingredientes invalido.");
                    }

                    Console.Write("Preco de venda por frasco (R$): ");
                    if (!TryParseDecimal(Console.ReadLine(), culturaBr, out var precoVenda))
                    {
                        throw new ArgumentException("Preco de venda invalido.");
                    }

                    Molho novoMolho;
                    if (ehReserva)
                    {
                        Console.Write("Tempo de maturacao em barril (dias): ");
                        if (!int.TryParse(Console.ReadLine()?.Trim(), out var diasMaturacao))
                        {
                            throw new ArgumentException("Tempo de maturacao invalido.");
                        }

                        Console.Write("Custo adicional do barril por frasco (R$): ");
                        if (!TryParseDecimal(Console.ReadLine(), culturaBr, out var custoBarril))
                        {
                            throw new ArgumentException("Custo adicional do barril invalido.");
                        }

                        novoMolho = new MolhoEspecialReserva
                        {
                            Nome = nomeMolho,
                            PimentaId = pimentaId,
                            GramasPimentaPorFrasco = gramas,
                            CustoEmbalagemIngredientes = custoEmbalagem,
                            PrecoVenda = precoVenda,
                            TempoMaturacaoDias = diasMaturacao,
                            CustoAdicionalBarril = custoBarril
                        };
                    }
                    else
                    {
                        novoMolho = new Molho
                        {
                            Nome = nomeMolho,
                            PimentaId = pimentaId,
                            GramasPimentaPorFrasco = gramas,
                            CustoEmbalagemIngredientes = custoEmbalagem,
                            PrecoVenda = precoVenda
                        };
                    }

                    var salvo = molhoService.CadastrarMolho(novoMolho);
                    Console.WriteLine($"\nMolho '{salvo.Nome}' (ID {salvo.Id}) cadastrado com sucesso!");
                    Console.WriteLine(
                        $"Custo total de producao: {salvo.CustoTotalProducao.ToString("C2", culturaBr)} | Preco venda: {salvo.PrecoVenda.ToString("C2", culturaBr)} | Lucro unitario: {salvo.LucroUnitario.ToString("C2", culturaBr)} ({salvo.MargemLucroPercentual:N1}%)");
                    Pausar();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Opcao invalida.");
                    Pausar();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro no catalogo de molhos: {ex.Message}");
            Pausar();
        }
    }
}

static void ExecutarRelatorioGerencial(MolhoService molhoService, CultureInfo culturaBr)
{
    LimparTela();
    ExibirCabecalho();
    Console.WriteLine("RELATORIO GERENCIAL DE CUSTOS E LUCRO DA VENDINHA");
    Console.WriteLine("------------------------------------------------------------");

    try
    {
        var rel = molhoService.GerarRelatorioGerencial();

        Console.WriteLine($"Catalogo de Molhos       : {rel.TotalMolhosCatalogo} receita(s) ({rel.TotalTradicionais} tradicional(is), {rel.TotalEspeciaisReserva} reserva(s) especial(is))");
        Console.WriteLine($"Custo Medio por Frasco   : {rel.MediaCustoPorFrasco.ToString("C2", culturaBr)}");
        Console.WriteLine($"Preco Medio de Venda     : {rel.MediaPrecoVenda.ToString("C2", culturaBr)}");
        Console.WriteLine($"Lucro Unitario Medio     : {rel.MediaLucroUnitario.ToString("C2", culturaBr)}");
        Console.WriteLine($"Margem Media de Lucro    : {rel.MargemMediaPercentual:N2}%");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine($"A Receber (Em Aberto)    : {rel.TotalAReceberDividasAbertas.ToString("C2", culturaBr)} ({rel.QuantidadeDividasAbertas} conta(s))");
        Console.WriteLine($"Ja Recebido (Quitadas)   : {rel.TotalJaRecebidoDividasPagas.ToString("C2", culturaBr)} ({rel.QuantidadeDividasPagas} conta(s))");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nErro ao gerar relatorio: {ex.Message}");
    }

    Pausar();
}

static Cliente? BuscarClientePorIdOuCpf(ClienteService clienteService, string? entrada)
{
    if (string.IsNullOrWhiteSpace(entrada))
    {
        return null;
    }

    var texto = entrada.Trim();
    var somenteDigitos = new string(texto.Where(char.IsDigit).ToArray());

    // checa primeiro se parece um cpf completo pra nao confundir cpf que comeca com zero com id numerico
    if (somenteDigitos.Length == 11)
    {
        return clienteService.BuscarPorCpf(texto);
    }

    if (int.TryParse(texto, out var id))
    {
        return clienteService.BuscarPorId(id);
    }

    return clienteService.BuscarPorCpf(texto);
}

static bool TryParseData(string? texto, out DateOnly data)
{
    data = default;
    if (string.IsNullOrWhiteSpace(texto))
    {
        return false;
    }

    string[] formatos = ["dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd"];
    return DateOnly.TryParseExact(texto.Trim(), formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out data);
}

static bool TryParseDecimal(string? texto, CultureInfo culturaBr, out decimal valor)
{
    valor = 0m;
    if (string.IsNullOrWhiteSpace(texto))
    {
        return false;
    }

    var limpo = texto.Trim().Replace("R$", "", StringComparison.OrdinalIgnoreCase).Trim();

    // aceita tanto padrao br (25,50) quanto ponto decimal (25.50)
    if (limpo.Contains(','))
    {
        return decimal.TryParse(limpo, NumberStyles.Number, culturaBr, out valor);
    }

    return decimal.TryParse(limpo, NumberStyles.Number, CultureInfo.InvariantCulture, out valor);
}

static string FormatarCpf(string cpf)
{
    var digitos = new string(cpf.Where(char.IsDigit).ToArray());
    if (digitos.Length != 11)
    {
        return cpf;
    }

    return $"{digitos[..3]}.{digitos[3..6]}.{digitos[6..9]}-{digitos[9..]}";
}

static void LimparTela()
{
    // evita exception quando o terminal estiver com saida redirecionada
    if (!Console.IsOutputRedirected)
    {
        Console.Clear();
    }
}

static void Pausar()
{
    Console.WriteLine();
    Console.Write("Pressione ENTER para continuar...");
    Console.ReadLine();
}
