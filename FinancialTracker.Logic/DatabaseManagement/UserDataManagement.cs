using SQLite;
public class SQLuserData
{
    private readonly string dataSource = "userDataDB.sqlite";
    public SQLuserData()
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        conn.CreateTable<UserData>();
        conn.Close();
    }
    public Dictionary<string, string> GetUserData()
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        Dictionary<string, string> UserDataDictionary = conn.Table<UserData>().ToDictionary(row => row.Username, row => row.Password);
        conn.Close();
        return UserDataDictionary;
    }
    public void SaveUserData(Dictionary<string, string> newUserData)
    {
        var options = new SQLiteConnectionString(dataSource, false);
        var conn = new SQLiteConnection(options);
        foreach (var Item in newUserData)
        {
            var record = new UserData { Username = Item.Key, Password = Item.Value};
            if (conn.Table<UserData>().FirstOrDefault(score => score.Username == Item.Key) == null)
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
}