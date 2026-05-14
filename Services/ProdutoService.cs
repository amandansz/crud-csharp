using crud.Models;

namespace crud.Services;

public class ProdutoService
{
    private List<Produto> produtos = new();
    private int idAtual = 1;

    public void Criar(string nome, double preco)
    {
        Produto produto = new Produto
        {
            Id = idAtual++,
            Nome = nome,
            Preco = preco
        };

        produtos.Add(produto);
    }

    public List<Produto> ListarProdutos()
    {
        return produtos;
    }
}

