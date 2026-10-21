using System.IO;
using System.Text.Json;
namespace Todolist;

public class JsonTaskRepository : ITaskRepository
{
    // путь к файлу
    private readonly string _filePath;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true // отступы и переносы
    };
    
    // если путь не передали
    public JsonTaskRepository(string filePath = "tasks.json")
    {
        _filePath = filePath;
    }

    public void Save(List<TodoModel> tasks)
    {
        string json = JsonSerializer.Serialize(tasks, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }

    public List<TodoModel> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<TodoModel>();
        }

        string json = File.ReadAllText(_filePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<TodoModel>();
        }
        
        // ? чтобы при сломаном файле вернуть пустой список
        List<TodoModel>? tasks = JsonSerializer.Deserialize<List<TodoModel>>(json, _jsonOptions);
        return tasks ?? new List<TodoModel>();
    }
}