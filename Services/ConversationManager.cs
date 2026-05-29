using System;
using System.Collections.Generic;
using System.Linq;
using CybersecurityChatbot.Models;

namespace CybersecurityChatbot.Services
{
    // Manages conversation flow and context
    public class ConversationManager
    {
        private readonly Queue<string> _recentTopics; // Generic collection for tracking
        private string _lastKeyword = string.Empty;
        private int _followUpCount = 0;
        
        // Delegate for follow-up handling
        public delegate string FollowUpHandler(string userInput, string currentTopic);
        public event FollowUpHandler OnFollowUpRequest;
        
        public ConversationManager()
        {
            _recentTopics = new Queue<string>();
        }
        
        public bool IsFollowUpRequest(string userInput)
        {
            var inputLower = userInput.ToLower();
            var followUpPhrases = new List<string>
            {
                "tell me more", "explain more", "more about", "another tip", "another one",
                "give me another", "what else", "elaborate", "go on", "continue",
                "more details", "and then", "so what", "i see", "ok", "okay"
            };
            
            return followUpPhrases.Any(phrase => inputLower.Contains(phrase));
        }
        
        public bool WantsAnotherTip(string userInput)
        {
            var inputLower = userInput.ToLower();
            var tipPhrases = new List<string>
            {
                "another tip", "another one", "more tips", "give me more", "additional"
            };
            
            return tipPhrases.Any(phrase => inputLower.Contains(phrase));
        }
        
        public bool WantsMoreDetails(string userInput)
        {
            var inputLower = userInput.ToLower();
            var detailPhrases = new List<string>
            {
                "explain more", "tell me more", "more details", "elaborate", "clarify"
            };
            
            return detailPhrases.Any(phrase => inputLower.Contains(phrase));
        }
        
        public bool WantsExample(string userInput)
        {
            var inputLower = userInput.ToLower();
            var examplePhrases = new List<string>
            {
                "example", "show me", "demonstrate", "like what", "for instance"
            };
            
            return examplePhrases.Any(phrase => inputLower.Contains(phrase));
        }
        
        public void UpdateCurrentTopic(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                _lastKeyword = keyword;
                _recentTopics.Enqueue(keyword);
                
                // Keep only last 5 topics for memory efficiency
                if (_recentTopics.Count > 5)
                    _recentTopics.Dequeue();
                    
                _followUpCount = 0;
            }
        }
        
        public void IncrementFollowUp()
        {
            _followUpCount++;
        }
        
        public string GetCurrentTopic()
        {
            return _lastKeyword;
        }
        
        public string GetPreviousTopic()
        {
            return _recentTopics.Count > 1 ? _recentTopics.ElementAt(_recentTopics.Count - 2) : _lastKeyword;
        }
        
        public int GetFollowUpCount() => _followUpCount;
        
        public void ResetFollowUp()
        {
            _followUpCount = 0;
        }
        
        public string GetConversationContext()
        {
            if (_recentTopics.Count == 0)
                return "new conversation";
            else if (_recentTopics.Count == 1)
                return $"discussing {_lastKeyword}";
            else
                return $"talking about {string.Join(", ", _recentTopics)}";
        }
    }
}