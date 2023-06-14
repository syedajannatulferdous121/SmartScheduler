using System;
using System.Collections.Generic;
using System.Linq;

class Task
{
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsHighPriority { get; set; }

    public Task(string description, DateTime dueDate, bool isHighPriority)
    {
        Description = description;
        DueDate = dueDate;
        IsCompleted = false;
        IsHighPriority = isHighPriority;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
        Console.WriteLine($"Task '{Description}' has been marked as completed.");
    }

    public override string ToString()
    {
        string status = IsCompleted ? "Completed" : "Pending";
        string priority = IsHighPriority ? "High Priority" : "Normal Priority";
        return $"[Due: {DueDate.ToString("dd MMM yyyy")}] {Description} - {priority} - {status}";
    }
}

class Scheduler
{
    private List<Task> tasks;

    public Scheduler()
    {
        tasks = new List<Task>();
    }

    public void AddTask(string description, DateTime dueDate, bool isHighPriority)
    {
        Task task = new Task(description, dueDate, isHighPriority);
        tasks.Add(task);
        Console.WriteLine($"Task '{description}' added to the schedule.");
    }

    public void ViewTasks()
    {
        Console.WriteLine("Scheduled Tasks:");
        foreach (Task task in tasks)
        {
            Console.WriteLine(task);
        }
    }

    public void MarkTaskAsCompleted(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            Task task = tasks[index];
            task.MarkAsCompleted();
        }
        else
        {
            Console.WriteLine("Invalid task index.");
        }
    }

    public void ViewPendingTasks()
    {
        Console.WriteLine("Pending Tasks:");
        foreach (Task task in tasks)
        {
            if (!task.IsCompleted)
            {
                Console.WriteLine(task);
            }
        }
    }

    public void ViewHighPriorityTasks()
    {
        Console.WriteLine("High Priority Tasks:");
        foreach (Task task in tasks)
        {
            if (task.IsHighPriority)
            {
                Console.WriteLine(task);
            }
        }
    }

    public void ViewOverdueTasks()
    {
        DateTime currentDate = DateTime.Now.Date;
        Console.WriteLine("Overdue Tasks:");
        foreach (Task task in tasks)
        {
            if (!task.IsCompleted && task.DueDate < currentDate)
            {
                Console.WriteLine(task);
            }
        }
    }

    public void SortTasksByDueDate()
    {
        tasks = tasks.OrderBy(t => t.DueDate).ToList();
        Console.WriteLine("Tasks sorted by due date.");
    }

    public void SortTasksByPriority()
    {
        tasks = tasks.OrderByDescending(t => t.IsHighPriority).ToList();
        Console.WriteLine("Tasks sorted by priority.");
    }

    public void EditTask(int index, string newDescription, DateTime newDueDate, bool newPriority)
    {
        if (index >= 0 && index < tasks.Count)
        {
            Task task = tasks[index];
            task.Description = newDescription;
            task.DueDate = newDueDate;
            task.IsHighPriority = newPriority;
            Console.WriteLine("Task updated successfully.");
        }
        else
        {
            Console.WriteLine("Invalid task index.");
        }
    }

    public void DeleteTask(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            Task task = tasks[index];
            tasks.Remove(task);
            Console.WriteLine("Task deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid task index.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Scheduler scheduler = new Scheduler();

        while (true)
        {
            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. View Pending Tasks");
            Console.WriteLine("5. View High Priority Tasks");
            Console.WriteLine("6. View Overdue Tasks");
            Console.WriteLine("7. Sort Tasks by Due Date");
            Console.WriteLine("8. Sort Tasks by Priority");
            Console.WriteLine("9. Edit Task");
            Console.WriteLine("10. Delete Task");
            Console.WriteLine("11. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter the task description: ");
                    string description = Console.ReadLine();
                    Console.Write("Enter the due date (dd/mm/yyyy): ");
                    DateTime dueDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                    Console.Write("Is it a high priority task? (y/n): ");
                    bool isHighPriority = Console.ReadLine().ToLower() == "y";
                    scheduler.AddTask(description, dueDate, isHighPriority);
                    break;
                case "2":
                    scheduler.ViewTasks();
                    break;
                case "3":
                    Console.Write("Enter the index of the task to mark as completed: ");
                    int index = int.Parse(Console.ReadLine());
                    scheduler.MarkTaskAsCompleted(index);
                    break;
                case "4":
                    scheduler.ViewPendingTasks();
                    break;
                case "5":
                    scheduler.ViewHighPriorityTasks();
                    break;
                case "6":
                    scheduler.ViewOverdueTasks();
                    break;
                case "7":
                    scheduler.SortTasksByDueDate();
                    break;
                case "8":
                    scheduler.SortTasksByPriority();
                    break;
                case "9":
                    Console.Write("Enter the index of the task to edit: ");
                    int editIndex = int.Parse(Console.ReadLine());
                    Console.Write("Enter the new task description: ");
                    string newDescription = Console.ReadLine();
                    Console.Write("Enter the new due date (dd/mm/yyyy): ");
                    DateTime newDueDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                    Console.Write("Is it a new high priority task? (y/n): ");
                    bool newPriority = Console.ReadLine().ToLower() == "y";
                    scheduler.EditTask(editIndex, newDescription, newDueDate, newPriority);
                    break;
                case "10":
                    Console.Write("Enter the index of the task to delete: ");
                    int deleteIndex = int.Parse(Console.ReadLine());
                    scheduler.DeleteTask(deleteIndex);
                    break;
                case "11":
                    Console.WriteLine("Thank you for using SmartScheduler. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
