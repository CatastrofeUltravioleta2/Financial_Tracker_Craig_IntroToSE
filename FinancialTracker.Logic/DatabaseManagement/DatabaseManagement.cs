using SQLite;

public class UserData
{
    [PrimaryKey]
    public string Username { get; init; }
    public string Password { get; init; }
}

public class TransactionData
{
    [PrimaryKey]
    public Guid TransactionId { get; init; }
    public string TransactionOwner { get; init; }
    public string Name { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public string Category { get; init; }
    public string Notes { get; init; }
}

public class BudgetData
{
    [PrimaryKey]
    public Guid BudgetId { get; init; }
    public string Owner { get; init; }
    public string Mode { get; init; }
    public decimal TotalAmount { get; init; }
    public decimal DeclaredIncomeForPeriod { get; init; }
    public DateTime PeriodStart { get; init; }
    public DateTime PeriodEnd { get; init; }
}

public class SubBudgetData
{
    [PrimaryKey]
    public Guid Id { get; init; }
    public Guid ParentId { get; init; }
    public string Category { get; init; }
    public decimal Amount { get; init; }
    public decimal IncomeShare { get; init; }
}