namespace Todolist;

public class TodoService
{
    private readonly ITaskRepository _repository;
    private List<TodoModel> _tasks = new();
    
    public TodoService(ITaskRepository repository)
    {
        _repository = repository;
    }

    // копия списка
    public List<TodoModel> GetAllTasks()
    {
        return new List<TodoModel>(_tasks);
    }

    public void LoadTasks()
    {
        _tasks = _repository.Load();
    }
    
    public void SaveTasks()
    {
        _repository.Save(_tasks);
    }

    public void AddTask(TodoModel task)
    {
        if (task == null) throw new ArgumentNullException(nameof(task), "Задача не может быть пустой");
        _tasks.Add(task);
    }
    
    public bool DeleteTask(Guid id)
    {
        // Ищем задачу по id
        TodoModel? taskToRemove = _tasks.FirstOrDefault(t => t.Id == id);
        if (taskToRemove != null)
        {
            _tasks.Remove(taskToRemove);
            return true;
        }
        
        return false;
    }

    public void UpdateTask(TodoModel updatedTask)
    {
        if (updatedTask == null) throw new ArgumentNullException(nameof(updatedTask));

        TodoModel? existingTask = _tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
        if (existingTask == null) throw new ArgumentNullException(nameof(updatedTask), "Задача не найдена");
        
        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.DueDate = updatedTask.DueDate;
        existingTask.Priority = updatedTask.Priority;
        existingTask.Status = updatedTask.Status;
    }
    
    public void ChangeStatus(Guid id, TaskStatus newStatus)
    {
        TodoModel? task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) throw new InvalidOperationException("Задача не найдена.");
        task.Status = newStatus;
    }
}