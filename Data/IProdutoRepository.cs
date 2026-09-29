using crud.Models;

namespace crud.Data;

public interface IProdutoRepository
{
    void EnsureTabelaProdutos();
    void Criar(Produto produto);
    List<Produto> ListarProdutos();
    bool Atualizar(int id, decimal preco);
    bool Deletar(int id);
}
