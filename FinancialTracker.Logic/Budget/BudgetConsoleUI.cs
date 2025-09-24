using System;

namespace FinanceApp
{
    public class BudgetConsoleUI
    {
        private BudgetManager manager = new BudgetManager();

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Budget");
                Console.WriteLine("2. Show All Budgets");
                Console.WriteLine("0. Exit");
                Console.Write("Select option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddBudget(); break;
                    case "2": manager.ShowAllBudgets(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Invalid option"); break;
                }
            }
        }

        private void AddBudget()
        {
            Console.Write("Enter annual pay: ");
            decimal annual = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter monthly budget: ");
            decimal monthly = decimal.Parse(Console.ReadLine() ?? "0");

            Budget budget = new Budget { AnnualPay = annual, MonthlyBudget = monthly };
            manager.AddBudget(budget);
        }
    }
}
