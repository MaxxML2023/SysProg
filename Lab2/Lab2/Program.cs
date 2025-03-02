using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Main Thread is starting.");

        // Завдання 1: Дві задачі, що виконуються паралельно з пропорційною затримкою
        Task tsk1 = new Task(() => MyTask(1));
        Task tsk2 = new Task(() => MyTask(2));
        tsk1.Start();
        tsk2.Start();

        // Завдання 2: Очікування виконання задач
        Task.WaitAll(tsk1, tsk2);
        Console.WriteLine("Both tasks completed.");

        // Завдання 3: Використання лямбда-виразу
        Task tsk3 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Lambda task is started.");
            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine($"Lambda task counter = {count}");
            }
            Console.WriteLine("Lambda task is done.");
        });
        tsk3.Wait();

        // Завдання 4: Паралельні обчислення за допомогою Invoke() з лямбда-виразами
        Parallel.Invoke(
            () => {
                Console.WriteLine("First lambda task started.");
                Thread.Sleep(1000);
                Console.WriteLine("First lambda task completed.");
            },
            () => {
                Console.WriteLine("Second lambda task started.");
                Thread.Sleep(1200);
                Console.WriteLine("Second lambda task completed.");
            }
        );

        Console.WriteLine("Main() is done.");

        // Затримка перед закриттям програми
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void MyTask(int id)
    {
        Console.WriteLine($"Task {id} started.");
        for (int count = 0; count < 5; count++)
        {
            Thread.Sleep(500 * id);
            Console.WriteLine($"Task {id} counter = {count}");
        }
        Console.WriteLine($"Task {id} is done.");
    }
}
