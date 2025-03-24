namespace AIChatAssistantProject.Models
{
    public class OpenAIRequest
    {
        public string Model { get; set; }
        public List<Message> Messages { get; set; }
        public double Temperature { get; set; }
        public int MaxTokens { get; set; }
    }
    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

    public class OpenAIResponse
    {
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; }
    }
}
