using System;

public static class TransactionEditor
{
    // fieldName: "name", "amount", "date", "category", "notes" (case-insensitive)
    // newValue is a string which will be parsed/used depending on the field.
    // Returns the updated Transaction.
    public static Transaction EditField(TransactionBook book, int index, string fieldName, string newValue)
    {
        if (book == null) throw new ArgumentNullException(nameof(book));
        var list = book.ListTransactions();
        if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));

        var old = list[index];
        var name = old.Name;
        var amount = old.Amount;
        var date = old.Date;
        var category = old.Category;
        var notes = old.Notes;

        switch (fieldName?.Trim().ToLowerInvariant())
        {
            case "name":
                name = newValue ?? string.Empty;
                break;
            case "amount":
                if (!decimal.TryParse(newValue, out amount)) throw new ArgumentException("Invalid amount", nameof(newValue));
                break;
            case "date":
                if (!DateTime.TryParse(newValue, out date)) throw new ArgumentException("Invalid date", nameof(newValue));
                break;
            case "category":
                category = newValue ?? string.Empty;
                break;
            case "notes":
                notes = newValue ?? string.Empty;
                break;
            default:
                throw new ArgumentException("Unknown field", nameof(fieldName));
        }

        var updated = Transaction.Create(name, amount, date, category, notes);
        book.UpdateTransaction(index, updated);
        return updated;
    }

    // Remove a transaction at the given index from the book and return the removed transaction.
    public static Transaction DeleteTransaction(TransactionBook book, int index)
    {
        if (book == null) throw new ArgumentNullException(nameof(book));
        // Delegate to TransactionBook.RemoveTransaction which adjusts totals
        return book.RemoveTransaction(index);
    }
}
