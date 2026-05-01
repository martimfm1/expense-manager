namespace expenseManager
{
    class Program
    {
        static List<Expense> expenses = new List<Expense>();

        static void Main(string[] args)
        {
            string file = "expenses.txt";
            if (!File.Exists(file))
            {
                Console.WriteLine("Expense file not found.");
                Console.WriteLine("Creating a new expense file...");
                File.Create(file).Close();
            }

            bool running = true;
            LoadExpenses();

            while (running)
            {
                Console.WriteLine("-----PERSONAL EXPENSE MANAGER-----");
                Console.WriteLine("1 - Add expense");
                Console.WriteLine("2 - List expenses");
                Console.WriteLine("3 - View total spent");
                Console.WriteLine("4 - View expenses by category");
                Console.WriteLine("5 - Exit");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddExpense();
                        break;
                    case 2:
                        ListExpenses();
                        break;
                    case 3:
                        ViewTotalSpent();
                        break;
                    case 4:
                        ViewExpensesByCategory();
                        break;
                    case 5:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddExpense()
        {
            Console.WriteLine("Enter expense name:");
            string name = Console.ReadLine() ?? "";
            Console.WriteLine("Enter expense value:");
            double value = Convert.ToDouble(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            Console.WriteLine("Enter expense description:");
            string description = Console.ReadLine() ?? "";
            Console.WriteLine("Enter expense date:");
            string date = Console.ReadLine() ?? "";
            Console.WriteLine("Enter expense category:");
            string category = Console.ReadLine() ?? "";

            Expense expense = new Expense()
            {
                name = name,
                value = value,
                description = description,
                date = date,
                category = category
            };

            expenses.Add(expense);
            SaveExpenses();
        }

        static void ListExpenses()
        {
            Console.WriteLine("-----EXPENSE LIST-----");
            foreach (var expense in expenses)
            {
                Console.WriteLine($"Name: {expense.name}");
                Console.WriteLine($"Value: {expense.value}");
                Console.WriteLine($"Description: {expense.description}");
                Console.WriteLine($"Date: {expense.date}");
                Console.WriteLine($"Category: {expense.category}");
                Console.WriteLine("-----------------------------------");
            }
        }

        static void ViewTotalSpent()
        {
            double total = 0;
            foreach (var expense in expenses)
            {
                total += expense.value;
            }
            Console.WriteLine($"Total spent: {total}");
        }

        static void ViewExpensesByCategory()
        {
            Console.WriteLine("Enter category to filter:");
            string category = Console.ReadLine() ?? "";
            Console.WriteLine($"-----EXPENSES IN CATEGORY: {category}-----");
            foreach (var expense in expenses)
            {
                if (expense.category.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Name: {expense.name}");
                    Console.WriteLine($"Value: {expense.value}");
                    Console.WriteLine($"Description: {expense.description}");
                    Console.WriteLine($"Date: {expense.date}");
                    Console.WriteLine($"Category: {expense.category}");
                    Console.WriteLine("-----------------------------------");
                }
            }
        }

        static void SaveExpenses()
        {
            string file = "expenses.txt";
            using (StreamWriter writer = new StreamWriter(file))
            {
                foreach (var expense in expenses)
                {
                    writer.WriteLine($"{expense.name}|{expense.value}|{expense.description}|{expense.date}|{expense.category}");
                }
            }
        }

        static void LoadExpenses()
        {
            string file = "expenses.txt";
            if (File.Exists(file))
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 5)
                    {
                        Expense expense = new Expense()
                        {
                            name = parts[0],
                            value = Convert.ToDouble(parts[1], System.Globalization.CultureInfo.InvariantCulture),
                            description = parts[2],
                            date = parts[3],
                            category = parts[4]
                        };
                        expenses.Add(expense);
                    }
                }
            }
        }
    }
}

class Expense
{
    public string name = "";
    public double value = 0;
    public string description = "";
    public string date = "";
    public string category = "";
}