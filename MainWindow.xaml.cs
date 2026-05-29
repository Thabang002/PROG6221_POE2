using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CybersecurityChatbot.Models;
using CybersecurityChatbot.Services;
using CybersecurityChatbot.Utilities;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly ChatbotService _chatbotService;
        private readonly VoiceService _voiceService;
        private ObservableCollection<ChatMessage> _messages;
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public MainWindow()
        {
            InitializeComponent();
            
            _chatbotService = new ChatbotService();
            _voiceService = new VoiceService();
            _messages = new ObservableCollection<ChatMessage>();
            MessagesItemsControl.ItemsSource = _messages;
            
            // Register custom response delegate
            _chatbotService.OnCustomResponse += CustomResponseHandler;
            
            Loaded += MainWindow_Loaded;
        }
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Display ASCII art
            AsciiArtDisplay.Text = AsciiArt.GetCyberSecurityArt();
            
            // Welcome message
            AddWelcomeMessage();
        }
        
        private void AddWelcomeMessage()
        {
            var welcomeMessage = new ChatMessage("Bot", 
                "👋 Welcome to your Cybersecurity Awareness Assistant!\n\n" +
                "I can help you with:\n" +
                "• 🔐 Password security tips\n" +
                "• ⚠️ Scam and phishing prevention\n" +
                "• 🛡️ Privacy protection strategies\n" +
                "• 🦠 Malware defense\n" +
                "• 🔑 Two-Factor Authentication (2FA)\n\n" +
                "Try asking me: 'Tell me about password safety' or 'How do I avoid scams?'");
            
            _messages.Add(welcomeMessage);
            
            // Speak welcome message
            _voiceService.Speak("Welcome to your Cybersecurity Awareness Assistant. How can I help protect you today?");
        }
        
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendUserMessage();
        }
        
        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Shift) != ModifierKeys.Shift)
            {
                e.Handled = true;
                SendUserMessage();
            }
        }
        
        private async void SendUserMessage()
        {
            string userInput = UserInputTextBox.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(userInput))
                return;
            
            // Add user message to chat
            var userMessage = new ChatMessage("User", userInput);
            _messages.Add(userMessage);
            
            // Clear input box
            UserInputTextBox.Clear();
            
            // Get bot response
            var botResponse = await System.Threading.Tasks.Task.Run(() => _chatbotService.GetResponse(userInput));
            _messages.Add(botResponse);
            
            // Speak bot response if voice is enabled
            if (_voiceService.IsVoiceEnabled)
            {
                _voiceService.Speak(botResponse.Text);
            }
            
            // Scroll to bottom
            await System.Threading.Tasks.Task.Delay(100);
            ChatScrollViewer.ScrollToBottom();
            
            // Update user info display
            UpdateUserInfoDisplay();
        }
        
        private void MicButton_Click(object sender, RoutedEventArgs e)
        {
            _voiceService.ToggleVoice();
            MicButton.Content = _voiceService.IsVoiceEnabled ? "🎤 ON" : "🎤 OFF";
            MicButton.Background = _voiceService.IsVoiceEnabled ? 
                new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(31, 111, 139)) : 
                new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(45, 51, 59));
            
            var statusMsg = new ChatMessage("Bot", 
                _voiceService.IsVoiceEnabled ? "🔊 Voice responses enabled!" : "🔇 Voice responses disabled.");
            _messages.Add(statusMsg);
        }
        
        private void ClearChatButton_Click(object sender, RoutedEventArgs e)
        {
            _messages.Clear();
            AddWelcomeMessage();
            _chatbotService.ClearMemory();
            UpdateUserInfoDisplay();
        }
        
        private void UpdateUserInfoDisplay()
        {
            var profile = _chatbotService.GetUserProfile();
            UserNameDisplay.Text = profile.Name;
            
            if (profile.Interests.Count > 0)
            {
                KnownTopicsDisplay.Text = string.Join(", ", profile.Interests);
            }
            else
            {
                KnownTopicsDisplay.Text = "None yet";
            }
        }
        
        private string CustomResponseHandler(string userInput, string sentiment, string keyword)
        {
            // Example of custom delegate response
            if (userInput.ToLower().Contains("thank"))
            {
                return "You're very welcome! 😊 Remember, staying informed is the first step to staying secure. Anything else I can help with?";
            }
            
            if (userInput.ToLower().Contains("help") && string.IsNullOrEmpty(keyword))
            {
                return "I'd be happy to help! Here are topics I know about:\n• Password Security\n• Scam Prevention\n• Privacy Protection\n• Malware Defense\n• Two-Factor Authentication\n\nWhich would you like to learn about?";
            }
            
            return null; // Let normal processing continue
        }
    }
}