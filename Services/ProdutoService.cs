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

    public bool Atualizar(string nome, decimal preco)
    {
        ValidarProduto(nome, preco);
        return produtoRepository.Atualizar(nome.Trim(), preco);
    }

    public bool Deletar(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome do produto é obrigatório.");
        }

        return produtoRepository.Deletar(nome.Trim());
    }

    private static void ValidarProduto(string nome, decimal preco)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome do produto é obrigatório.");
        }

        if (preco < 0)
        {
            throw new ArgumentException("O preço não pode ser negativo.");
        }
    }
}

