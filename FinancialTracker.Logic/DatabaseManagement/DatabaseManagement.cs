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
    public int TransactionId { get; init; }
    public string TransactionOwner { get; init; }
    public string Name { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public string Category { get; init; }
    public string Notes { get; init; }
}