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
    public DbSet<Pimenta> Pimentas => Set<Pimenta>();
    public DbSet<Molho> Molhos => Set<Molho>();

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

        modelBuilder.Entity<Pimenta>(entity =>
        {
            entity.ToTable("Pimentas", t =>
            {
                t.HasCheckConstraint("CK_Pimentas_Scoville_Positivo", "\"NivelScoville\" > 0");
                t.HasCheckConstraint("CK_Pimentas_Custo_Positivo", "\"CustoPorGrama\" > 0");
            });

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.EspecieOrigem)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(p => p.NivelScoville)
                .IsRequired();

            entity.Property(p => p.CustoPorGrama)
                .IsRequired()
                .HasPrecision(10, 4);
        });

        modelBuilder.Entity<Molho>(entity =>
        {
            entity.ToTable("Molhos", t =>
            {
                t.HasCheckConstraint("CK_Molhos_Gramas_Positivo", "\"GramasPimentaPorFrasco\" > 0");
                t.HasCheckConstraint("CK_Molhos_Embalagem_NaoNegativa", "\"CustoEmbalagemIngredientes\" >= 0");
                t.HasCheckConstraint("CK_Molhos_PrecoVenda_Positivo", "\"PrecoVenda\" > 0");
            });

            entity.HasKey(m => m.Id);

            entity.Property(m => m.Nome)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(m => m.GramasPimentaPorFrasco)
                .IsRequired()
                .HasPrecision(10, 2);

            entity.Property(m => m.CustoEmbalagemIngredientes)
                .IsRequired()
                .HasPrecision(10, 2);

            entity.Property(m => m.PrecoVenda)
                .IsRequired()
                .HasPrecision(10, 2);

            // propriedades polimorficas/calculadas em memoria, nao vao pro banco
            entity.Ignore(m => m.CustoTotalProducao);
            entity.Ignore(m => m.LucroUnitario);
            entity.Ignore(m => m.MargemLucroPercentual);

            // heranca tph: guarda tradicional e reserva especial na mesma tabela usando discriminador
            entity.HasDiscriminator<string>("TipoMolho")
                .HasValue<Molho>("Tradicional")
                .HasValue<MolhoEspecialReserva>("EspecialReserva");

            entity.Property<string>("TipoMolho")
                .IsRequired()
                .HasMaxLength(30);

            // trava pra nao apagar pimenta que ainda esteja em uso em algum molho
            entity.HasOne(m => m.PimentaBase)
                .WithMany(p => p.Molhos)
                .HasForeignKey(m => m.PimentaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<MolhoEspecialReserva>(entity =>
        {
            // colunas extras do tph ficam nulas quando o molho for tradicional
            entity.Property(r => r.TempoMaturacaoDias);

            entity.Property(r => r.CustoAdicionalBarril)
                .HasPrecision(10, 2);
        });

        modelBuilder.Entity<Divida>(entity =>
        {
            // trava no banco pra nunca aceitar divida zerada/negativa nem quantidade invalida
            entity.ToTable("Dividas", t =>
            {
                t.HasCheckConstraint("CK_Dividas_Valor_Positivo", "\"Valor\" > 0");
                t.HasCheckConstraint("CK_Dividas_Quantidade_Positiva", "\"QuantidadeFrascos\" > 0");
            });

            entity.HasKey(d => d.Id);

            entity.Property(d => d.MolhoId)
                .IsRequired(false);

            entity.Property(d => d.QuantidadeFrascos)
                .IsRequired()
                .HasDefaultValue(1);

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

            // se algum dia apagar um molho do catalogo, preserva o historico financeiro da divida
            entity.HasOne(d => d.Molho)
                .WithMany(m => m.Dividas)
                .HasForeignKey(d => d.MolhoId)
                .OnDelete(DeleteBehavior.SetNull);
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
