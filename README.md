# Expense Manager

A terminal-based personal expense manager built in C# as a learning project.

---

## How to use

When you start the program, a menu appears with 5 options:

-----GESTÃO DE DESPESAS PESSOAIS-----
1 - Adicionar despesa
2 - Listar despesas
3 - Ver total gasto
4 - Ver despesas por categoria
5 - Sair

**1 - Add expense** — enter the name, value, description, date and category of the expense.

**2 - List expenses** — shows all saved expenses.

**3 - View total spent** — shows the sum of all expenses.

**4 - View by category** — filter expenses by category name.

**5 - Exit** — closes the program. Expenses are saved automatically.

> Expenses are saved in a `despesas.txt` file in the same folder as the program.

---

## For developers

### How it works

- Expenses are stored in memory as a `List<Despesa>` during the session
- On startup, `CarregarDespesas()` reads `despesas.txt` and loads all expenses
- When adding an expense, `SalvarDespesas()` rewrites the file with the updated list
- Each expense is stored as a line in the format: `name|value|description|date|category`

### Project structure

- `Program.cs` — all program logic and the `Despesa` class
- `despesas.txt` — automatically generated file containing the expenses

### Requirements

- .NET SDK 10.0 or higher

### Run locally

```bash
git clone https://github.com/yourusername/gestor-despesas
cd gestor-despesas
dotnet run
```

### Build to .exe

```bash
dotnet publish -c Release -r win-x64 --self-contained true
```

The `.exe` file will be in `bin/Release/net10.0/win-x64/publish/`.

---

## Technologies

- C# / .NET