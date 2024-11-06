using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Chatbot.xaml
    /// </summary>
    public partial class Chatbot : Page
    {
        private readonly ChatbotService chatbotService;
        public Chatbot()
        {
            InitializeComponent();
            chatbotService = new ChatbotService();
            sidebar.NavigateToPage += Sidebar_NavigateToPage;
        }
        private void Sidebar_NavigateToPage(object sender, string pageName)
        {
            Frame frame = new Frame();
            frame.Navigate(sidebar.Navigate(pageName));
            this.Content = frame;
        }
        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = UserInputTextBox.Text;
            if (!string.IsNullOrWhiteSpace(userMessage))
            {
                // Display user's message
                AddMessageToChat(userMessage, isUserMessage: true);
                UserInputTextBox.Clear();

                // Send the message to the Gemini API and get the response
                string botResponse = await chatbotService.GetGeminiResponseAsync(userMessage);

                // Display the bot's response
                AddMessageToChat(botResponse, isUserMessage: false);

                // Scroll to the latest message
                ChatScrollViewer.ScrollToEnd();
            }
        }
        private void AddMessageToChat(string message, bool isUserMessage)
        {
            // Create a chat bubble for the message
            Border chatBubble = new Border
            {
                Background = isUserMessage ? Brushes.LightBlue : Brushes.LightGray,
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(10),
                Margin = new Thickness(5),
                HorizontalAlignment = isUserMessage ? HorizontalAlignment.Right : HorizontalAlignment.Left
            };

            TextBlock messageText = new TextBlock
            {
                Text = message,
                Foreground = Brushes.Black,
                TextWrapping = TextWrapping.Wrap
            };

            chatBubble.Child = messageText;
            MessagesPanel.Children.Add(chatBubble);
        }
    }
}
