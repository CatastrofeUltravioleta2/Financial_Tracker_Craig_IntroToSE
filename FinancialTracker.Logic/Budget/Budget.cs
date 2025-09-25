public enum BudgetMode
{
    Monthly,
    Yearly,
    Custom
}

public class SubBudget
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid ParentId { get; set; } = Guid.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; } = 0m;
    public decimal IncomeShare { get; set; } = 0m;

}

public class Budget
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Owner { get; set; } = string.Empty;
    public BudgetMode Mode { get; set; } = BudgetMode.Monthly;
    public decimal TotalAmount { get; set; } = 0m;
    public decimal DeclaredIncomeForPeriod { get; set; } = 0m;
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public List<SubBudget> SubBudgets { get; set; } = new List<SubBudget>();
    public int DaysInPeriod => (int)(PeriodEnd.Date - PeriodStart.Date).TotalDays + 1;
}

public class BudgetManagerEdition
{


    public void RemoveSubBudget(Budget budget, string category)
    {
        var existing = budget.SubBudgets.FirstOrDefault(s => string.Equals(s.Category, category, StringComparison.OrdinalIgnoreCase));
        budget.SubBudgets.Remove(existing);
    }
    public bool ValidateSubBudgetsAmount(Budget budget)
    {
        var sumSubs = budget.SubBudgets.Sum(s => s.Amount);
        if (sumSubs > budget.TotalAmount) // small epsilon
        {
            return false;
        }
        return true;
    }
    public bool ValidateSubBudgetsIncomeShares(Budget budget)
    {
        var sumIncomeShares = budget.SubBudgets.Sum(s => s.IncomeShare);
        if (budget.DeclaredIncomeForPeriod > 0 && sumIncomeShares > budget.DeclaredIncomeForPeriod)
        {
            return false;
        }

        return true;
    }

    public decimal GetMainSpent(Budget budget, TransactionBook allTransactions)
    {
        var filtered = allTransactions.ListTransactions().Where(t => t.Date.Date >= budget.PeriodStart.Date && t.Date.Date <= budget.PeriodEnd.Date);
        decimal sum = filtered.Sum(t => t.Amount);
        return sum;
    }

    public decimal GetSubBudgetSpent(Budget budget, SubBudget sub, TransactionBook allTransactions)
    {
        var filtered = allTransactions.ListTransactions().Where(t =>
            t.Date.Date >= budget.PeriodStart.Date &&
            t.Date.Date <= budget.PeriodEnd.Date &&
            string.Equals(t.Category, sub.Category, StringComparison.OrdinalIgnoreCase));
        return filtered.Sum(t => t.Amount);
    }

    public Budget ApplyYearlyToMonthly(Budget budget)
    {
        decimal perMonth = Math.Round(budget.TotalAmount / 12m, 2);
        var prevTotalSubs = budget.SubBudgets.Sum(s => s.Amount);
        decimal perMonthIncome = Math.Round(budget.DeclaredIncomeForPeriod / 12m, 2);
        var now = DateTime.Now;
        var _PeriodStart = new DateTime(now.Year, now.Month, 1);

        // Change to monthly
        var newBudget = new Budget()
        {
            Id = budget.Id,
            Owner = budget.Owner,
            Mode = BudgetMode.Monthly,
            PeriodStart = _PeriodStart,
            PeriodEnd = budget.PeriodStart.AddMonths(1).AddDays(-1),
            DeclaredIncomeForPeriod = perMonthIncome,
            TotalAmount = perMonth,
            SubBudgets = new List<SubBudget>()

        };
        return newBudget;
    }

    public void ResetAllToBeginning(Budget budget)
    {
        if (budget == null) return;
        budget.TotalAmount = 0m;
        budget.DeclaredIncomeForPeriod = 0m;
        budget.SubBudgets.Clear();
    }

    public void ResetSubBudgets(Budget budget)
    {
        if (budget == null) return;
        budget.SubBudgets.Clear();
    }

}
