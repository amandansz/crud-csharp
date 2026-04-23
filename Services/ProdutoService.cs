public class ProdutoService
{
     private List<Produto> produtos = new List<Produto>();
    private int idAtual = 1;

        public void Criar(string nome, double preco)
    {
        Produto p = new Produto
        {
            Id = idAtual++,
            Nome = nome,
            Preco = preco
        };

        produtos.Add(p);
    }
}
