using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void ThreadTask(object obj)
    {
        (int threadId, ThreadPriority priority) = ((int, ThreadPriority))obj;
        Stopwatch stopwatch = Stopwatch.StartNew();
        long count = 0;

        Console.WriteLine($"Потік {threadId} (Пріоритет: {priority}): стартував");

        while (stopwatch.ElapsedMilliseconds < 5000) // Працювати 5 секунд
        {
            count++;
            if (count % 10000000 == 0) // Виводити стан потоку кожні 10 млн ітерацій
            {
                Console.WriteLine($"Потік {threadId} (Пріоритет: {priority}): працює...");
            }
        }
        Console.WriteLine($"Потік {threadId} (Пріоритет: {priority}): завершено, виконано ітерацій: {count}");
    }

    static void RunThreads(ThreadPriority[] priorities)
    {
        Thread[] threads = new Thread[priorities.Length];
        for (int i = 0; i < priorities.Length; i++)
        {
            threads[i] = new Thread(ThreadTask);
            threads[i].Priority = priorities[i];
            threads[i].Start((i + 1, priorities[i]));
        }
        foreach (var thread in threads)
        {
            thread.Join();
        }
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Варіант 2: П'ять потоків з різними пріоритетами");
        ThreadPriority[] prioritiesV2 = { ThreadPriority.Highest, ThreadPriority.Lowest, ThreadPriority.AboveNormal, ThreadPriority.Normal, ThreadPriority.BelowNormal };
        RunThreads(prioritiesV2);

        Console.WriteLine("Натисніть Enter для виходу...");
        Console.ReadLine();
    }
}
