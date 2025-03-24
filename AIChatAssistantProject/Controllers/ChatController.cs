using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace AIChatAssistantProject.Controllers
{
    public class ChatController : Controller
    {
        private static readonly string apiKey= "";
        private static List<Dictionary<string, object>> chatHistory = new List<Dictionary<string, object>>();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message, IFormFile image)
        {
            if (string.IsNullOrEmpty(message) && image == null)
            {
                return Json(new { error = "Please enter a message or upload an image." });
            }

            var messages = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "role", "user" },
                    { "content", new List<object>() }
                }
            };

            // Handle image
            if (image != null && image.Length > 0)
            {
               
                if (image.Length > 2 * 1024 * 1024) // 2MB limit
                {
                    return Json(new { error = "Image size exceeds 2MB limit." });
                }

                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    var base64Image = Convert.ToBase64String(imageBytes);
                    var mimeType = image.ContentType;

                    var imageContent = new Dictionary<string, object>
                    {
                        { "type", "image_url" },
                        { "image_url", new Dictionary<string, object>
                            {
                                { "url", $"data:{mimeType};base64,{base64Image}" },
                                { "detail", "high" }
                            }
                        }
                    };

                    ((List<object>)messages[0]["content"]).Add(imageContent);
                }
            }

            // Handle text message
            if (!string.IsNullOrEmpty(message))
            {
                var textContent = new Dictionary<string, object>
                {
                    { "type", "text" },
                    { "text", message }
                };

                ((List<object>)messages[0]["content"]).Add(textContent);
            }

            // Add messages to chat history
            chatHistory.AddRange(messages);

            // Take only the last 5 messages to avoid sending a huge history
            var limitedHistory = chatHistory.Skip(Math.Max(0, chatHistory.Count - 5)).ToList();

            // Send chat completion request
            var postData = new
            {
                model = "gpt-4o-mini",
                messages = limitedHistory,
                temperature = 0.7,
                max_tokens = 200
            };

            string apiUrl = "https://api.openai.com/v1/chat/completions";

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                    client.Timeout = TimeSpan.FromSeconds(30);

                    var content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");

                    // Log request start time
                    var startTime = DateTime.Now;
                    Console.WriteLine($"Sending request to OpenAI API at {startTime}");

                    var response = await client.PostAsync(apiUrl, content);

                    // Log request end time
                    var endTime = DateTime.Now;
                    Console.WriteLine($"Received response from OpenAI API at {endTime}. Duration: {(endTime - startTime).TotalSeconds} seconds");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = JsonSerializer.Deserialize<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
                        if (responseData != null && responseData.ContainsKey("choices"))
                        {
                            var choices = (JsonElement)responseData["choices"];
                            var botMessage = choices[0].GetProperty("message").GetProperty("content").GetString();

                            chatHistory.Add(new Dictionary<string, object>
                            {
                                { "role", "assistant" },
                                { "content", botMessage }
                            });

                            return Json(new { response = botMessage });
                        }
                        else
                        {
                            return Json(new { error = "Invalid response from API: No choices found." });
                        }
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"API Error: {errorMessage}");
                        return Json(new { error = $"Failed to connect to API: {response.ReasonPhrase}" });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return Json(new { error = "An error occurred while connecting to the API. Please try again later." });
            }
        }

        [HttpPost]
        public IActionResult ClearChat()
        {
            chatHistory.Clear();
            return Json(new { success = true });
        }
    }
}
