using System;

public class Program
{
    //Console testing
    public static void Main()
    {

        var book = new TransactionBook();
        book.AddTransaction(new Transaction(new Guid(), "test", "Paycheck", 2000.00m, new DateTime(2025, 9, 1), "Income", "September pay"));
        book.AddTransaction(new Transaction(new Guid(), "test", "Groceries", -150.25m, new DateTime(2025, 9, 5), "Food", "Weekly shopping"));

        while (true)
        {
            Console.WriteLine("\nTransactions:");
            foreach (var t in book.ListTransactions())
                Console.WriteLine(t.ToString());
            Console.WriteLine($"Total amount: ${book.TotalAmount:N2}\n");

            Console.WriteLine("Options: [E]dit transaction, [D]elete transaction, [Q]uit");
            Console.Write("Choice: ");
            var choice = Console.ReadLine()?.Trim().ToUpperInvariant() ?? "";
            if (choice == "Q") return;
            if (choice == "E")
            {
                var list = book.ListTransactions();
                if (list.Count == 0)
                {
                    Console.WriteLine("No transactions to edit.");
                    continue;
                }

                Console.WriteLine("Select index to edit:");
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine($"{i}: {list[i]}");

                Console.Write("Index (or C to cancel): ");
                var idxRaw = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(idxRaw)) { Console.WriteLine("No index provided."); continue; }
                if (idxRaw.Trim().Equals("C", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("Edit cancelled."); continue; }
                if (!int.TryParse(idxRaw, out var idx) || idx < 0 || idx >= list.Count)
                {
                    Console.WriteLine("Invalid index.");
                    continue;
                }

                Console.WriteLine("Fields: name, amount, date, category, notes");
                Console.Write("Field to edit (or C to cancel): ");
                var field = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(field)) { Console.WriteLine("No field provided."); continue; }
                if (field.Trim().Equals("C", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("Edit cancelled."); continue; }

                Console.Write("New value (or C to cancel): ");
                var newValue = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newValue)) { Console.WriteLine("No value provided."); continue; }
                if (newValue.Trim().Equals("C", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("Edit cancelled."); continue; }

                try
                {
                    var updated = TransactionEditor.EditField(book, idx, field, newValue);
                    Console.WriteLine($"Transaction updated: {updated}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Edit failed: {ex.Message}");
                }
            }
            else if (choice == "D")
            {
                var list = book.ListTransactions();
                if (list.Count == 0)
                {
                    Console.WriteLine("No transactions to delete.");
                    continue;
                }

                Console.WriteLine("Select index to delete:");
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine($"{i}: {list[i]}");

                Console.Write("Index (or C to cancel): ");
                var idxRaw = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(idxRaw)) { Console.WriteLine("No index provided."); continue; }
                if (idxRaw.Trim().Equals("C", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("Delete cancelled."); continue; }
                if (!int.TryParse(idxRaw, out var idx) || idx < 0 || idx >= list.Count)
                {
                    Console.WriteLine("Invalid index.");
                    continue;
                }

                try
                {
                    var removed = TransactionEditor.DeleteTransaction(book, idx);
                    Console.WriteLine($"Removed: {removed}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Delete failed: {ex.Message}");
                }
            }
        }
    }
}
