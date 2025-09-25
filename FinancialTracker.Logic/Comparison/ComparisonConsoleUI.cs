// using System;
// using FinanceApp;
// using FinanceApp.Services;


// namespace FinanceApp.UI
// {
//     public class ComparisonConsoleUI
//     {
//         private readonly TransactionBook book;
//         private readonly BudgetManager budgetManager;
//         private readonly ComparisonService comparison;

//         public ComparisonConsoleUI(TransactionBook book, BudgetManager budgetManager, ComparisonService comparison)
//         {
//             this.book = book;
//             this.budgetManager = budgetManager;
//             this.comparison = comparison;
//         }

//         public void Run()
//         {
//             bool running = true;
//             while (running)
//             {
//                 ShowMenu();
//                 Console.Write("Select option: ");
//                 string? choice = Console.ReadLine();

//                 switch (choice)
//                 {
//                     case "1":
//                         CompareMonthlySpending();
//                         break;
//                     case "2":
//                         CompareYearlySpending();
//                         break;
//                     case "3":
//                         CompareMonthlySpendingByCategory();
//                         break;
//                     case "4":
//                         CompareYearlySpendingByCategory();
//                         break;
//                     case "5":
//                         CheckMonthlyBudget();
//                         break;
//                     case "6":
//                         CheckYearlyBudget();
//                         break;
//                     case "0":
//                         running = false;
//                         break;
//                     default:
//                         Console.WriteLine("Invalid option.");
//                         break;
//                 }
//             }
//         }

//         private void ShowMenu()
//         {
//             Console.WriteLine();
//             Console.WriteLine("=== Comparison Menu ===");
//             Console.WriteLine("1) Spending vs Spending (Monthly: this vs last)");
//             Console.WriteLine("2) Spending vs Spending (Yearly: this vs last)");
//             Console.WriteLine("3) Spending vs Spending (Monthly by Category)");
//             Console.WriteLine("4) Spending vs Spending (Yearly by Category)");
//             Console.WriteLine("5) Spending vs Budget (Monthly)");
//             Console.WriteLine("6) Spending vs Budget (Yearly)");
//             Console.WriteLine("0) Back");
//         }

//         private void CompareMonthlySpending()
//         {
//             int year = AskInt("Year (e.g., 2025): ");
//             int month = AskInt("Month (1-12): ");

//             var res = comparison.CompareMonthlySpending(book, year, month);
//             Console.WriteLine("[" + res.Label + "]");
//             Console.WriteLine("  This: " + res.CurrentTotal.ToString("C"));
//             Console.WriteLine("  Prev: " + res.PreviousTotal.ToString("C"));
//             Console.WriteLine("  Diff: " + res.Difference.ToString("C"));
//         }

//         private void CompareYearlySpending()
//         {
//             int year = AskInt("Year (e.g., 2025): ");

//             var res = comparison.CompareYearlySpending(book, year);
//             Console.WriteLine("[" + res.Label + "]");
//             Console.WriteLine("  This: " + res.CurrentTotal.ToString("C"));
//             Console.WriteLine("  Prev: " + res.PreviousTotal.ToString("C"));
//             Console.WriteLine("  Diff: " + res.Difference.ToString("C"));
//         }

//         private void CompareMonthlySpendingByCategory()
//         {
//             int year = AskInt("Year (e.g., 2025): ");
//             int month = AskInt("Month (1-12): ");
//             Console.Write("Category: ");
//             string? category = Console.ReadLine();

//             var res = comparison.CompareMonthlySpendingByCategory(book, year, month, category ?? "");
//             Console.WriteLine("[" + res.Label + "]");
//             Console.WriteLine("  This: " + res.CurrentTotal.ToString("C"));
//             Console.WriteLine("  Prev: " + res.PreviousTotal.ToString("C"));
//             Console.WriteLine("  Diff: " + res.Difference.ToString("C"));
//         }

//         private void CompareYearlySpendingByCategory()
//         {
//             int year = AskInt("Year (e.g., 2025): ");
//             Console.Write("Category: ");
//             string? category = Console.ReadLine();

//             var res = comparison.CompareYearlySpendingByCategory(book, year, category ?? "");
//             Console.WriteLine("[" + res.Label + "]");
//             Console.WriteLine("  This: " + res.CurrentTotal.ToString("C"));
//             Console.WriteLine("  Prev: " + res.PreviousTotal.ToString("C"));
//             Console.WriteLine("  Diff: " + res.Difference.ToString("C"));
//         }

//         private void CheckMonthlyBudget()
//         {
//             int year = AskInt("Year (e.g., 2025): ");
//             int month = AskInt("Month (1-12): ");

//             var res = comparison.CheckMonthlyBudget(book, budgetManager.GetAllBudgets(), year, month);
//             Console.WriteLine("[" + res.Label + "]");
//             Console.WriteLine("  Spent : " + res.Spent.ToString("C"));
//             Console.WriteLine("  Budget: " + res.Budget.ToString("C"));
//             Console.WriteLine("  Status: " + (res.IsOverBudget ? "Over Budget" : "Within Budget"));
//         }

//         private void CheckYearlyBudget()
//         {
//             int year = AskInt("Year (e.g., 2025): ");

//             var res = comparison.CheckYearlyBudget(book, budgetManager.GetAllBudgets(), year);
//             Console.WriteLine("[" + res.Label + "]");
//             Console.WriteLine("  Spent : " + res.Spent.ToString("C"));
//             Console.WriteLine("  Budget: " + res.Budget.ToString("C"));
//             Console.WriteLine("  Status: " + (res.IsOverBudget ? "Over Budget" : "Within Budget"));
//         }

//         private static int AskInt(string prompt)
//         {
//             Console.Write(prompt);
//             string? s = Console.ReadLine();
//             int v;
//             if (int.TryParse(s, out v))
//             {
//                 return v;
//             }
//             return 0;
//         }
//     }
// }
