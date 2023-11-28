namespace Data;

/// <summary>
/// Classe responsável por inicializar e configurar o banco de dados.
/// </summary>
public class DatabaseInitializer
{
    /// <summary>
    /// Inicializa o banco de dados, garantindo que ele seja criado se ainda não existir.
    /// </summary>
    /// <param name="context">O contexto do banco de dados a ser inicializado.</param>
    public static void Initialize(WppDbContext context) => context.Database.EnsureCreated();

    /// <summary>
    /// Obtém a string de conexão padrão para o banco de dados.
    /// </summary>
    /// <returns>A string de conexão padrão.</returns>
    public static string GetDefaultConnectionString() => $"Server=(localdb)\\MSSQLLocalDB;Database=WppDB;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=True;";
}