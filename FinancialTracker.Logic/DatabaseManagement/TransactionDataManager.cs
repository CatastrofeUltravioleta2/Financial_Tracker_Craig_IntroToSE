using SQLite;
public class SQLTransactionData
{
    private readonly string dataSource = "transactionDataDB.sqlite";
    public SQLTransactionData()
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        conn.CreateTable<TransactionData>();
        conn.Close();
    }
    public Dictionary<int, (string, string, decimal, DateTime, string, string)> GetTransactionData()
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        Dictionary<int, (string, string, decimal, DateTime, string, string)> TransactionDataDictionary = conn.Table<TransactionData>().ToDictionary(row => row.TransactionId, row => (row.TransactionOwner, row.Name, row.Amount, row.Date, row.Category, row.Notes));
        conn.Close();
        return TransactionDataDictionary;
    }
    public void SaveTransactionData(Dictionary<int, (string, string, decimal, DateTime, string, string)> newTransactionData)
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        foreach (var Item in newTransactionData)
        {
            var record = new TransactionData { TransactionId = Item.Key, TransactionOwner = Item.Value.Item1, Name = Item.Value.Item2, Amount = Item.Value.Item3, Date = Item.Value.Item4, Category = Item.Value.Item5, Notes = Item.Value.Item6 };
            if (conn.Table<TransactionData>().FirstOrDefault(tr => tr.TransactionId == Item.Key) == null)
            {
                var results = conn.Insert(record);
            }
            else
            {
                conn.Update(record);
            }
        }
        conn.Close();
    }
    public Dictionary<int, (string, string, decimal, DateTime, string, string)>  GetTransactionByUser(string username)
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        var userTransactions = conn.Table<TransactionData>().Where(tr => tr.TransactionOwner == username).ToDictionary(row => row.TransactionId, row => (row.TransactionOwner, row.Name, row.Amount, row.Date, row.Category, row.Notes));
        conn.Close();
        return userTransactions;
    }
}