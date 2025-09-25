using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceApp
{
    public class BudgetManager
    {
        private List<Budget> budgets = new List<Budget>();

        public void AddBudget(Budget budget)
        {
            budgets.Add(budget);
            Console.WriteLine("Budget added: " + budget);
        }
        
        public IReadOnlyList<Budget> GetAllBudgets()
        {
            return budgets.AsReadOnly();
        }

        public void ShowAllBudgets()
        {
            if (!budgets.Any())
            {
                Console.WriteLine("No budgets have been set.");
                return;
            }

            foreach (var budget in budgets)
            {
                Console.WriteLine(budget);
            }
        }

        public decimal GetAverageMonthlyBudgetFromAnnual()
        {
            if (!budgets.Any()) return 0;

            decimal total = budgets.Sum(b => b.GetMonthlyBudget(true));
            return total / budgets.Count;
        }

        public decimal GetTotalMonthlyCategoryBudget(string category)
        {
            return budgets.Sum(b => b.GetMonthlyCategoryBudget(category));
        }

        public decimal GetTotalYearlyCategoryBudget(string category)
        {
            return budgets.Sum(b => b.GetYearlyCategoryBudget(category));
        }
    }
}