using System;
using System.Collections.Generic;
using System.Linq;
using CybersecurityChatbot.Models;

namespace CybersecurityChatbot.Services
{
    public class ChatbotService
    {
        private readonly KeywordRecognizer _keywordRecognizer;
        private readonly SentimentDetector _sentimentDetector;
        private readonly ConversationManager _conversationManager;
        private readonly ResponseCache<string> _responseCache;
        private UserProfile _userProfile;
        
        private readonly List<string> _greetings;
        private readonly List<string> _farewells;
        private readonly List<string> _fallbackResponses;
        private readonly List<string> _encouragementMessages;
        
        // Delegate for custom response handling
        public delegate string ResponseHandler(string userInput, string sentiment, string keyword);
        public event ResponseHandler OnCustomResponse;
        
        public ChatbotService()
        {
            _keywordRecognizer = new KeywordRecognizer();
            _sentimentDetector = new SentimentDetector();
            _conversationManager = new ConversationManager();
            _responseCache = new ResponseCache<string>(50);
            _userProfile = new UserProfile();
            
            _greetings = new List<string>
            {
                "Hello! 👋 I'm your Cybersecurity Awareness Assistant. How can I help protect you today?",
                "Welcome back! 🛡️ Ready to learn about staying safe online?",
                "Hi there! 👨‍💻 Ask me anything about passwords, privacy, or avoiding scams!",
                "Greetings! 🔐 I'm here to help you stay secure online. What would you like to learn about?"
            };
            
            _farewells = new List<string>
            {
                "Stay safe online! 🔐 Remember: cybersecurity is everyone's responsibility.",
                "Goodbye! 👋 Keep those passwords strong and stay vigilant!",
                "Take care! 🛡️ Don't forget to enable 2FA on your important accounts.",
                "Until next time! 💪 Stay secure and keep learning about online safety."
            };
            
            _fallbackResponses = new List<string>
            {
                "That's interesting! I specialize in cybersecurity topics like password safety, scam prevention, and privacy protection. What would you like to know?",
                "I'm here to help with cybersecurity awareness! Try asking me about passwords, scams, privacy, or malware protection.",
                "Great question! To better assist you, could you tell me more about your cybersecurity concern?",
                "I want to help! Would you like tips on passwords, avoiding scams, or protecting your privacy?"
            };
            
            _encouragementMessages = new List<string>
            {
                "You're taking an important step toward better security! 💪",
                "Great question - staying informed is key to staying safe! 🛡️",
                "I'm glad you're interested in protecting yourself online! 🔐",
                "Learning about cybersecurity is a smart move! Keep going! ⭐"
            };
        }
        
        public ChatMessage GetResponse(string userInput)
        {
            // Error handling for null/empty input
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return new ChatMessage("Bot", "I didn't catch that. Could you please type a question or message?", "neutral", null);
            }
            
            try
            {
                // Update interaction count and history
                _userProfile.InteractionCount++;
                _userProfile.AddToHistory(userInput);
                
                // Extract user info
                ExtractUserInfo(userInput);
                
                // Detect sentiment
                string sentiment = _sentimentDetector.DetectSentiment(userInput);
                
                // Check conversation flow first (follow-up questions)
                string followUpResponse = HandleConversationFlow(userInput, sentiment);
                if (followUpResponse != null)
                {
                    return new ChatMessage("Bot", followUpResponse, sentiment, _conversationManager.GetCurrentTopic());
                }
                
                // Recognize keywords
                string keyword = _keywordRecognizer.RecognizeKeyword(userInput);
                
                // Check cache for identical or similar queries
                string cacheKey = $"{keyword ?? "no_keyword"}_{sentiment}";
                if (_responseCache.TryGet(cacheKey, out string cachedResponse))
                {
                    var cachedMessage = new ChatMessage("Bot", cachedResponse, sentiment, keyword);
                    cachedMessage.CurrentTopic = keyword;
                    return cachedMessage;
                }
                
                // Generate response
                string responseText = GenerateResponse(userInput, sentiment, keyword);
                
                // Cache the response
                _responseCache.Add(cacheKey, responseText);
                
                // Update conversation state
                if (keyword != null)
                {
                    _userProfile.AddInterest(keyword);
                    _userProfile.Remember("last_topic", keyword);
                    _conversationManager.UpdateCurrentTopic(keyword);
                }
                
                return new ChatMessage("Bot", responseText, sentiment, keyword, _conversationManager.GetCurrentTopic());
            }
            catch (Exception ex)
            {
                // Professional error handling - never crash
                return new ChatMessage("Bot", "I encountered a small hiccup. Could you please rephrase your question? I'm here to help with cybersecurity topics!", "neutral", null);
            }
        }
        
        private string HandleConversationFlow(string userInput, string sentiment)
        {
            var inputLower = userInput.ToLower();
            string currentTopic = _conversationManager.GetCurrentTopic();
            
            // Handle "another tip" requests
            if (_conversationManager.WantsAnotherTip(userInput) && !string.IsNullOrEmpty(currentTopic))
            {
                _conversationManager.IncrementFollowUp();
                string anotherTip = _keywordRecognizer.GetAnotherTip(currentTopic);
                return $"{anotherTip}\n\nWould you like another tip about {currentTopic} or shall we explore something else?";
            }
            
            // Handle "tell me more" / "explain more" requests
            if (_conversationManager.WantsMoreDetails(userInput) && !string.IsNullOrEmpty(currentTopic))
            {
                _conversationManager.IncrementFollowUp();
                return GetDetailedExplanation(currentTopic, sentiment);
            }
            
            // Handle "give me an example" requests
            if (_conversationManager.WantsExample(userInput) && !string.IsNullOrEmpty(currentTopic))
            {
                return GetExampleForTopic(currentTopic);
            }
            
            return null;
        }
        
        private string GetDetailedExplanation(string topic, string sentiment)
        {
            var detailedResponses = new Dictionary<string, string>
            {
                { "password", "Let me explain password security in more detail:\n\n**Why strong passwords matter:** Hackers use automated tools to guess passwords. A weak password can be cracked in seconds, while a strong one takes years.\n\n**How to create strong passwords:**\n• Use 12+ characters\n• Mix uppercase, lowercase, numbers, and symbols\n• Avoid personal info (birthdays, names)\n• Use passphrases like 'Correct-Horse-Battery-Staple'\n\n**Password managers** generate and store unique passwords for each site, so you only need to remember one master password." },
                
                { "scam", "Let me dive deeper into scam prevention:\n\n**Common scam types to watch for:**\n• Phishing emails pretending to be from banks\n• Tech support scams calling about 'virus infections'\n• Romance scams building fake relationships\n• Investment scams promising unrealistic returns\n• Package delivery scams with fake tracking links\n\n**Red flags to spot:**\n• Urgent language ('Act now or your account closes')\n• Requests for gift cards or wire transfers\n• Typos and grammatical errors\n• Unexpected attachments\n• Too-good-to-be-true offers\n\n**What to do:** Never share personal info, verify through official channels, and report suspicious messages." },
                
                { "privacy", "Here's a detailed look at privacy protection:\n\n**Online tracking:** Companies track your browsing to build profiles about you. Use privacy tools to limit this.\n\n**Privacy checklist:**\n✓ Review app permissions (does a flashlight app need your contacts?)\n✓ Use a VPN on public Wi-Fi\n✓ Enable 'Do Not Track' in browsers\n✓ Use privacy-focused search engines\n✓ Cover your webcam when not in use\n✓ Opt out of data sharing where possible\n\n**Social media privacy:** Set profiles to private, don't share location in real-time, and think before posting anything you wouldn't want public." }
            };
            
            string empathy = _sentimentDetector.GetSentimentEmoji(sentiment);
            if (detailedResponses.ContainsKey(topic))
                return $"{empathy} {detailedResponses[topic]}\n\nWould you like more information on this topic or a practical example?";
            
            return $"I'd be happy to explain more about {topic}. What specific aspect would you like to understand better?";
        }
        
        private string GetExampleForTopic(string topic)
        {
            var examples = new Dictionary<string, string>
            {
                { "password", "**Example:** Instead of using 'Fluffy2024' (easily guessed), create 'Purple-Tiger-Dance-72!' - it's easier to remember but much harder to crack!" },
                { "scam", "**Example:** You get an email 'Your Netflix account is suspended - click here to verify.' Instead of clicking, open Netflix directly in your browser. That's how you spot the scam!" },
                { "privacy", "**Example:** Before posting vacation photos online, wait until you're back home. Posting in real-time tells everyone your house is empty!" }
            };
            
            if (examples.ContainsKey(topic))
                return $"📝 {examples[topic]}\n\nWould you like more tips about {topic}?";
                
            return $"Here's a practical tip about {topic}: always question unexpected requests for your personal information, even if they seem to come from trusted sources.";
        }
        
        private string GenerateResponse(string userInput, string sentiment, string keyword)
        {
            var inputLower = userInput.ToLower();
            var random = new Random();

            // Check for custom response from delegate
            if (OnCustomResponse != null)
            {
                foreach (ResponseHandler handler in OnCustomResponse.GetInvocationList())
                {
                    var customResponse = handler(userInput, sentiment, keyword);
                    if (!string.IsNullOrEmpty(customResponse))
                    {
                        string empatheticResponse = _sentimentDetector.GetEmpatheticResponse(sentiment, keyword, customResponse);
                        string encouragement = random.Next(3) == 0 ? $" {_encouragementMessages[random.Next(_encouragementMessages.Count)]}" : "";
                        return empatheticResponse + encouragement;
                    }
                }
            }

            // Greeting handling
            if (inputLower.Contains("hello") || inputLower.Contains("hi ") || inputLower.Contains("hey ") || inputLower == "hi")
            {
                var greeting = _greetings[random.Next(_greetings.Count)];
                if (_userProfile.Name != "Guest")
                    greeting = $"Hi {_userProfile.Name}! {greeting}";
                return greeting;
            }

            // Default fallback response
            return GetFallbackResponse(keyword, random);
        }

        private string GetFallbackResponse(string keyword, Random random)
        {
            return _fallbackResponses[random.Next(_fallbackResponses.Count)];
        }

        private void ExtractUserInfo(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return;

            var lower = userInput.ToLower();
            if (lower.Contains("my name is "))
            {
                var start = lower.IndexOf("my name is ", StringComparison.Ordinal) + "my name is ".Length;
                var namePart = userInput.Substring(start).Trim();
                if (!string.IsNullOrWhiteSpace(namePart))
                {
                    var firstWord = namePart.Split(' ')[0];
                    _userProfile.Name = char.ToUpper(firstWord[0]) + firstWord.Substring(1);
                }
            }
        }

        public void ClearMemory()
        {
            _userProfile = new UserProfile();
            _responseCache.Clear();
            _conversationManager.ResetFollowUp();
        }

        public UserProfile GetUserProfile()
        {
            return _userProfile;
        }
    }
}
