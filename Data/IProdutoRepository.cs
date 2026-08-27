using crud.Models;

namespace crud.Data;

public interface IProdutoRepository
{
    void EnsureTabelaProdutos();
    void Criar(Produto produto);
    List<Produto> ListarProdutos();
    bool Atualizar(string nome, double preco);
    bool Deletar(string nome);
}
