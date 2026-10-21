namespace Todolist;

public interface ITaskRepository
{
    void Save(List<TodoModel> tasks);
    
    List<TodoModel> Load();
}