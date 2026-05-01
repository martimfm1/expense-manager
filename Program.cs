


namespace gestorDespesas
{
    class Program
    {
        static List<Despesa> despesas = new List<Despesa>();

        static void Main(string[] args)
        {
            //criar ficheiro
            string file = "despesas.txt";
            if (!File.Exists(file))
            {
                Console.WriteLine("Task file not found.");
                Console.WriteLine("Creating a new task file...");
                File.Create(file).Close();
            }

            bool running = true;

            CarregarDespesas();

            while (running)
            {
                Console.WriteLine("-----GESTÃO DE DESPESAS PESSOAIS-----");

                Console.WriteLine("1 - Adicionar despesa");
                Console.WriteLine("2 - Listar despesas");
                Console.WriteLine("3 - Ver total gasto");
                Console.WriteLine("4 - Ver despesas por categoria");
                Console.WriteLine("5 - Sair");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AdicionarDespesa();
                        break;
                    case 2:
                        ListarDespesas();
                        break;
                    case 3:
                        VerTotalGasto();
                        break;
                    case 4:
                        VerDespesasPorCategoria();
                        break;
                    case 5:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarDespesa()
        {
            Console.WriteLine("Digite o nome da despesa:");
            string name = Console.ReadLine() ?? "";
            Console.WriteLine("Digite o valor da despesa:");
            double value = Convert.ToDouble(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            Console.WriteLine("Digite a descrição da despesa:");
            string description = Console.ReadLine() ?? "";
            Console.WriteLine("Digite a data da despesa:");
            string date = Console.ReadLine() ?? "";
            Console.WriteLine("Digite a categoria da despesa:");
            string category = Console.ReadLine() ?? "";

            Despesa despesa = new Despesa()
            {
                name = name ?? "",
                value = value,
                description = description ?? "",
                date = date ?? "",
                category = category ?? ""
            };

            despesas.Add(despesa);
            SalvarDespesas();
        }

        static void ListarDespesas()
        {
            Console.WriteLine("-----LISTA DE DESPESAS-----");
            foreach (var despesa in despesas)
            {
                Console.WriteLine($"Nome: {despesa.name}");
                Console.WriteLine($"Valor: {despesa.value}");
                Console.WriteLine($"Descrição: {despesa.description}");
                Console.WriteLine($"Data: {despesa.date}");
                Console.WriteLine($"Categoria: {despesa.category}");
                Console.WriteLine("-----------------------------------");
            }
        }

        static void VerTotalGasto()
        {
            double total = 0;
            foreach (var despesa in despesas)
            {
                total += despesa.value;
            }
            Console.WriteLine($"Total gasto: {total}");
        }

        static void VerDespesasPorCategoria()
        {
            Console.WriteLine("Digite a categoria para filtrar:");
            string category = Console.ReadLine() ?? "";
            Console.WriteLine($"-----DESPESAS NA CATEGORIA: {category}-----");
            foreach (var despesa in despesas)
            {
                if (despesa.category.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Nome: {despesa.name}");
                    Console.WriteLine($"Valor: {despesa.value}");
                    Console.WriteLine($"Descrição: {despesa.description}");
                    Console.WriteLine($"Data: {despesa.date}");
                    Console.WriteLine($"Categoria: {despesa.category}");
                    Console.WriteLine("-----------------------------------");
                }
            }
        }

        static void SalvarDespesas()
        {
            string file = "despesas.txt";
            using (StreamWriter writer = new StreamWriter(file))
            {
                foreach (var despesa in despesas)
                {
                    writer.WriteLine($"{despesa.name}|{despesa.value}|{despesa.description}|{despesa.date}|{despesa.category}");
                }
            }
        }
        static void CarregarDespesas()
        {
            string file = "despesas.txt";
            if (File.Exists(file))
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 5)
                    {
                        Despesa despesa = new Despesa()
                        {
                            name = parts[0],
                            value = Convert.ToDouble(parts[1], System.Globalization.CultureInfo.InvariantCulture),
                            description = parts[2],
                            date = parts[3],
                            category = parts[4]
                        };
                        despesas.Add(despesa);
                    }
                }
            }
        }
    }
}


class Despesa{
    public string name = "";
    public double value = 0;
    public string description = "";
    public string date = "";
    public string category = "";

}
