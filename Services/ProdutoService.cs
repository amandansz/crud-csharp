using crud.Models;

namespace crud.Services;

public class ProdutoService
{
    private readonly List<Produto> produtos = new();
    private int idAtual = 1;

    public void Criar(string nome, double preco)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome do produto é obrigatório.");
        }

        if (preco < 0)
        {
            throw new ArgumentException("O preço não pode ser negativo.");
        }

        Produto produto = new Produto
        {
            Id = idAtual++,
            Nome = nome.Trim(),
            Preco = preco
        };

        produtos.Add(produto);
    }

    public List<Produto> ListarProdutos()
    {
        return new List<Produto>(produtos);
    }
}

