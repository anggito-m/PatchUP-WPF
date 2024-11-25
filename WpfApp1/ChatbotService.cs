using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace WpfApp1
{
    class ChatbotService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> GetGeminiResponseAsync(string message)
        {
            var apiKey = ConfigurationManager.AppSettings["GeminiApiKey"]; ;
            var requestUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={apiKey}";

            JObject json = new JObject();
            JArray contents = new JArray();
            JObject content = new JObject();
            JArray parts = new JArray();
            JObject part = new JObject();
            part["text"] = $"{message}";
            parts.Add(part);
            content["parts"] = parts;
            contents.Add(content);
            json["contents"] = contents;

            string jsonString = json.ToString();
            // Corrected JSON payload structure
            
            var content_up = new StringContent(jsonString, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(requestUrl, content_up);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    //var jsonResponse = JObject.Parse(responseString);
                    //MessageBox.Show(responseString);
                    JObject jsonResponse = new JObject();
                    jsonResponse = JObject.Parse(responseString);

                    // Access the "text" value
                    //jsonObject["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString()
                    //string resultText = jsonResponse["candidates"][0]["content"][0]["parts"][0]["text"].ToString();
                    string resultText = jsonResponse["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();

                    // Assuming the response structure has a "content" field
                    //var resultText = jsonResponse["result"]?.ToString();
                    //MessageBox.Show(resultText);
                    return resultText ?? "No response content available.";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode}. Details: {errorContent}";
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
}
