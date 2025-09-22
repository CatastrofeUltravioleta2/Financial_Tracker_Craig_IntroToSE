namespace FinancialTracker.Logic;

public class Transaction
{
    public DateTime Date { get; set; }
    public decimal price { get; set; }
    public int Amount { get; set; }
    public string Category { get; set; }

    public Transaction(DateTime date, string description, decimal amount, string category)
    {
        Date = date;
        Price = price;
        Amount = amount;
        Category = category;

        public decimal Total => Price * Amount;
    }
}