using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WpfApp1
{
    class ChatbotService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> GetGeminiResponseAsync(string message)
        {
            var apiKey = "My_API_Key";
            var requestUrl = $"https://generativelanguage.googleapis.com/v1beta2/models/gemini-1.5-flash-latest:generateContent?key={apiKey}";

            var content = new StringContent(
                $"{{\"contents\": [{{\"parts\": [{{\"text\": \"{message}\"}}]}}]}}",
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await client.PostAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            else
            {
                return $"Error: {response.StatusCode}. Unable to reach Gemini API.";
            }
        }
    }
}
