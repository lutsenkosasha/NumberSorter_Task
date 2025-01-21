using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestTask
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = LoadConfiguration();
            string apiUrl = config["ApiUrl"];

            Random rand = new Random();
            int count = rand.Next(20, 101);
            int[] numbers = Enumerable.Range(0, count).Select(_ => rand.Next(-100, 101)).ToArray();

            Console.WriteLine("Исходная последовательность:");
            Console.WriteLine(string.Join(", ", numbers));

            // Выбор алгоритма сортировки
            var randomAlgorithm = rand.Next(2);
            if (randomAlgorithm == 0)
            {
                Console.WriteLine("\nСортировка алгоритмом: QuickSort");
                SortingAlgorithms.QuickSort(numbers, 0, numbers.Length - 1);
            }
            else
            {
                Console.WriteLine("\nСортировка алгоритмом: InsertionSort");
                SortingAlgorithms.InsertionSort(numbers);
            }

            Console.WriteLine("\nОтсортированная последовательность:");
            Console.WriteLine(string.Join(", ", numbers));

            // Отправка данных на сервер
            await ApiClient.SendDataToApi(numbers, apiUrl);
        }

        static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            return builder.Build();
        }
    }
}