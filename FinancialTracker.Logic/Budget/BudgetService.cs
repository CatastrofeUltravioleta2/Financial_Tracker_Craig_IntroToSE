namespace FinancialTracker.Logic;

public class BudgetService
{
    private Budget _budget = new Budget();

    public void SetAnnualPay(decimal annualPay)
    {
        _budget.AnnualPay = annualPay;
    }

    public void SetMonthlyBudget(decimal monthly)
    {
        _budget.MonthlyBudget = monthly;
    }

    public decimal GetMonthlyBudget()
    {
        return _budget.GetMonthlyBudget();
    }

    public void SetCategoryBudget(string category, decimal amount)
    {
        _budget.SetCategoryBudget(category, amount);
    }

    public decimal GetCategoryBudget(string category)
    {
        return _budget.GetCategoryBudget(category);
    }

    public IReadOnlyDictionary<string, decimal> GetAllCategoryBudgets()
    {
        return _budget.CategoryBudgets;
    }

    private readonly IBudgetStore _store;
    public BudgetService(IBudgetStore store)
    {
        _store = store;
        _budget = _store.Load();
    }

    public void Save()
    {
        _store.Save(_budget);
    }

    public void Load()
    {
        _budget = _store.Load();
    }
}
