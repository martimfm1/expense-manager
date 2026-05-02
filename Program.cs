
using System.Security.Cryptography.X509Certificates;

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
                Console.Clear();
                Console.WriteLine("Expense file not found.");
                Console.WriteLine("Creating a new expense file...");
                File.Create(file).Close();
                Thread.Sleep(1500);
                Console.Clear();
            }

            bool running = true;
            LoadExpenses();

            while (running)
            {
                
                Console.Clear();
                PrintWithPadding("-----PERSONAL EXPENSE MANAGER-----", 2);
                PrintWithPadding("1 - Add expense",1);
                PrintWithPadding("2 - List expenses",1);
                PrintWithPadding("3 - View total spent",1);
                PrintWithPadding("4 - View expenses by category",1);
                PrintWithPadding("5 - Settings",1);
                PrintWithPadding("6 - Exit",1);

                int option = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

                switch (option)
                {
                    case 1:
                        AddExpense();
                        PrintWithPadding("Expense added successfully!");
                        Thread.Sleep(1500);
                        PrintWithPadding("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        ListExpenses();
                        PrintWithPadding("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        ViewTotalSpent();
                        PrintWithPadding("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        ViewExpensesByCategory();
                        PrintWithPadding("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        SettingsMenu();
                        PrintWithPadding("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 6:
                        running = false;
                        PrintWithPadding("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    default:
                        PrintWithPadding("Invalid option. Please try again.");
                        PrintWithPadding("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        static void AddExpense()
        {
            Console.Clear();
            PrintWithPadding("Enter expense name:");
            string name = Console.ReadLine() ?? "";
            Console.Clear();
            PrintWithPadding("Enter expense value:");
            double value = Convert.ToDouble(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            Console.Clear();
            PrintWithPadding("Enter expense description:");
            string description = Console.ReadLine() ?? "";
            Console.Clear();
            PrintWithPadding("Enter expense date:");
            string date = Console.ReadLine() ?? "";
            Console.Clear();
            PrintWithPadding("Enter expense category:");
            string category = Console.ReadLine() ?? "";
            Console.Clear();

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
            Console.Clear();
            PrintWithPadding("-----EXPENSE LIST-----" , 2);
            foreach (var expense in expenses)
            {
                PrintWithPadding($"Name: {expense.name}");
                PrintWithPadding($"Value: {expense.value}");
                PrintWithPadding($"Description: {expense.description}");
                PrintWithPadding($"Date: {expense.date}");
                PrintWithPadding($"Category: {expense.category}");
                PrintWithPadding("-----------------------------------", 1);
            }
        }

static string currency = "$";

        static void ViewTotalSpent()
        {
            double total = 0;
            foreach (var expense in expenses)
            {
                total += expense.value;
            }
            Console.Clear();
            PrintWithPadding($"Total spent: {currency}{total}", 2);
        }

        static void ViewExpensesByCategory()
        {
            Console.Clear();
            PrintWithPadding("----- CATEGORIES -----", 2);
            PrintWithPadding($"Categories: {string.Join(", ", expenses.Select(e => e.category).Distinct())}", 1);
            PrintWithPadding("Enter category to filter:");
            string category = Console.ReadLine() ?? "";
            Console.Clear();
            PrintWithPadding($"----- EXPENSES IN CATEGORY: {category} -----", 2);
            foreach (var expense in expenses)
            {
                if (expense.category.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    PrintWithPadding($"Name: {expense.name}");
                    PrintWithPadding($"Value: {expense.value}");
                    PrintWithPadding($"Description: {expense.description}");
                    PrintWithPadding($"Date: {expense.date}");
                    PrintWithPadding($"Category: {expense.category}");
                    PrintWithPadding("-----------------------------------", 1);
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

        static ConsoleColor currentColor = ConsoleColor.Gray;

        static void SettingsMenu()
        {
            Console.Clear();
            PrintWithPadding("-----SETTINGS-----", 2);
            PrintWithPadding("1 - Clear all expenses",1);
            PrintWithPadding("2 - Change currency",1);
            PrintWithPadding("3 - Change terminal color",1);
            PrintWithPadding("4 - Back to main menu",1);

            int option = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

            switch (option)
            {
                case 1:
                    expenses.Clear();
                    SaveExpenses();
                    Console.Clear();
                    PrintWithPadding("All expenses cleared.");
                    Thread.Sleep(1000);
                    break;
                case 2:
                    Console.Clear();
                    PrintWithPadding("Enter new currency symbol:");
                    currency = Console.ReadLine() ?? "$";
                    PrintWithPadding("Currency changed.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    PrintWithPadding("Available colors: Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray, DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White");
                    PrintWithPadding("Enter new terminal color:");
                    string colorInput = Console.ReadLine() ?? "White";
                    if (Enum.TryParse(colorInput, true, out ConsoleColor color))
                    {
                        currentColor = color;
                        PrintWithPadding("Terminal color changed.");
                    }
                    else
                    {
                        PrintWithPadding("Invalid color. Using default.");
                    }
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;
                case 4:
                    Console.Clear();
                    PrintWithPadding("Returning to main menu...");
                    Thread.Sleep(1000);
                    Console.Clear();
                    break;
                default:
                    PrintWithPadding("Invalid option. Returning to main menu.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;
            }
        }
        static void PrintWithPadding(string text, int paddingBottom = 0)
        {
            int spaces = Math.Max(0, (Console.WindowWidth - text.Length) / 2);
            Console.ForegroundColor = currentColor;
            Console.WriteLine(new string(' ', spaces) + text);
            Console.ResetColor();
            for (int i = 0; i < paddingBottom; i++)
            {
                Console.WriteLine();
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