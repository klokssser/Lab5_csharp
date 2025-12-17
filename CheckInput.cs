internal class CheckInput
{
    public int ReadInt(string input)
    {
        int number;
        Console.Write(input);
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Ошибка: Введите корректное целое число.");
            Console.Write(input);
        }
        return number;
    }

    public int ReadPositiveInt(string input)
    {
        int number;
        while (true)
        {
            number = ReadInt(input);
            if (number > 0)
            {
                return number;
            }
            Console.WriteLine("Ошибка: Число должно быть положительным.");
        }
    }

    public double ReadDouble(string input)
    {
        double number;
        Console.Write(input);
        while (!double.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Ошибка: Введите корректное дробное число.");
            Console.Write(input);
        }
        return number;
    }

    public long ReadLong(string input)
    {
        long number;
        Console.Write(input);
        while (!long.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Ошибка: Введите корректное длинное целое число.");
            Console.Write(input);
        }
        return number;
    }

    public char ReadChar(string input)
    {
        char character;
        Console.Write(input);
        while (!char.TryParse(Console.ReadLine(), out character))
        {
            Console.WriteLine("Ошибка: Введите один символ.");
            Console.Write(input);
        }
        return character;
    }

    public int[] ReadArray(string input)
    {
        Console.WriteLine(input);
        Console.Write("Введите элементы массива через пробел: ");

        string? line = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(line))
        {
            return new int[0];
        }

        string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int[] array = new int[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            if (!int.TryParse(parts[i], out array[i]))
            {
                Console.WriteLine($"Ошибка: '{parts[i]}' не является корректным числом. Элемент будет заменен на 0.");
                array[i] = 0;
            }
        }

        return array;
    }
}
