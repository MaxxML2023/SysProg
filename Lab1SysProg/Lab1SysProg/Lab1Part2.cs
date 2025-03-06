using System;
using System.Diagnostics;
using System.Threading;

class Lab1Part2
{
    static long[] iterationCounts; 

    static void ThreadTask(object obj)
    {
        (int threadId, ThreadPriority priority) = ((int, ThreadPriority))obj;
        Stopwatch stopwatch = Stopwatch.StartNew();
        long count = 0;

        Console.WriteLine($"Потік {threadId} (Пріоритет: {priority}): стартував");

        while (stopwatch.ElapsedMilliseconds < 5000) 
        {
            count++;
            if (count % 10000000 == 0) 
            {
                Console.WriteLine($"Потік {threadId} (Пріоритет: {priority}): працює... Ітерацій: {count}");
            }
        }

        iterationCounts[threadId - 1] = count; 
        Console.WriteLine($"Потік {threadId} (Пріоритет: {priority}): завершено, виконано ітерацій: {count}");
    }

    static void RunThreads(ThreadPriority[] priorities)
    {
        int numThreads = priorities.Length;
        iterationCounts = new long[numThreads]; 
        Thread[] threads = new Thread[numThreads];

        for (int i = 0; i < numThreads; i++)
        {
            threads[i] = new Thread(ThreadTask);
            threads[i].Priority = priorities[i];
            threads[i].Start((i + 1, priorities[i]));
        }

        foreach (var thread in threads)
        {
            thread.Join(); 
        }

       
        long totalIterations = 0;
        foreach (var count in iterationCounts)
        {
            totalIterations += count;
        }

        Console.WriteLine("\n=== Розподіл процесорного часу у % ===");
        for (int i = 0; i < numThreads; i++)
        {
            double percentage = (double)iterationCounts[i] / totalIterations * 100;
            Console.WriteLine($"Потік {i + 1} (Пріоритет: {priorities[i]}): {percentage:F2}%");
        }
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Варіант 3: Чотири потоки з різними пріоритетами");
        ThreadPriority[] prioritiesV3 = { ThreadPriority.Lowest, ThreadPriority.AboveNormal, ThreadPriority.BelowNormal, ThreadPriority.Highest };
        RunThreads(prioritiesV3);

        Console.WriteLine("\nНатисніть Enter для виходу...");
        Console.ReadLine();
    }
}
