using Microsoft.EntityFrameworkCore;
using Models;

// Arquivo: WppDbContext.cs
namespace Data;

/// <summary>
/// Representa o contexto de banco de dados para a entidade GroupWpp.
/// </summary>
public class WppDbContext : DbContext
{
    /// <summary>
    /// Construtor padrão sem parâmetros para o contexto do banco de dados.
    /// </summary>
    public WppDbContext() { }

    /// <summary>
    /// Construtor que aceita as opções de contexto do banco de dados.
    /// </summary>
    /// <param name="options">Opções de contexto do banco de dados.</param>
    public WppDbContext(DbContextOptions<WppDbContext> options) : base(options) { }

    /// <summary>
    /// Obtém ou define o conjunto de dados para a entidade PollOption no contexto do banco de dados.
    /// </summary>
    public DbSet<CommunityData> CommunityData { get; set; }

    /// <summary>
    /// Obtém ou define o conjunto de dados para a entidade GroupWpp no contexto do banco de dados.
    /// </summary>
    public DbSet<WppGroup> GroupWppData { get; set; }

    /// <summary>
    /// Obtém ou define o conjunto de dados para a entidade MemberGroup no contexto do banco de dados.
    /// </summary>
    public DbSet<MemberGroup> MemberGroupData { get; set; }

    /// <summary>
    /// Obtém ou define o conjunto de dados para a entidade MessageData no contexto do banco de dados.
    /// </summary>
    public DbSet<MessageData> MessageData { get; set; }

    /// <summary>
    /// Obtém ou define o conjunto de dados para a entidade SendPoll no contexto do banco de dados.
    /// </summary>
    public DbSet<SendPoll> SendPoll { get; set; }

    /// <summary>
    /// Obtém ou define o conjunto de dados para a entidade PollOption no contexto do banco de dados.
    /// </summary>
    public DbSet<PollOptions> PollOption { get; set; }

    /// <summary>
    /// Obtém ou define o conjunto de dados para a entidade LogData no contexto do banco de dados.
    /// </summary>
    public DbSet<LogData> LogData { get; set; }

    /// <summary>
    /// Configura as opções de contexto do banco de dados ao usar o provedor SQL Server.
    /// </summary>
    /// <param name="optionsBuilder">Construtor de opções de contexto do banco de dados.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(DatabaseInitializer.GetDefaultConnectionString());

    /// <summary>
    /// Configura o modelo do banco de dados durante a construção do modelo.
    /// </summary>
    /// <param name="builder">Construtor do modelo do banco de dados.</param>
    /// <exception cref="ArgumentNullException">Exceção lançada quando o parâmetro builder é nulo.</exception>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var fks = builder
             .Model
             .GetEntityTypes()
             .SelectMany(t => t.GetForeignKeys())
             .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var item in fks)
        {
            item.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(builder);

        _ = builder
            .Entity<CommunityData>()
            .HasKey(x => x.ID);

        _ = builder
            .Entity<CommunityData>()
            .HasIndex(x => x.CommunityID);

        _ = builder
            .Entity<CommunityData>()
            .HasMany(cd => cd.WppGroups)
            .WithOne(wg => wg.CommunityData)
            .HasForeignKey(wg => wg.CommunityID);

        _ = builder
            .Entity<WppGroup>()
            .HasIndex(o => o.Name);

        _ = builder
            .Entity<WppGroup>()
            .HasKey(o => o.ID);

        _ = builder
            .Entity<WppGroup>()
            .HasMany(gw => gw.MemberGroup)
            .WithOne(mg => mg.Group) // 1 grupo possui N membros
            .HasForeignKey(x => x.GroupID);

        _ = builder
            .Entity<MemberGroup>()
            .HasKey(x => x.MemberID); // Define a chave primária para MemberGroup

        _ = builder
            .Entity<MemberGroup>()
            .HasOne(mg => mg.Group)
            .WithMany(gw => gw.MemberGroup)  // N membros pertencem a 1 grupo
            .HasForeignKey(x => x.GroupID);

        _ = builder
            .Entity<MemberGroup>()
            .Property(x => x.MemberID)
            .ValueGeneratedOnAdd(); // Indicar que a chave primária é gerada automaticamente

        _ = builder
            .Entity<MessageData>()
            .HasIndex(o => o.MessageText);

        _ = builder
            .Entity<SendPoll>()
            .HasKey(o => o.PollID);

        _ = builder
            .Entity<PollOptions>()
            .HasKey(o => o.PollOptionID);

        _ = builder
            .Entity<PollOptions>()
            .HasIndex(o => o.name);

        _ = builder
            .Entity<MessageData>()
            .HasOne(ms => ms.SendPoll)
            .WithOne(sp => sp.MessageSettings)
            .HasForeignKey<SendPoll>(sp => sp.MessageID);

        _ = builder
            .Entity<SendPoll>()
            .HasMany(sp => sp.PollOptions)
            .WithOne(new SendPoll().message)
            .HasForeignKey(po => po.PollID);
    }
}
