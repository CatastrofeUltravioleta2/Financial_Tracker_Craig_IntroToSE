using SQLite;
public class SQLBudgetData
{
    private readonly string dataSource = "budgetDataDB.sqlite";
    public SQLBudgetData()
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        conn.CreateTable<BudgetData>();
        conn.Close();
    }
    public Dictionary<Guid, (string, string, decimal, decimal, DateTime, DateTime)> GetBudgetData()
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        Dictionary<Guid, (string, string, decimal, decimal, DateTime, DateTime)> BudgetDataDictionary = conn.Table<BudgetData>().ToDictionary(row => row.BudgetId, row => (row.Owner, row.Mode, row.TotalAmount, row.DeclaredIncomeForPeriod, row.PeriodStart, row.PeriodEnd));
        conn.Close();
        return BudgetDataDictionary;
    }
    public void SaveBudgetData(Dictionary<Guid, (string, string, decimal, decimal, DateTime, DateTime)> newBudgetData)
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        foreach (var Item in newBudgetData)
        {
            var record = new BudgetData { BudgetId = Item.Key, Owner = Item.Value.Item1, Mode = Item.Value.Item2, TotalAmount = Item.Value.Item3, DeclaredIncomeForPeriod = Item.Value.Item4, PeriodStart = Item.Value.Item5, PeriodEnd = Item.Value.Item6 };
            if (conn.Table<BudgetData>().FirstOrDefault(bd => bd.BudgetId == Item.Key) == null)
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
    public Dictionary<Guid, (string, string, decimal, decimal, DateTime, DateTime)> GetBudgetByUser(string username)
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        var userBudgets = conn.Table<BudgetData>().Where(bd => bd.Owner == username).ToDictionary(row => row.BudgetId, row => (row.Owner, row.Mode, row.TotalAmount, row.DeclaredIncomeForPeriod, row.PeriodStart, row.PeriodEnd));
        conn.Close();
        return userBudgets;
    }
    public void DeleteBudget(Guid BudgetId)
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        var record = conn.Table<BudgetData>().FirstOrDefault(bd => bd.BudgetId == BudgetId);
        if (record != null)
        {
            conn.Delete(record);
        }
        conn.Close();
    }
}
public class SQLSubBudgetData
{
    private readonly string dataSource = "subbudgetDataDB.sqlite";
    public SQLSubBudgetData()
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        conn.CreateTable<SubBudgetData>();
        conn.Close();
    }
    public Dictionary<Guid, (Guid, string, decimal, decimal)> GetBudgetData()
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        Dictionary<Guid, (Guid, string, decimal, decimal)> SubBudgetDataDictionary = conn.Table<SubBudgetData>().ToDictionary(row => row.Id, row => (row.ParentId, row.Category, row.Amount, row.IncomeShare));
        conn.Close();
        return SubBudgetDataDictionary;
    }
    public void SaveSubBudgetData(Dictionary<Guid, (Guid, string, decimal, decimal)> newSubBudgetData)
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        foreach (var Item in newSubBudgetData)
        {
            var record = new SubBudgetData { Id = Item.Key, ParentId = Item.Value.Item1, Category = Item.Value.Item2, Amount = Item.Value.Item3, IncomeShare = Item.Value.Item4 };
            if (conn.Table<SubBudgetData>().FirstOrDefault(sbd => sbd.Id == Item.Key) == null)
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
    public Dictionary<Guid, (Guid, string, decimal, decimal)> GetSubBudgetByParentId(Guid parentId)
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        var parenSubBudgets = conn.Table<SubBudgetData>().Where(sbd => sbd.ParentId == parentId).ToDictionary(row => row.Id, row => (row.ParentId, row.Category, row.Amount, row.IncomeShare));
        conn.Close();
        return parenSubBudgets;
    }
    public void DeleteSubBudget(Guid SubBudgetId)
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        var record = conn.Table<SubBudgetData>().FirstOrDefault(sbd => sbd.Id == SubBudgetId);
        if (record != null)
        {
            conn.Delete(record);
        }
        conn.Close();
    }
}