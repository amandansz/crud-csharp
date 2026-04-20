using System.Runtime.CompilerServices;

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

        while (true)
        {
            exibirMenu();
            String opcao = Console.ReadLine() ?? string.Empty;

            switch (opcao)
            {
                case "1":
                    criarProduto();
                    break;
                case "2":
                    listarProduto();
                    break;
                case "3":
                    atualizarProduto();
                    break;
                case "4":
                    deletarProduto();
                    break;
                case "5":
                    Console.WriteLine("Saindo do sistema...");
                    return;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;

            }
        }
    }
    static void exibirMenu()
    {
        Console.WriteLine("======MENU=======");
        Console.WriteLine("1 - Criar Produto");
        Console.WriteLine("2 - Listar Produtos");
        Console.WriteLine("3 - Atualizar Produto");
        Console.WriteLine("4 - Deletar Produto");
        Console.WriteLine("5 - Sair do sistema");
        Console.Write("Escolha uma opção: ");
    }

    //CREATE 
    static void criarProduto()
    {
        Console.WriteLine("Digite o nome do produto: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Digite o preço do produto: ");
        double preco = double.Parse(Console.ReadLine() ?? "0");

        // Criação do objeto
        Produto p = new Produto();

        // Preenchimento de dados
        p.Id = idAtual++;
        p.Nome = nome;
        p.Preco = preco;

        // Adiciona na lista  
        produtos.Add(p);

        Console.WriteLine("Produto criado com sucesso!");
    }

    //READ
    static void listarProduto()
    {
        Console.WriteLine("/nLista de Produtos:");

        foreach (var produto in produtos)
        {
            Console.WriteLine($"Id: {produto.Id} | Nome: {produto.Nome} | Preço: {produto.Preco}");
        }
    }

    //UPDATE
    static void atualizarProduto()
    {
        Console.WriteLine("Digite o ID do produto que deseja atualizar: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        Produto? produto = produtos.FirstOrDefault(p => p.Id == id);

        if (produto != null)
        {
            Console.WriteLine("Novo nome: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Novo preço: ");
            double preco = double.Parse(Console.ReadLine() ?? "0");

            produto.Nome = nome;
            produto.Preco = preco;

            Console.WriteLine("Produto atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
    }

    //DELETE
    static void deletarProduto()
    {
        Console.WriteLine("Digite o ID do produto que seja deletar: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        Produto? produto = produtos.FirstOrDefault(p => p.Id == id);

        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }
        else
        {
            produtos.Remove(produto);
            Console.WriteLine("Produto deletado com sucesso!");
        }


    }
}


