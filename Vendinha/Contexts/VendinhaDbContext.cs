using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vendinha.Models;

namespace Vendinha.Contexts;

// contexto principal do ef core pra acessar as tabelas da vendinha
public class VendinhaDbContext : DbContext
{
    // construtor vazio pro uso direto no console e ferramentas de migration
    public VendinhaDbContext()
    {
    }

    // construtor com options caso queira injetar configuracao externa no futuro
    public VendinhaDbContext(DbContextOptions<VendinhaDbContext> options) : base(options)
    {
    }

    // tabelas mapeadas no banco
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Divida> Dividas => Set<Divida>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // so aplica a config padrao se nada tiver sido passado antes no construtor
        if (!optionsBuilder.IsConfigured)
        {
            // tenta pegar da variavel de ambiente primeiro, senao usa o postgres local padrao
            var connectionString = Environment.GetEnvironmentVariable("VENDINHA_CONNECTION_STRING")
                ?? "Host=localhost;Port=5432;Database=vendinha_avo_bob;Username=postgres;Password=postgres";

            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Clientes");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.NomeCompleto)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(c => c.Cpf)
                .IsRequired()
                .HasMaxLength(14);

            // garante que nao vai entrar cpf repetido no banco
            entity.HasIndex(c => c.Cpf)
                .IsUnique();

            entity.Property(c => c.DataNascimento)
                .IsRequired();

            entity.Property(c => c.Email)
                .HasMaxLength(150);

            // propriedades calculadas em memoria, nao viram coluna no postgres
            entity.Ignore(c => c.Idade);
            entity.Ignore(c => c.TotalDividas);

            // se apagar o cliente, apaga junto as dividas dele em cascata
            entity.HasMany(c => c.Dividas)
                .WithOne(d => d.Cliente)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Divida>(entity =>
        {
            // trava no banco pra nunca aceitar divida zerada ou negativa
            entity.ToTable("Dividas", t =>
                t.HasCheckConstraint("CK_Dividas_Valor_Positivo", "\"Valor\" > 0"));

            entity.HasKey(d => d.Id);

            entity.Property(d => d.DescricaoMolhos)
                .IsRequired()
                .HasMaxLength(300)
                .HasDefaultValue("Molhos de pimenta artesanais do Avô Bob");

            entity.Property(d => d.Valor)
                .IsRequired()
                .HasPrecision(10, 2);

            entity.Property(d => d.SituacaoPaga)
                .IsRequired()
                .HasDefaultValue(false);

            entity.Property(d => d.DataCriacao)
                .IsRequired()
                .HasDefaultValueSql("NOW()")
                .HasConversion(new ValueConverter<DateTime, DateTime>(
                    v => NormalizarParaUtc(v),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc).ToLocalTime()));

            entity.Property(d => d.DataPagamento)
                .IsRequired(false)
                .HasConversion(new ValueConverter<DateTime?, DateTime?>(
                    v => v.HasValue ? NormalizarParaUtc(v.Value) : null,
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc).ToLocalTime() : null));
        });
    }

    // garante que datas locais (datetime.now) vao em utc pro timestamptz do postgres sem erro do npgsql
    private static DateTime NormalizarParaUtc(DateTime data)
    {
        return data.Kind switch
        {
            DateTimeKind.Utc => data,
            DateTimeKind.Local => data.ToUniversalTime(),
            _ => DateTime.SpecifyKind(data, DateTimeKind.Local).ToUniversalTime()
        };
    }
}
