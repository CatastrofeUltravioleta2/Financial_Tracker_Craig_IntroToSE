using System;

public class Transaction
{
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    // Method creating a Transaction
    public static Transaction Create(string name, decimal amount, DateTime date, string category, string notes)
    {
        return new Transaction
        {
            Name = name ?? string.Empty,
            Amount = amount,
            Date = date,
            Category = category ?? string.Empty,
            Notes = notes ?? string.Empty
        };
    }

    public override string ToString()
    {
    return $"{Name} | {Date:MM/dd/yyyy} | ${Amount:N2} | {Category} | {Notes}";
    }
}

public class TransactionBook
{
    private readonly List<Transaction> _transactions = new();
    private decimal _totalAmount = 0.0m;

    // Add transactions and update total
    public void AddTransaction(Transaction tx)
    {
        if (tx == null) throw new ArgumentNullException(nameof(tx));
        _transactions.Add(tx);
    _totalAmount += tx.Amount;
    }

    // Return read-only view of transactions
    public IReadOnlyList<Transaction> ListTransactions() => _transactions.AsReadOnly();

    // Replace transaction at index and adjust running total
    public void UpdateTransaction(int index, Transaction newTx)
    {
        if (newTx == null) throw new ArgumentNullException(nameof(newTx));
        if (index < 0 || index >= _transactions.Count) throw new ArgumentOutOfRangeException(nameof(index));

        var old = _transactions[index];
        _transactions[index] = newTx;
        // adjust total
        _totalAmount = _totalAmount - old.Amount + newTx.Amount;
    }

    // Total amount across all transactions
    public decimal TotalAmount => _totalAmount;

    // Self destruct
    public void Clear()
    {
        _transactions.Clear();
    _totalAmount = 0.0m;
    }

    // Remove the transaction at the specified index and update total
    // Returns the removed transaction
    public Transaction RemoveTransaction(int index)
    {
        if (index < 0 || index >= _transactions.Count) throw new ArgumentOutOfRangeException(nameof(index));
        var old = _transactions[index];
        _transactions.RemoveAt(index);
        _totalAmount -= old.Amount;
        return old;
    }
}
