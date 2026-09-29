using crud.Data;
using crud.Models;

namespace crud.Services;

public class ProdutoService
{
    private readonly IProdutoRepository produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        this.produtoRepository = produtoRepository;
    }

    public void Criar(string nome, decimal preco)
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
            Nome = nome.Trim(),
            Preco = preco
        };

        produtoRepository.Criar(produto);
    }

    public List<Produto> ListarProdutos()
    {
        return produtoRepository.ListarProdutos();
    }

    public bool Atualizar(int id, decimal preco)
    {
        ValidarProduto(id, preco);
        return produtoRepository.Atualizar(id, preco);
    }

    public bool Deletar(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("O id do produto é obrigatório.");
        }

        return produtoRepository.Deletar(id);
    }

    private static void ValidarProduto(int id, decimal preco)
    {
        if (id <= 0)
        {
            throw new ArgumentException("O id do produto é obrigatório.");
        }

        if (preco < 0)
        {
            throw new ArgumentException("O preço não pode ser negativo.");
        }
    }
}

