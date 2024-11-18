using System;
using System.Threading;

class Program
{
    static readonly Random random = new Random();
    static readonly double[] y = new double[10];

    // Функція для генерації випадкових чисел у діапазоні (0..50)
    static void GenerateArray()
    {
        for (int i = 0; i < y.Length; i++)
        {
            y[i] = random.NextDouble() * 50; // Генерація випадкового числа в межах (0..50)
        }
    }

    // Потік T0: знаходження суми чисел більших за 15
    static void SumGreaterThan15()
    {
        double sum = 0;
        foreach (var number in y)
        {
            if (number > 15)
            {
                sum += number;
            }
        }
        Console.WriteLine($"Сума чисел бiльших за 15: {sum:F2}");
    }

    // Потік T1: знаходження добутку чисел менших за 10
    static void ProductLessThan10()
    {
        double product = 1;
        bool found = false;

        foreach (var number in y)
        {
            if (number < 10)
            {
                product *= number;
                found = true;
            }
        }

        if (found)
        {
            Console.WriteLine($"Добуток чисел менших за 10: {product:F2}");
        }
        else
        {
            Console.WriteLine("Немає чисел менших за 10.");
        }
    }

    static void Main()
    {
        // Генерація масиву
        GenerateArray();

        // Виведення масиву
        Console.WriteLine("Масив чисел:");
        foreach (var number in y)
        {
            Console.WriteLine(number);
        }

        // Створення потоків
        Thread threadT0 = new Thread(SumGreaterThan15);
        Thread threadT1 = new Thread(ProductLessThan10);

        // Запуск потоків
        threadT0.Start();
        threadT1.Start();

        // Очікування завершення потоків
        threadT0.Join();
        threadT1.Join();

        Console.WriteLine("Програма завершена.");
    }
}