using System;

namespace CybersecurityChatbot.Models
{
    public class ChatMessage
    {
        public string Sender { get; set; } // "User" or "Bot"
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public string Sentiment { get; set; }
        public string DetectedKeyword { get; set; }
        public string CurrentTopic { get; set; }
        public bool ShowKeyword => !string.IsNullOrEmpty(DetectedKeyword);
        public bool ShowSentiment => !string.IsNullOrEmpty(Sentiment);
        
        public ChatMessage()
        {
            Timestamp = DateTime.Now;
        }
        
        public ChatMessage(string sender, string text) : this()
        {
            Sender = sender;
            Text = text;
            Sentiment = string.Empty;
            DetectedKeyword = string.Empty;
            CurrentTopic = string.Empty;
        }
        
        public ChatMessage(string sender, string text, string sentiment, string keyword, string topic = "") : this()
        {
            Sender = sender;
            Text = text;
            Sentiment = sentiment;
            DetectedKeyword = keyword;
            CurrentTopic = topic;
        }
    }
}