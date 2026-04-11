public class Produto // Classe Produto que representa um produto com propriedades Id, Nome e Preco
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public double Preco { get; set; }

}

public class Program
{
    static List<Produto> produtos = new List<Produto>();
    static int idAtual = 1;


    static void Main()
    {
        Console.WriteLine("Sistema de produtos iniciando...");
        Produto p = new Produto();
        p.Id = 1;
        p.Nome = "Teste";
        p.Preco = 10.0;

        produtos.Add(p);

        Console.WriteLine(produtos[0].Nome);
    }
}
