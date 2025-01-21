using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestTask
{
    public static class ApiClient
    {
        // Отправка данных на REST API
        public static async Task SendDataToApi(int[] sortedNumbers, string apiUrl)
        {
            using HttpClient client = new HttpClient();
            var jsonContent = JsonSerializer.Serialize(sortedNumbers);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(apiUrl, httpContent);

                // Чтение и вывод содержимого ответа
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nДанные успешно отправлены на сервер.");
                    Console.WriteLine("Ответ сервера:");
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"\nОшибка при отправке данных: {response.StatusCode}");
                    Console.WriteLine("Ответ сервера:");
                    Console.WriteLine(responseBody);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"\nОшибка при выполнении запроса: {e.Message}");
            }
        }
    }
}
