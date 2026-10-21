namespace Todolist;

public class TodoModel
{
    // уник айди задачи
    public Guid Id { get; set; } = Guid.NewGuid();

    private string _title = string.Empty;
    private string _description = string.Empty;
    private DateTime _dueDate;

    public string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Название не может быть пустым");
            _title = value.Trim();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (value != null)
            {
                _description = value;
            }
            else
            {
                _description = string.Empty;
            }
        }
    }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime DueDate
    {
        get => _dueDate;
        set
        {
            if (value < CreatedAt.Date)
                throw new ArgumentException("Срок выполнения не может быть раньше даты создания.");
            _dueDate = value;
        }
    }
    
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
    
    public TodoModel() { }
    
    public TodoModel(string title, string description, DateTime dueDate, TaskPriority priority = TaskPriority.Medium)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        Status = TaskStatus.NotStarted;
    }
    
}