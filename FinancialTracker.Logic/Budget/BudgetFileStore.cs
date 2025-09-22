using System.IO;
using System.Text.Json;

namespace FinancialTracker.Logic;

public interface IBudgetStore
{
    void Save(Budget data);
    Budget Load();
}

public class FileBudgetStore : IBudgetStore
{
    private readonly string _path;
    public FileBudgetStore(string path)
    {
        _path = path;
        var dir = Path.GetDirectoryName(_path);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    public void Save(Budget data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_path, json);
    }

    public Budget Load()
    {
        if (!File.Exists(_path))
            return new Budget();
        var json = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<Budget>(json) ?? new Budget();
    }
}
