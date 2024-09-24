using System;
using System.Threading.Tasks;

class Program
{
    static char[] letters = { 'C', 's', 'h', 'a', 'r', 'p' };

    static async Task PrintLetterAsync(char letter, int delay)
    {
        await Task.Delay(delay); // імітація затримки
        Console.Write(letter);
    }

    static async Task Main(string[] args)
    {
        Task[] tasks = new Task[letters.Length];
        for (int i = 0; i < letters.Length; i++)
        {
            int delay = i * 500; // час затримки
            tasks[i] = PrintLetterAsync(letters[i], delay);
        }

        await Task.WhenAll(tasks); // чекаємо виконання всіх потоків

        Console.ReadLine();
    }
}
