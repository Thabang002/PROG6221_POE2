using System.Collections.Generic;

namespace CybersecurityChatbot.Models
{
    public class UserProfile
    {
        public string Name { get; set; } = "Guest";
        public List<string> Interests { get; set; } = new List<string>();
        public Dictionary<string, string> UserMemory { get; set; } = new Dictionary<string, string>();
        public int InteractionCount { get; set; } = 0;
        public string CurrentTopic { get; set; } = string.Empty;
        public List<string> ConversationHistory { get; set; } = new List<string>();
        public Dictionary<string, int> TopicInterestLevel { get; set; } = new Dictionary<string, int>();
        
        public void Remember(string key, string value)
        {
            if (UserMemory.ContainsKey(key))
                UserMemory[key] = value;
            else
                UserMemory.Add(key, value);
        }
        
        public string Recall(string key)
        {
            return UserMemory.ContainsKey(key) ? UserMemory[key] : null;
        }
        
        public void AddInterest(string topic)
        {
            if (!Interests.Contains(topic))
            {
                Interests.Add(topic);
                TopicInterestLevel[topic] = 1;
            }
            else
            {
                TopicInterestLevel[topic] = TopicInterestLevel.ContainsKey(topic) ? TopicInterestLevel[topic] + 1 : 1;
            }
        }
        
        public string GetFavoriteTopic()
        {
            if (TopicInterestLevel.Count == 0) return null;
            
            string favorite = null;
            int maxCount = 0;
            
            foreach (var topic in TopicInterestLevel)
            {
                if (topic.Value > maxCount)
                {
                    maxCount = topic.Value;
                    favorite = topic.Key;
                }
            }
            
            return favorite;
        }
        
        public void AddToHistory(string message)
        {
            ConversationHistory.Add(message);
            // Keep only last 50 messages for memory efficiency
            if (ConversationHistory.Count > 50)
                ConversationHistory.RemoveAt(0);
        }
    }
}