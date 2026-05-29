using System;
using System.Collections.Generic;

namespace CybersecurityChatbot.Services
{
    public class SentimentDetector
    {
        // Using HashSet for O(1) word lookup instead of List O(n)
        private readonly HashSet<string> _positiveWords;
        private readonly HashSet<string> _negativeWords;
        private readonly HashSet<string> _urgentWords;
        private readonly HashSet<string> _curiousWords;
        private readonly HashSet<string> _frustratedWords;
        private readonly HashSet<string> _worriedWords;
        
        public SentimentDetector()
        {
            _positiveWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "good", "great", "awesome", "excellent", "happy", "thanks", "thank you",
                "perfect", "love", "like", "helpful", "amazing", "wonderful", "fantastic",
                "clear", "easy", "simple", "appreciate", "enjoy", "best", "nice"
            };
            
            _negativeWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "bad", "terrible", "awful", "horrible", "hate", "dislike", "worst"
            };
            
            _urgentWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "urgent", "emergency", "asap", "quick", "fast", "immediately", "now",
                "hacked", "breach", "stolen", "compromised"
            };
            
            _curiousWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "curious", "wonder", "interesting", "tell me more", "explain", "how does",
                "why", "what about", "learn", "understand"
            };
            
            _frustratedWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "frustrated", "annoying", "confusing", "difficult", "hard", "complicated",
                "doesn't work", "not working", "stupid", "useless", "waste", "fed up"
            };
            
            _worriedWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "worried", "scared", "nervous", "anxious", "afraid", "concerned",
                "unsafe", "vulnerable", "exposed", "risk", "danger"
            };
        }
        
        public string DetectSentiment(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return "neutral";
                
            var inputLower = userInput.ToLower();
            
            // Priority order: urgent/security concerns first
            foreach (var word in _urgentWords)
            {
                if (inputLower.Contains(word))
                    return "urgent";
            }
            
            foreach (var word in _worriedWords)
            {
                if (inputLower.Contains(word))
                    return "worried";
            }
            
            foreach (var word in _frustratedWords)
            {
                if (inputLower.Contains(word))
                    return "frustrated";
            }
            
            foreach (var word in _curiousWords)
            {
                if (inputLower.Contains(word))
                    return "curious";
            }
            
            // Count positive/negative
            int positiveCount = 0;
            int negativeCount = 0;
            
            foreach (var word in _positiveWords)
            {
                if (inputLower.Contains(word))
                    positiveCount++;
            }
            
            foreach (var word in _negativeWords)
            {
                if (inputLower.Contains(word))
                    negativeCount++;
            }
            
            if (positiveCount > negativeCount)
                return "positive";
            else if (negativeCount > positiveCount)
                return "negative";
            
            return "neutral";
        }
        
        public string GetEmpatheticResponse(string sentiment, string keyword, string baseResponse)
        {
            switch (sentiment)
            {
                case "worried":
                    return $"😟 I understand it can be worrying to think about {keyword ?? "cybersecurity threats"}. {baseResponse}\n\nRemember, being aware is the first step to staying protected. You're doing the right thing by learning about this!";
                    
                case "frustrated":
                    return $"😤 I hear your frustration - cybersecurity can feel overwhelming at times. {baseResponse}\n\nWould you like me to explain this in a simpler way or give you practical steps to follow?";
                    
                case "curious":
                    return $"🤔 Great question! I love your curiosity about staying safe online. {baseResponse}\n\nWould you like me to share more details about this topic?";
                    
                case "urgent":
                    return $"⚠️ **Important Security Alert!** ⚠️\n\n{baseResponse}\n\nIf you believe you've been compromised, contact your IT security team or your bank immediately. I'm here to help guide you through the next steps.";
                    
                case "positive":
                    return $"😊 Excellent! {baseResponse}\n\nKeep up the great security habits! Anything else you'd like to learn about?";
                    
                case "negative":
                    return $"🤔 I understand your concern. {baseResponse}\n\nLet me know if you'd like me to clarify anything or provide more practical examples.";
                    
                default:
                    return baseResponse;
            }
        }
        
        public string GetSentimentEmoji(string sentiment)
        {
            return sentiment switch
            {
                "positive" => "😊",
                "negative" => "😟",
                "worried" => "😟",
                "frustrated" => "😤",
                "curious" => "🤔",
                "urgent" => "⚠️",
                _ => "🤖"
            };
        }
    }
}