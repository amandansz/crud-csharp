using crud.Models;
using MySql.Data.MySqlClient;

namespace crud.Data;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ConnectionFactory connectionFactory;

    public ProdutoRepository(ConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public void EnsureTabelaProdutos()
    {
        const string sql = @"
            CREATE TABLE IF NOT EXISTS produtos (
                id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                nome VARCHAR(255) NOT NULL,
                preco DECIMAL(10,2) NOT NULL
            );";

        using var conexao = connectionFactory.GetConnection();
        conexao.Open();

        using var comando = new MySqlCommand(sql, conexao);
        comando.ExecuteNonQuery();
    }

    public void Criar(Produto produto)
    {
        const string sql = @"
            INSERT INTO produtos (nome, preco)
            VALUES (@nome, @preco);
            SELECT LAST_INSERT_ID();";

        using var conexao = connectionFactory.GetConnection();
        conexao.Open();

        using var comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@nome", produto.Nome);
        comando.Parameters.AddWithValue("@preco", produto.Preco);

        object? resultado = comando.ExecuteScalar();
        if (resultado is not null)
        {
            produto.Id = Convert.ToInt32(resultado);
        }
    }

    public List<Produto> ListarProdutos()
    {
        const string sql = "SELECT id, nome, preco FROM produtos ORDER BY id;";

        using var conexao = connectionFactory.GetConnection();
        conexao.Open();

        using var comando = new MySqlCommand(sql, conexao);
        using var reader = comando.ExecuteReader();

        List<Produto> produtos = new();

        while (reader.Read())
        {
            Produto produto = new Produto
            {
                Id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Preco = reader.GetDecimal("preco")
            };

            produtos.Add(produto);
        }

        return produtos;
    }

    public bool Atualizar(int id, decimal preco)
    {
        const string sql = "UPDATE produtos SET preco = @preco WHERE id = @id;";

        using var conexao = connectionFactory.GetConnection();
        conexao.Open();

        using var comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@id", id);
        comando.Parameters.AddWithValue("@preco", preco);

        return comando.ExecuteNonQuery() > 0;
    }

    public bool Deletar(int id)
    {
        const string sql = "DELETE FROM produtos WHERE id = @id;";

        using var conexao = connectionFactory.GetConnection();
        conexao.Open();

        using var comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@id", id);

        return comando.ExecuteNonQuery() > 0;
    }
}
