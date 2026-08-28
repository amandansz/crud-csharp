using crud.Models;
using crud.Services;
using crud.Data;
using System.Globalization;

public class Program
{
    private static ProdutoService produtoService = null!;

    static void Main()
    {

        Console.WriteLine("Sistema de produtos iniciando...");

        try
        {
            ConnectionFactory factory = new ConnectionFactory();
            ProdutoRepository produtoRepository = new ProdutoRepository(factory);

            using var conexao = factory.GetConnection();

            conexao.Open();
            produtoRepository.EnsureTabelaProdutos();
            produtoService = new ProdutoService(produtoRepository);

            Console.WriteLine("✅ Conexão com o banco realizada com sucesso!");
            Console.WriteLine("✅ Tabela de produtos pronta para uso.");

            conexao.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao conectar: {ex.Message}");
            if (ex.InnerException is not null)
            {
                Console.WriteLine($"ℹ️ Detalhe técnico: {ex.InnerException.Message}");
            }
            return;
        }

        while (true)
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

                case "3":
                    AtualizarProduto();
                    break;

                case "4":
                    DeletarProduto();
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
        Console.WriteLine("3 - Atualizar Produto");
        Console.WriteLine("4 - Deletar Produto");
        Console.WriteLine("5 - Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void CriarProduto()
    {
        if (produtoService == null)
        {
            Console.WriteLine("Serviço de produto não inicializado.");
            return;
        }
        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        decimal preco = LerPrecoValido();

        try
        {
            produtoService.Criar(nome, preco);
            Console.WriteLine("Produto criado com sucesso!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro de validação: {ex.Message}");
        }
    }

    static void AtualizarProduto()
    {
        Console.Write("Nome do produto: ");
        string nome = Console.ReadLine() ?? string.Empty;
        
        decimal preco = LerPrecoValido();

        try
        {
            bool atualizado = produtoService.Atualizar(nome, preco);
            Console.WriteLine(atualizado
                ? "Produto atualizado com sucesso!"
                : "Nenhum produto encontrado com esse nome.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro de validação: {ex.Message}");
        }
    }

        static void DeletarProduto()
    {
        Console.Write("Nome do produto: ");
        string nome = Console.ReadLine() ?? string.Empty;

        try
        {
            bool deletado = produtoService.Deletar(nome);
            Console.WriteLine(deletado
                ? "Produto deletado com sucesso!"
                : "Nenhum produto encontrado com esse nome.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro de validação: {ex.Message}");
        }
    }

    static decimal LerPrecoValido()
    {
        while (true)
        {
            Console.Write("Preço: ");
            string entrada = Console.ReadLine() ?? string.Empty;

            bool validoCulturaAtual = decimal.TryParse(
                entrada,
                NumberStyles.Float,
                CultureInfo.CurrentCulture,
                out decimal precoCulturaAtual);

            if (validoCulturaAtual)
            {
                return precoCulturaAtual;
            }

            bool validoInvariant = decimal.TryParse(
                entrada,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out decimal precoInvariant);

            if (validoInvariant)
            {
                return precoInvariant;
            }

            Console.WriteLine("Preço inválido. Digite um número válido (ex.: 10,50 ou 10.50).");
        }
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

