# AI Chat Assistant

## Overview

**AI Chat Assistant** is a web-based application built using **ASP.NET Core MVC**, allowing users to interact with an AI-powered chatbot. The app supports text conversations, file uploads (e.g., `.txt` files), and image uploads, with a clean and responsive UI. The chatbot uses an external API (such as OpenAI's GPT-4o-mini model) to generate intelligent responses.

The project includes features like dark mode, file/image preview, and a scrollable chat window, making it user-friendly and visually appealing.

---

## Features

- **Interactive Chat Interface:** Users can send text messages, upload files, and images.
- **File and Image Previews:** Displays uploaded file details before sending them.
- **Dark Mode:** Switch between light and dark themes.
- **Responsive Design:** Optimized for desktop and mobile devices.
- **Scrollable Chat Window:** Allows easy navigation of long conversations.
- **Sticky Input Bar:** Ensures easy message input at all times.
- **Error Handling:** Displays meaningful error messages for failed API requests.

---

## Technologies Used

- **ASP.NET Core MVC** - Backend framework.
- **Bootstrap 5** - UI components for a modern look.
- **Font Awesome** - Icons for the interface.
- **JavaScript (Fetch API)** - Handles API requests.
- **CSS Flexbox** - Provides a responsive layout.
- **OpenAI API** - Powers the chatbot (GPT-4o-mini model).

---

## Prerequisites

Before running the project, ensure you have:

- **.NET SDK** (6.0 or later)
- **Visual Studio** (or another compatible IDE)
- **OpenAI API Key** ([Get one here](https://platform.openai.com/))

---

## Setup Instructions

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/mohammedhagggagg/AIChatAssistant.git
   cd AIChatAssistant
   ```
2. **Open the Project:** In Visual Studio or another IDE.
3. **Configure the OpenAI API Key:**
   - Open `ChatController.cs` in the `Controllers` folder.
   - Ensure the following implementation is present:
     ```csharp
     private readonly string apiKey;
     private readonly IConfiguration _configuration; // For reading settings
     private static List<Dictionary<string, object>> chatHistory = new List<Dictionary<string, object>>();

     // Constructor to enable IConfiguration
     public ChatController(IConfiguration configuration)
     {
         _configuration = configuration;
         apiKey = _configuration["OpenAI:ApiKey"]; // Read API Key from appsettings.json
     }
     ```
4. **Restore Dependencies:**
   ```bash
   dotnet restore
   ```
5. **Run the Application:**
   - Press **F5** in Visual Studio.
   - Or run:
     ```bash
     dotnet run
     ```
6. **Access the Chat Interface:**
   - Open your browser and navigate to `https://localhost:7031/Chat/Index`

---

## Project Structure

- **Controllers/ChatController.cs** - Handles chat-related requests.
- **Views/Chat/Index.cshtml** - Main chat interface.
- **Views/Shared/_Layout.cshtml** - Defines the app's layout.
- **wwwroot/** - Contains static files (CSS, JavaScript, etc.).

---

## Usage

- **Start a Conversation:** Type a message and press Enter.
- **Upload Files:** Click `Upload File`, preview, then send.
- **Upload Images:** Click `Upload Image`, preview, then send.
- **Toggle Dark Mode:** Click the moon icon.
- **Clear Chat:** Click the trash icon to reset the conversation.

---

## Known Issues

- **Image Processing:** Currently, images are not fully processed by OpenAI. Future updates will add base64 encoding.
- **Large Files:** Files over 50,000 characters are not supported.

---

## Future Improvements

- Enable AI to process images.
- Save chat history in a database.
- Support additional file types (PDFs, Word docs, etc.).
- Implement user authentication.
- Improve chat performance with pagination.

---

## Contributing

Contributions are welcome! Follow these steps:

1. **Fork the repository.**
2. **Create a new branch:**
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. **Commit your changes:**
   ```bash
   git commit -m "Added your feature description"
   ```
4. **Push your branch and create a pull request:**
   ```bash
   git push origin feature/your-feature-name
   ```

---

## License

This project is licensed under the MIT License.

---

## Contact

- **Email:** mohammedhaggagg@gmail.com  
- **GitHub:** [mohammedhagggagg](https://github.com/mohammedhagggagg)

---

## Git Ignore

```
# Ignore appsettings.json to prevent uploading sensitive data (like API keys)
appsettings.json
appsettings.Development.json

# Ignore bin and obj folders (compiled files)
bin/
obj/

# Ignore Visual Studio user-specific files
*.csproj.user
.vs/
*.suo

# Ignore NuGet packages folder
packages/

# Ignore temporary files
*.log
*.tmp

# Ignore environment-specific files
*.env
```

