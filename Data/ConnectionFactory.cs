using MySql.Data.MySqlClient;
namespace crud.Data;

public class ConnectionFactory
{
    private string connectionString;
    public ConnectionFactory()
    {
        // Environment -> Classe do C# que conversa com o sistema operacional.
        //GetEnvironmentVariable -> Método que pega a variável de ambiente no sistema do windows.

        string senha = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

        connectionString =
          $"Server=localhost;Database=crud_produtos;Uid=root;Pwd={senha};";
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);

    }
}

