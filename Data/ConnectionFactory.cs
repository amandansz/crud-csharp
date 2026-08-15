using MySql.Data.MySqlClient;
namespace crud.Data;

public class ConnectionFactory
{
        private readonly string connectionString;

    public ConnectionFactory()
    {
                string host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
                string porta = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
                string banco = Environment.GetEnvironmentVariable("DB_NAME") ?? "crud_produtos";
                string usuario = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
                string senha = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

                connectionString =
                    $"Server={host};Port={porta};Database={banco};Uid={usuario};Pwd={senha};";
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);

    }
}

