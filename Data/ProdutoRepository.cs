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
                preco DOUBLE NOT NULL
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
                Preco = reader.GetDouble("preco")
            };

            produtos.Add(produto);
        }

        return produtos;
    }

    public bool Atualizar(string nome, double preco)
    {
        const string sql = "UPDATE produtos SET preco = @preco WHERE nome = @nome;";

        using var conexao = connectionFactory.GetConnection();
        conexao.Open();

        using var comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@nome", nome);
        comando.Parameters.AddWithValue("@preco", preco);

        return comando.ExecuteNonQuery() > 0;
    }

    public bool Deletar(string nome)
    {
        const string sql = "DELETE FROM produtos WHERE nome = @nome;";

        using var conexao = connectionFactory.GetConnection();
        conexao.Open();

        using var comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@nome", nome);

        return comando.ExecuteNonQuery() > 0;
    }
}
