using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot.Services
{
    public class KeywordRecognizer
    {
        // Using Dictionary for O(1) lookup performance
        private readonly Dictionary<string, KeywordInfo> _keywordDatabase;
        private readonly List<string> _allKeywords;
        
        public class KeywordInfo
        {
            public List<string> Synonyms { get; set; }
            public List<string> Responses { get; set; }  // Multiple responses for random selection
            public string Category { get; set; }
            
            public KeywordInfo(List<string> synonyms, List<string> responses, string category)
            {
                Synonyms = synonyms;
                Responses = responses;
                Category = category;
            }
        }
        
        public KeywordRecognizer()
        {
            _keywordDatabase = new Dictionary<string, KeywordInfo>
            {
                { "password", new KeywordInfo(
                    new List<string> { "password", "passcode", "login", "credentials", "passphrase", "pwd", "authentication" },
                    new List<string> {
                        "🔐 **Password Security:** Use strong, unique passwords (12+ chars with uppercase, lowercase, numbers, symbols).",
                        "🔑 **Password Tip:** Enable Two-Factor Authentication (2FA) for an extra layer of security!",
                        "🛡️ **Password Manager:** Consider using a password manager like Bitwarden or LastPass.",
                        "⚠️ **Never reuse passwords!** Each account needs its own unique password.",
                        "📝 **Create passphrases:** 'PurpleUnicornDance$72' is harder to crack than 'P@ssw0rd'"
                    },
                    "Authentication"
                )},
                { "scam", new KeywordInfo(
                    new List<string> { "scam", "phishing", "fraud", "fake", "deception", "trick", "hoax", "social engineering" },
                    new List<string> {
                        "⚠️ **Spotting Scams:** Never click suspicious links. Verify sender email addresses carefully.",
                        "🛡️ **Phishing Protection:** Legit companies never ask for passwords via email or phone.",
                        "🔍 **Red Flags:** Urgent language, spelling errors, and requests for personal info are scam signs.",
                        "📞 **Phone Scams:** Hang up on callers demanding payment or personal information.",
                        "💻 **Email Safety:** Hover over links to see real URL before clicking."
                    },
                    "Threat Prevention"
                )},
                { "privacy", new KeywordInfo(
                    new List<string> { "privacy", "data", "personal info", "private", "confidential", "gdpr", "tracking" },
                    new List<string> {
                        "🛡️ **Privacy Protection:** Review privacy settings on all your social media accounts monthly.",
                        "🔒 **Data Security:** Use end-to-end encrypted messaging like Signal or WhatsApp.",
                        "🌐 **Browser Privacy:** Use private browsing modes and consider privacy-focused browsers like Brave.",
                        "📱 **App Permissions:** Review which apps have access to your location, camera, and contacts.",
                        "🔍 **Search Privacy:** DuckDuckGo doesn't track your searches like Google does."
                    },
                    "Data Protection"
                )},
                { "malware", new KeywordInfo(
                    new List<string> { "malware", "virus", "trojan", "ransomware", "spyware", "worm", "rootkit", "adware" },
                    new List<string> {
                        "🦠 **Malware Defense:** Install reliable antivirus software (Windows Defender is good and free!).",
                        "💾 **Ransomware Protection:** Regular backups saved offline can save you from ransomware.",
                        "📎 **Email Attachments:** Never open attachments from unknown senders.",
                        "💿 **Download Safety:** Only download software from official websites.",
                        "🔄 **Keep Updated:** Regular system updates patch security vulnerabilities."
                    },
                    "Threat Prevention"
                )},
                { "2fa", new KeywordInfo(
                    new List<string> { "2fa", "two factor", "mfa", "multi factor", "authenticator", "2-step" },
                    new List<string> {
                        "🔑 **2FA Benefits:** Adds 99.9% more security to your accounts!",
                        "📱 **Authenticator Apps:** Use Google Authenticator, Microsoft Authenticator, or Authy.",
                        "⚠️ **SMS Risks:** Authenticator apps are more secure than text message codes.",
                        "🔐 **Enable 2FA:** Turn it on for email, banking, social media, and cloud storage.",
                        "💾 **Backup Codes:** Save your 2FA backup codes in a safe place!"
                    },
                    "Authentication"
                )},
                { "backup", new KeywordInfo(
                    new List<string> { "backup", "recovery", "restore", "copy", "cloud storage" },
                    new List<string> {
                        "💾 **3-2-1 Backup Rule:** 3 copies, 2 media types, 1 offsite backup.",
                        "☁️ **Cloud Backup:** Services like Backblaze or iDrive offer automated backups.",
                        "💿 **External Drives:** Keep an offline backup for ransomware protection.",
                        "🔄 **Test Your Backups:** Regularly verify you can restore from backup.",
                        "📅 **Schedule Backups:** Set automatic daily or weekly backups."
                    },
                    "Data Protection"
                )},
                { "update", new KeywordInfo(
                    new List<string> { "update", "patch", "upgrade", "latest version", "security update" },
                    new List<string> {
                        "🔄 **Why Updates Matter:** Fix security vulnerabilities and add new features.",
                        "⚡ **Enable Auto-Updates:** Turn on automatic updates for OS and apps.",
                        "🔧 **Patch Tuesday:** Microsoft releases security updates on second Tuesday monthly.",
                        "📱 **Mobile Updates:** Keep phone OS and apps updated for security.",
                        "⚠️ **Zero-Day Exploits:** Updates fix known vulnerabilities being exploited."
                    },
                    "System Security"
                )},
                { "wifi", new KeywordInfo(
                    new List<string> { "wifi", "wireless", "network", "router", "hotspot" },
                    new List<string> {
                        "📡 **Wi-Fi Security:** Change default router passwords immediately!",
                        "🔒 **Encryption:** Use WPA3 or WPA2 encryption on your network.",
                        "👥 **Guest Network:** Create separate Wi-Fi for visitors and IoT devices.",
                        "🕵️ **Public Wi-Fi:** Always use VPN on public networks.",
                        "🚫 **Hide SSID:** Disable broadcasting if you want hidden network."
                    },
                    "Network Security"
                )}
            };
            
            _allKeywords = _keywordDatabase.Keys.ToList();
        }
        
        // Returns detected keyword with O(1) lookup after identification
        public string RecognizeKeyword(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return null;
                
            var inputLower = userInput.ToLower();
            
            foreach (var keyword in _keywordDatabase)
            {
                if (keyword.Value.Synonyms.Any(synonym => inputLower.Contains(synonym)))
                {
                    return keyword.Key;
                }
            }
            
            return null;
        }
        
        // Returns random response from multiple options (using List for random selection)
        public string GetRandomResponseForKeyword(string keyword)
        {
            if (string.IsNullOrEmpty(keyword) || !_keywordDatabase.ContainsKey(keyword))
                return null;
                
            var responses = _keywordDatabase[keyword].Responses;
            var random = new Random();
            return responses[random.Next(responses.Count)];
        }
        
        // For follow-up questions - get another tip on same topic
        public string GetAnotherTip(string keyword)
        {
            if (string.IsNullOrEmpty(keyword) || !_keywordDatabase.ContainsKey(keyword))
                return null;
                
            var responses = _keywordDatabase[keyword].Responses;
            var random = new Random();
            
            // If we have multiple tips, return a different one (could implement tracking to avoid repeats)
            return responses[random.Next(responses.Count)];
        }
        
        public string GetCategory(string keyword)
        {
            return _keywordDatabase.ContainsKey(keyword) ? _keywordDatabase[keyword].Category : "General";
        }
        
        public List<string> GetAllKeywords() => _allKeywords;
        
        public bool IsValidKeyword(string keyword) => _keywordDatabase.ContainsKey(keyword);
    }
}