using crud.Models;
using crud.Services;

public class Program
{
    private static ProdutoService produtoService = new ProdutoService();

    static void Main()
    {

        Console.WriteLine("Sistema de produtos iniciando..."); while (true)
        {
            ExibirMenu();
            string opcao = Console.ReadLine() ?? string.Empty;

            switch (opcao)
            {
                case "1":
                    CriarProduto();
                    break;

                case "2":
                    ListarProdutos();
                    break;

                case "5":
                    Console.WriteLine("Saindo do sistema...");
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void ExibirMenu()
    {
        Console.WriteLine("====== MENU ======");
        Console.WriteLine("1 - Criar Produto");
        Console.WriteLine("2 - Listar Produtos");
        Console.WriteLine("5 - Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void CriarProduto()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Preço: ");
        double preco = double.Parse(Console.ReadLine() ?? "0");

        produtoService.Criar(nome, preco);

        Console.WriteLine("Produto criado com sucesso!");
    }

    static void ListarProdutos()
    {
        List<Produto> lista = produtoService.ListarProdutos();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        Console.WriteLine("\n=== LISTA DE PRODUTOS ===");

        foreach (var produto in lista)
        {
            Console.WriteLine($"ID: {produto.Id}");
            Console.WriteLine($"Nome: {produto.Nome}");
            Console.WriteLine($"Preço: {produto.Preco}");
            Console.WriteLine("-------------------");
        }
    }
}

