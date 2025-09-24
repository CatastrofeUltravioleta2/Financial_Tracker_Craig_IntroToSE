using System;
using System.Collections.Generic;
using FinanceApp;
namespace FinanceApp.Services
{
    // 1. Spending vs Spending
    // 2.Spending vs Budget
    public class ComparisonService
    {
        // ----- Spending vs Spending -----

        // Compare spending of a specific year-month with the previous month.
        // Example: 2025-09 vs 2025-08
        public PeriodComparisonResult CompareMonthlySpending(TransactionBook book, int year, int month)
        {
            int prevYear;
            int prevMonth;
            GetPreviousMonth(year, month, out prevYear, out prevMonth);

            decimal current = SumForMonthYear(book, year, month);

            decimal previous = SumForMonthYear(book, prevYear, prevMonth);

            PeriodComparisonResult result = new PeriodComparisonResult();
            result.CurrentTotal = current;
            result.PreviousTotal = previous;
            result.Difference = current - previous;
            result.Label = "Monthly Spending " + year + "-" + month.ToString("D2")
                         + " vs " + prevYear + "-" + prevMonth.ToString("D2");

            return result;
        }

        // Compare spending of a year with the previous year.
        // Example: 2025 vs 2024
        public PeriodComparisonResult CompareYearlySpending(TransactionBook book, int year)
        {
            decimal current = SumForYear(book, year);
            decimal previous = SumForYear(book, year - 1);

            PeriodComparisonResult result = new PeriodComparisonResult();
            result.CurrentTotal = current;
            result.PreviousTotal = previous;
            result.Difference = current - previous;
            result.Label = "Yearly Spending " + year + " vs " + (year - 1);

            return result;
        }

        // Compare spending for a specific category by month.
        // Example: Food in 2025-09 vs 2025-08
        public PeriodComparisonResult CompareMonthlySpendingByCategory(TransactionBook book, int year, int month, string category)
        {
            if (category == null)
            {
                category = string.Empty;
            }

            int prevYear;
            int prevMonth;
            GetPreviousMonth(year, month, out prevYear, out prevMonth);

            decimal current = SumForMonthYearCategory(book, year, month, category);
            decimal previous = SumForMonthYearCategory(book, prevYear, prevMonth, category);

            PeriodComparisonResult result = new PeriodComparisonResult();
            result.CurrentTotal = current;
            result.PreviousTotal = previous;
            result.Difference = current - previous;
            result.Label = "Monthly Spending (" + category + ") "
                         + year + "-" + month.ToString("D2")
                         + " vs " + prevYear + "-" + prevMonth.ToString("D2");

            return result;
        }

        // Compare spending for a specific category by year.
        // Example: Rent in 2025 vs 2024
        public PeriodComparisonResult CompareYearlySpendingByCategory(TransactionBook book, int year, string category)
        {
            if (category == null)
            {
                category = string.Empty;
            }

            decimal current = SumForYearCategory(book, year, category);
            decimal previous = SumForYearCategory(book, year - 1, category);

            PeriodComparisonResult result = new PeriodComparisonResult();
            result.CurrentTotal = current;
            result.PreviousTotal = previous;
            result.Difference = current - previous;
            result.Label = "Yearly Spending (" + category + ") " + year + " vs " + (year - 1);

            return result;
        }

        // ----- Spending vs Budget -----

        // Compare monthly spending with monthly budgets.
        // budgets can be passed from BudgetManager.GetAllBudgets().
        public BudgetCheckResult CheckMonthlyBudget(TransactionBook book, IEnumerable<Budget> budgets, int year, int month)
        {
            decimal spent = SumForMonthYear(book, year, month);
            decimal budget = SumMonthlyBudgets(budgets);

            BudgetCheckResult result = new BudgetCheckResult();
            result.Spent = spent;
            result.Budget = budget;
            result.IsOverBudget = spent > budget;
            result.Label = "Monthly Budget Check " + year + "-" + month.ToString("D2");

            return result;
        }

        // Compare yearly spending with yearly budgets.
        // If AnnualPay is set, it is used
        // otherwise MonthlyBudget*12 is used.
        public BudgetCheckResult CheckYearlyBudget(TransactionBook book, IEnumerable<Budget> budgets, int year)
        {
            decimal spent = SumForYear(book, year);
            decimal budget = SumYearlyBudgets(budgets);

            BudgetCheckResult result = new BudgetCheckResult();
            result.Spent = spent;
            result.Budget = budget;
            result.IsOverBudget = spent > budget;
            result.Label = "Yearly Budget Check " + year;

            return result;
        }

        // Date calculation

        /// Get the previous month for a given year-month.
        /// Example: 2025-01 → 2024-12
        private void GetPreviousMonth(int year, int month, out int prevYear, out int prevMonth)
        {
            if (month <= 1)
            {
                prevMonth = 12;
                prevYear = year - 1;
            }
            else
            {
                prevMonth = month - 1;
                prevYear = year;
            }
        }

        // Transaction totals

        private decimal SumForMonthYear(TransactionBook book, int year, int month)
        {
            if (book == null) return 0;

            var list = book.ListTransactions();
            decimal total = 0;

            foreach (var t in list)
            {
                if (t != null && t.Date.Year == year && t.Date.Month == month)
                {
                    total += t.Amount;
                }
            }
            return total;
        }

        private decimal SumForYear(TransactionBook book, int year)
        {
            if (book == null) return 0;

            var list = book.ListTransactions();
            decimal total = 0;

            foreach (var t in list)
            {
                if (t != null && t.Date.Year == year)
                {
                    total += t.Amount;
                }
            }
            return total;
        }

        private decimal SumForMonthYearCategory(TransactionBook book, int year, int month, string category)
        {
            if (book == null) return 0;
            if (category == null) category = string.Empty;

            var list = book.ListTransactions();
            decimal total = 0;

            foreach (var t in list)
            {
                if (t != null && t.Date.Year == year && t.Date.Month == month && t.Category == category)
                {
                    total += t.Amount;
                }
            }
            return total;
        }

        private decimal SumForYearCategory(TransactionBook book, int year, string category)
        {
            if (book == null) return 0;
            if (category == null) category = string.Empty;

            var list = book.ListTransactions();
            decimal total = 0;

            foreach (var t in list)
            {
                if (t != null && t.Date.Year == year && t.Category == category)
                {
                    total += t.Amount;
                }
            }
            return total;
        }

        // Budget totals

        private decimal SumMonthlyBudgets(IEnumerable<Budget> budgets)
        {
            if (budgets == null) return 0;

            decimal total = 0;
            foreach (var b in budgets)
            {
                if (b == null) continue;

                decimal monthly;
                if (b.AnnualPay > 0)
                {
                    monthly = b.AnnualPay / 12m;
                }
                else
                {
                    monthly = b.MonthlyBudget;
                }

                total += monthly;
            }
            return total;
        }

        private decimal SumYearlyBudgets(IEnumerable<Budget> budgets)
        {
            if (budgets == null) return 0;

            decimal total = 0;
            foreach (var b in budgets)
            {
                if (b == null) continue;

                decimal yearly;
                if (b.AnnualPay > 0)
                {
                    yearly = b.AnnualPay;
                }
                else
                {
                    yearly = b.MonthlyBudget * 12m;
                }

                total += yearly;
            }
            return total;
        }
    }
}
