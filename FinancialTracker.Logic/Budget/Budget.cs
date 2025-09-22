public class Budget
{
    public decimal AnnualPay { get; set; }
    public decimal MonthlyBudget { get; set; }
    public Dictionary<string, decimal> CategoryBudgets { get; set; } = new();

    public decimal GetMonthlyBudget()
    {
        if (AnnualPay > 0)
            return AnnualPay / 12;
        return MonthlyBudget;
    }

    public void SetCategoryBudget(string category, decimal amount)
    {
        CategoryBudgets[category] = amount;
    }

    public decimal GetCategoryBudget(string category)
    {
        return CategoryBudgets.ContainsKey(category) ? CategoryBudgets[category] : 0;
    }
}
