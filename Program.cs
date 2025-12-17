using lab5_C_;

internal class Program
{
    private static HermitageService _service = new HermitageService();
    private static CheckInput _input = new CheckInput();

    static void Main(string[] args)
    {
        _service.LoadDatabase();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Меню базы данных Эрмитажа ---");
            Console.WriteLine("1. Просмотр всех данных");
            Console.WriteLine("2. Добавить художника");
            Console.WriteLine("3. Добавить стиль");
            Console.WriteLine("4. Добавить картину");
            Console.WriteLine("5. Удалить картину");
            Console.WriteLine("6. Запрос 1: Картины в определенной части музея (Таблица: Картины)");
            Console.WriteLine("7. Запрос 2: Картины определенного художника (Таблицы: Картины, Художники)");
            Console.WriteLine("8. Запрос 3: Количество художников с > N картин в части M (Все таблицы)");
            Console.WriteLine("9. Запрос 4: Информация о самой старой картине (Все таблицы)");
            Console.WriteLine("10. Сохранить изменения");
            Console.WriteLine("0. Выход");

            int choice = _input.ReadInt("Выберите действие: ");

            switch (choice)
            {
                case 1:
                    ViewAllData();
                    break;
                case 2:
                    AddArtist();
                    break;
                case 3:
                    AddStyle();
                    break;
                case 4:
                    AddPainting();
                    break;
                case 5:
                    DeletePainting();
                    break;
                case 6:
                    RunQuery1();
                    break;
                case 7:
                    RunQuery2();
                    break;
                case 8:
                    RunQuery3();
                    break;
                case 9:
                    RunQuery4();
                    break;
                case 10:
                    _service.SaveDatabase();
                    Console.WriteLine("Данные сохранены.");
                    break;
                case 0:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    private static void ViewAllData()
    {
        Console.WriteLine("\n--- Художники ---");
        foreach (var item in _service.GetAllArtists())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\n--- Стили ---");
        foreach (var item in _service.GetAllStyles())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\n--- Картины ---");
        foreach (var item in _service.GetAllPaintings())
        {
            Console.WriteLine(item);
        }
    }

    private static void AddArtist()
    {
        Console.Write("Введите имя художника: ");
        string name = Console.ReadLine() ?? "Неизвестно";
        _service.AddArtist(name);
        Console.WriteLine("Художник добавлен.");
    }

    private static void AddStyle()
    {
        Console.Write("Введите название стиля: ");
        string name = Console.ReadLine() ?? "Неизвестно";
        _service.AddStyle(name);
        Console.WriteLine("Стиль добавлен.");
    }

    private static void AddPainting()
    {
        Console.Write("Введите название картины: ");
        string name = Console.ReadLine() ?? "Неизвестно";

        int artistId = _input.ReadInt("Введите ID художника: ");
        if (!_service.ArtistExists(artistId))
        {
            Console.WriteLine("Художник с таким ID не найден.");
            return;
        }

        int styleId = _input.ReadInt("Введите ID стиля: ");
        if (!_service.StyleExists(styleId))
        {
            Console.WriteLine("Стиль с таким ID не найден.");
            return;
        }

        int year = _input.ReadInt("Введите год создания: ");
        int part = _input.ReadInt("Введите номер части Эрмитажа: ");

        _service.AddPainting(name, artistId, styleId, year, part);
        Console.WriteLine("Картина добавлена.");
    }

    private static void DeletePainting()
    {
        int id = _input.ReadInt("Введите ID картины для удаления: ");
        if (_service.RemovePainting(id))
        {
            Console.WriteLine("Картина удалена.");
        }
        else
        {
            Console.WriteLine("Картина не найдена.");
        }
    }

    private static void RunQuery1()
    {
        int part = _input.ReadInt("Введите номер части Эрмитажа: ");
        var result = _service.Query1_GetPaintingsInPart(part);
        Console.WriteLine($"Найдено {result.Count} картин:");
        foreach (var line in result)
        {
            Console.WriteLine(line);
        }
    }

    private static void RunQuery2()
    {
        Console.Write("Введите имя художника (или часть): ");
        string name = Console.ReadLine() ?? "";
        var result = _service.Query2_GetPaintingsByArtist(name);
        Console.WriteLine($"Найдено {result.Count} картин:");
        foreach (var line in result)
        {
            Console.WriteLine(line);
        }
    }

    private static void RunQuery3()
    {
        int minCount = 5;
        int part = 2;
        Console.WriteLine($"Запрос: Количество художников, у которых более {minCount} картин во {part}-й части Эрмитажа.");
        int count = _service.Query3_CountArtistsWithPaintingsInPart(minCount, part);
        Console.WriteLine($"Ответ: {count}");
    }

    private static void RunQuery4()
    {
        Console.WriteLine("Запрос: Информация о самой старой картине в базе.");
        string info = _service.Query4_OldestPaintingInfo();
        Console.WriteLine(info);
    }
}
