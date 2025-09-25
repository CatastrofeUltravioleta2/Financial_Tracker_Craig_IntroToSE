using System;
using System.Collections.Generic;

namespace FinanceApp
{
    public class Budget
    {
        public decimal AnnualPay { get; set; }      // annually
        public decimal MonthlyBudget { get; set; }  // monthly

        // budget per category
        public Dictionary<string, decimal> MonthlyCategoryBudgets { get; private set; } = new();
        public Dictionary<string, decimal> YearlyCategoryBudgets { get; private set; } = new();

        /// gets the monthly budget
        /// fromAnnual = true > AnnualPay / 12
        /// false > MonthlyBudget
        public decimal GetMonthlyBudget(bool fromAnnual = true)
        {
            if (fromAnnual && AnnualPay > 0)
                return AnnualPay / 12;
            return MonthlyBudget;
        }

        /// sets the category budget for month
        public void SetMonthlyCategoryBudget(string category, decimal amount)
        {
            MonthlyCategoryBudgets[category] = amount;
        }

        /// set the category budget for year
        public void SetYearlyCategoryBudget(string category, decimal amount)
        {
            YearlyCategoryBudgets[category] = amount;
        }

        /// gets the category budget for month
        public decimal GetMonthlyCategoryBudget(string category)
        {
            if (MonthlyCategoryBudgets.TryGetValue(category, out var amount))
            {
                return amount;
            }
            return 0;
        }

        /// gets the category budget for year
        public decimal GetYearlyCategoryBudget(string category)
        {
            if (YearlyCategoryBudgets.TryGetValue(category, out var amount))
            {
                return amount;
            }
            return 0;
        }

        public override string ToString()
        {
            return $"Annual Pay: {AnnualPay:C}, Monthly Budget: {MonthlyBudget:C}, " +
                   $"Categories (Monthly: {MonthlyCategoryBudgets.Count}, Yearly: {YearlyCategoryBudgets.Count})";
        }
    }
}