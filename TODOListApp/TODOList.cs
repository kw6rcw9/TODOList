namespace ConsoleApp1;

public class TODOList
{
    private List<Task> _tasks = new ();
    private const string _fileName = "tasks.txt";

    public TODOList()
    {
        LoadTasks();
    }

    // Загрузка задач из файла
    private void LoadTasks()
    {
        if (File.Exists(_fileName))
        {
            var lines = File.ReadAllLines(_fileName);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && Enum.TryParse(parts[0], out Priority priority))
                {
                    _tasks.Add(new Task(parts[1], priority));
                }
            }
        }
    }

    // Сохранение задач в файл
    private void SaveTasks()
    {
        var lines = _tasks.Select(t => $"{(int)t.TaskPriority}|{t.Description}");
        File.WriteAllLines(_fileName, lines);
    }

    // Добавление задачи
    public void CreatTask()
    {
        Console.Write("Введите описание задачи: ");
        string description = Console.ReadLine();

        Console.WriteLine("Укажите приоритет (1 - High, 2 - Mid, 3 - Low): ");
        if (int.TryParse(Console.ReadLine(), out int priorityValue) && Enum.IsDefined(typeof(Priority), priorityValue))
        {
            Priority priority = (Priority)priorityValue;
            _tasks.Add(new Task(description, priority));
            Console.WriteLine("Задача добавлена.");
        }
        else
        {
            Console.WriteLine("Неверный приоритет.");
        }
    }

    // Удаление задачи
    public void RemoveTask()
    {
        Console.Write("Введите номер задачи для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= _tasks.Count)
        {
            _tasks.RemoveAt(taskNumber - 1);
            Console.WriteLine("Задача удалена.");
        }
        else
        {
            Console.WriteLine("Неверный номер задачи.");
        }
    }

    // Просмотр списка задач
    public void ViewTasks()
    {
        if (_tasks.Count == 0)
        {
            Console.WriteLine("Список задач пуст.");
        }
        else
        {
            Console.WriteLine("Список задач:");
            for (int i = 0; i < _tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_tasks[i]}");
            }
        }
    }

    // Фильтрация задач по приоритету
    public void FilterTasksByPriority()
    {
        Console.WriteLine("Введите приоритет для фильтрации (1 - Высокий, 2 - Средний, 3 - Низкий): ");
        if (int.TryParse(Console.ReadLine(), out int priorityValue) && Enum.IsDefined(typeof(Priority), priorityValue))
        {
            Priority priority = (Priority)priorityValue;
            var filteredTasks = _tasks.Where(t => t.TaskPriority == priority).ToList();

            if (filteredTasks.Any())
            {
                Console.WriteLine($"Задачи с приоритетом [{priority}]:");
                for (int i = 0; i < filteredTasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {filteredTasks[i]}");
                }
            }
            else
            {
                Console.WriteLine($"Нет задач с приоритетом [{priority}].");
            }
        }
        else
        {
            Console.WriteLine("Неверный приоритет.");
        }
    }

    // Поиск задач по ключевому слову
    public void SearchTasks()
    {
        Console.Write("Введите ключевое слово для поиска: ");
        string keyword = Console.ReadLine().ToLower();

        var foundTasks = _tasks.Where(t => t.Description.ToLower().Contains(keyword)).ToList();

        if (foundTasks.Any())
        {
            Console.WriteLine("Найденные задачи:");
            for (int i = 0; i < foundTasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {foundTasks[i]}");
            }
        }
        else
        {
            Console.WriteLine("Задачи не найдены.");
        }
    }

    // Основной метод меню
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Удалить задачу");
            Console.WriteLine("3. Просмотреть все задачи");
            Console.WriteLine("4. Фильтровать задачи по приоритету");
            Console.WriteLine("5. Поиск задач");
            Console.WriteLine("6. Выход");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreatTask();
                    break;
                case "2":
                    RemoveTask();
                    break;
                case "3":
                    ViewTasks();
                    break;
                case "4":
                    FilterTasksByPriority();
                    break;
                case "5":
                    SearchTasks();
                    break;
                case "6":
                    SaveTasks();
                    Console.WriteLine("До свидания!");
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
}

