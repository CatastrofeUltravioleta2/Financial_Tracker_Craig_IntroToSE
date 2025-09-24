using System;
using Xunit;

namespace FinancialTracker.Test;

public class UnitTest1
{
    public class TransactionEditorTests
    {
        public TransactionEditorTests() { }

        [Fact]
        public void UpdateTransaction_ReplacesItemAndAdjustsTotal()
        {
            var book = new TransactionBook();
            var original = Transaction.Create("Old", 10.00m, new DateTime(2025, 1, 1), "OldCat", "OldNotes");
            book.AddTransaction(original);

            var updatedTx = Transaction.Create("New", 20.50m, new DateTime(2025, 2, 2), "NewCat", "NewNotes");
            book.UpdateTransaction(0, updatedTx);

            var item = book.ListTransactions()[0];
            Assert.Equal("New", item.Name);
            Assert.Equal(20.50m, item.Amount);
            Assert.Equal(20.50m, book.TotalAmount);
        }

        [Fact]
        public void UpdateTransaction_InvalidIndex_Throws()
        {
            var book = new TransactionBook();
            var tx = Transaction.Create("X", 1.0m, DateTime.Today, "C", "N");
            Assert.Throws<ArgumentOutOfRangeException>(() => book.UpdateTransaction(0, tx));
        }

        [Fact]
        public void EditField_Amount_Success()
        {
            var book = new TransactionBook();
            var original = Transaction.Create("Item", 5.00m, new DateTime(2025, 3, 3), "Cat", "Notes");
            book.AddTransaction(original);

            var updated = TransactionEditor.EditField(book, 0, "amount", "12.34");

            Assert.Equal(12.34m, updated.Amount);
            Assert.Equal(12.34m, book.ListTransactions()[0].Amount);
            Assert.Equal(12.34m, book.TotalAmount);
        }

        [Fact]
        public void EditField_InvalidAmount_Throws()
        {
            var book = new TransactionBook();
            var original = Transaction.Create("Item", 5.00m, DateTime.Today, "Cat", "Notes");
            book.AddTransaction(original);

            Assert.Throws<ArgumentException>(() => TransactionEditor.EditField(book, 0, "amount", "not-a-number"));
        }

        [Fact]
        public void EditField_UnknownField_Throws()
        {
            var book = new TransactionBook();
            var original = Transaction.Create("Item", 5.00m, DateTime.Today, "Cat", "Notes");
            book.AddTransaction(original);

            Assert.Throws<ArgumentException>(() => TransactionEditor.EditField(book, 0, "unknown", "x"));
        }

        [Fact]
        public void DeleteTransaction_RemovesItemAndAdjustsTotal()
        {
            var book = new TransactionBook();
            var a = Transaction.Create("A", 10.0m, DateTime.Today, "C", "N");
            var b = Transaction.Create("B", 5.0m, DateTime.Today, "C", "N");
            book.AddTransaction(a);
            book.AddTransaction(b);

            var removed = TransactionEditor.DeleteTransaction(book, 0);

            Assert.Equal("A", removed.Name);
            Assert.Equal(1, book.ListTransactions().Count);
            Assert.Equal(5.0m, book.TotalAmount);
        }

        [Fact]
        public void DeleteTransaction_InvalidIndex_Throws()
        {
            var book = new TransactionBook();
            Assert.Throws<ArgumentOutOfRangeException>(() => TransactionEditor.DeleteTransaction(book, 0));
        }
    }
}
