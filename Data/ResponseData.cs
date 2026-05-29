using System;
using System.Collections.Generic;

namespace CybersecurityChatbot.Data
{
    /// <summary>
    /// Centralized data storage for all chatbot responses
    /// Using dictionaries and lists for efficient lookups (O(1) complexity)
    /// </summary>
    public static class ResponseData
    {
        #region Cybersecurity Tips Database
        
        // Using Dictionary with List for random response selection
        public static readonly Dictionary<string, List<string>> CybersecurityTips = new Dictionary<string, List<string>>
        {
            ["password"] = new List<string>
            {
                "🔐 Use a password manager to generate and store unique passwords for each account.",
                "🔑 Create passphrases like 'Correct-Horse-Battery-Staple' - they're easier to remember and harder to crack!",
                "⚠️ Never reuse passwords across different websites or services.",
                "📝 Your password should be at least 12 characters long with mixed case, numbers, and symbols.",
                "🔄 Change your passwords every 3-6 months, especially for sensitive accounts.",
                "🚫 Avoid using personal information like birthdays, pet names, or addresses in passwords."
            },
            ["scam"] = new List<string>
            {
                "⚠️ Never click links in unsolicited emails - hover over them first to see the real URL.",
                "📞 Hang up on unexpected calls asking for personal information or payments.",
                "💻 Legitimate companies will NEVER ask for your password via email or phone.",
                "🔍 Look for spelling errors and urgent language - these are common scam tactics.",
                "📧 Verify sender email addresses carefully - scammers use addresses that look similar to real ones.",
                "💰 Never send money via gift cards or wire transfers to someone you haven't met in person."
            },
            ["privacy"] = new List<string>
            {
                "🛡️ Review your social media privacy settings monthly - companies change them frequently.",
                "🔒 Use end-to-end encrypted messaging apps like Signal or WhatsApp for sensitive conversations.",
                "🌐 Use a VPN on public Wi-Fi to encrypt your internet traffic.",
                "📱 Check which apps have access to your location, camera, and contacts - revoke unnecessary permissions.",
                "🔍 Search for yourself online to see what personal information is publicly available.",
                "📧 Use email aliases or 'plus addressing' (yourname+shopping@gmail.com) to track who's selling your data."
            },
            ["malware"] = new List<string>
            {
                "🦠 Keep your antivirus software updated and run regular scans.",
                "💾 Back up important files to an external drive or cloud storage - ransomware can't steal what's backed up!",
                "📎 Never open email attachments from unknown senders, even if they look official.",
                "💿 Only download software from official websites, not third-party sources.",
                "🔄 Enable automatic updates for your operating system and all applications.",
                "🔒 Use Windows Defender or equivalent - it's free and effective for most users."
            },
            ["2fa"] = new List<string>
            {
                "📱 Use authenticator apps (Google Authenticator, Microsoft Authenticator, Authy) instead of SMS when possible.",
                "🔑 Enable 2FA on your email account first - it's the key to resetting your other passwords.",
                "💾 Save your backup codes in a safe place - you'll need them if you lose your phone.",
                "⚠️ SMS-based 2FA is better than nothing, but authenticator apps are more secure.",
                "🔐 Most major services (Google, Facebook, Twitter, banks) offer 2FA - turn it on today!",
                "🛡️ Hardware security keys (YubiKey) provide the strongest form of 2FA protection."
            },
            ["backup"] = new List<string>
            {
                "💾 Follow the 3-2-1 rule: 3 copies of data, 2 different media types, 1 offsite backup.",
                "☁️ Use both cloud backup (Backblaze, iDrive) and local backup (external hard drive).",
                "🔄 Test your backups regularly - a backup is useless if you can't restore from it.",
                "📅 Set up automatic backups so you never forget to back up important files.",
                "💿 Keep one backup offline (disconnected from your computer) to protect against ransomware.",
                "📸 For photos, use multiple services (Google Photos, iCloud, plus an external drive)."
            },
            ["update"] = new List<string>
            {
                "⚡ Turn on automatic updates for your operating system - most security patches are released this way.",
                "📱 Don't ignore phone update notifications - they often fix critical security vulnerabilities.",
                "🔧 Set a weekly reminder to check for updates on all your devices and applications.",
                "🔄 Enable automatic updates for your web browser and browser extensions.",
                "⚠️ Zero-day exploits target unpatched systems - updating immediately reduces your risk.",
                "💻 Outdated software is the #1 way hackers gain access to computers - keep everything current!"
            },
            ["wifi"] = new List<string>
            {
                "📡 Change your router's default admin password immediately - default credentials are publicly known.",
                "🔒 Use WPA3 encryption if available, otherwise WPA2 - never use WEP or open networks.",
                "👥 Create a guest network for visitors and IoT devices like smart speakers and cameras.",
                "🕵️ Use a VPN on any public Wi-Fi network (coffee shops, airports, hotels).",
                "🚫 Disable WPS (Wi-Fi Protected Setup) - it has known security vulnerabilities.",
                "📱 Turn off auto-connect to Wi-Fi networks to avoid accidentally joining malicious hotspots."
            }
        };
        
        #endregion
        
        #region Follow-up Response Database
        
        public static readonly Dictionary<string, List<string>> DetailedExplanations = new Dictionary<string, List<string>>
        {
            ["password"] = new List<string>
            {
                "**Why strong passwords matter:**\nHackers use automated tools that can guess millions of passwords per second. A password like 'password123' can be cracked instantly, while 'Purple-Tiger-Dance-72!' would take centuries to crack.\n\n**How hackers crack passwords:**\n• Dictionary attacks (common words and phrases)\n• Brute force (trying every combination)\n• Credential stuffing (using passwords leaked from other sites)\n\n**The solution:** Use a password manager to generate and store unique, complex passwords for every site. You only need to remember one master password!",
                
                "**Creating strong passwords - practical guide:**\n\n✅ **DO:**\n• Use passphrases: 'Coffee-Morning-Rainbow-92!'\n• Mix character types: Aa1!@#$%\n• Make them long (12+ characters)\n\n❌ **DON'T:**\n• Use personal info (birthdays, names, addresses)\n• Use common substitutions (P@ssw0rd is still weak)\n• Reuse passwords across sites\n\n**Pro tip:** Password managers like Bitwarden, 1Password, or LastPass generate and fill passwords for you automatically!"
            },
            ["scam"] = new List<string>
            {
                "**Real-world scam examples:**\n\n📧 **Phishing Email:** 'Your PayPal account has been limited. Click here to verify your identity.' The link goes to a fake PayPal login page that steals your credentials.\n\n📞 **Phone Scam:** 'This is Microsoft Support. We detected a virus on your computer.' They ask for remote access and payment.\n\n💬 **Social Media Scam:** 'You won a prize! Click this link to claim it.' The link installs malware or steals information.\n\n**Red flags to always watch for:**\n• Unexpected urgency ('Act now or else!')\n• Requests for gift cards or wire transfers\n• Spelling and grammar mistakes\n• Pressure to keep it secret\n\n**What to do:** Stop communicating, verify through official channels, report to authorities (FTC, IC3).",
                
                "**How to verify if something is a scam:**\n\n1️⃣ **Don't use contact info from the message** - Scammers provide fake numbers/emails\n2️⃣ **Go directly to the official website** - Type the URL yourself, don't click links\n3️⃣ **Call the official number** - Found on your statement or the back of your card\n4️⃣ **Check with someone you trust** - Scammers rely on isolating you\n5️⃣ **Trust your gut** - If it feels wrong, it probably is\n\n**Remember:** Banks, government agencies, and legitimate companies will NEVER demand immediate payment via gift cards, wire transfer, or cryptocurrency."
            },
            ["privacy"] = new List<string>
            {
                "**Your data is valuable - here's who wants it:**\n\n📊 **Advertisers:** Track your browsing to show targeted ads\n🕵️ **Data brokers:** Collect and sell your information\n💻 **Hackers:** Want personal data for identity theft\n🏢 **Employers:** May monitor online activity\n\n**How to take back control:**\n\n✅ **Browser privacy:**\n• Use Firefox or Brave instead of Chrome\n• Install uBlock Origin and Privacy Badger\n• Use DuckDuckGo instead of Google\n\n✅ **Phone privacy:**\n• Review app permissions weekly\n• Disable location services for most apps\n• Use Signal for messaging\n\n✅ **Social media:**\n• Set profiles to private\n• Don't post location in real-time\n• Remove old posts regularly",
                
                "**Advanced privacy protection:**\n\n🌐 **VPN (Virtual Private Network):** Encrypts all your internet traffic and hides your IP address. Essential for public Wi-Fi.\n\n🔍 **Opt-out of data collection:**\n• Google: Turn off ad personalization\n• Facebook: Limit ad tracking\n• Amazon: Disable voice recording storage\n\n📧 **Email privacy tips:**\n• Use aliases for different services\n• Create a 'spam' email for newsletters\n• Use encrypted email (ProtonMail) for sensitive communications\n\n🔐 **Device security:**\n• Full disk encryption (BitLocker on Windows, FileVault on Mac)\n• Lock your screen when away\n• Use a privacy screen filter in public\n\n**Remember:** Privacy isn't about having something to hide - it's about having something to protect!"
            }
        };
        
        #endregion
        
        #region Example Database
        
        public static readonly Dictionary<string, List<string>> Examples = new Dictionary<string, List<string>>
        {
            ["password"] = new List<string>
            {
                "**❌ Bad password example:** 'Fluffy2024'\n- Contains a pet name (easily guessed from social media)\n- Uses the current year (common pattern)\n- Only 9 characters (too short)\n- No special characters\n\n**✅ Good password example:** 'Purple-Tiger-Dance-72!'\n- 22 characters (very strong)\n- Uses random words you can remember\n- Includes numbers and symbols\n- No personal information\n\n**Even better:** Let a password manager generate 'xK9#mP2$vL7@qR4!wN6&' for you!",
                
                "**Real password cracking demonstration:**\n\nPassword: 'Summer2024'\nTime to crack: ~30 seconds ❌\n\nPassword: 'PurpleMonkeyDishwasher!'\nTime to crack: ~500 years ✅\n\nPassword: 'P@ssw0rd123'\nTime to crack: ~10 minutes ❌\n\n**The difference?** Length beats complexity! A long, easy-to-remember passphrase is more secure than a short, complex password with substitutions hackers already know."
            },
            ["scam"] = new List<string>
            {
                "**Example: Spotting a phishing email**\n\n📧 **Fake email you receive:**\n*From: security@paypa1.com* (notice the '1' instead of 'l')\nSubject: **URGENT: Account suspended**\n\n'Dear customer,\n\nYour PayPal account has been suspended due to suspicious activity. Click the link below to verify your identity immediately or your account will be permanently closed.\n\n[Verify Account Now]\n\nThank you,\nPayPal Security'\n\n**Red flags spotted:**\n❌ Wrong domain (paypa1.com vs paypal.com)\n❌ Urgent/threatening language\n❌ Generic greeting ('Dear customer' not your name)\n❌ Link to fake login page\n\n**What to do:**\n1. Don't click the link\n2. Open browser and type 'paypal.com' directly\n3. Check your account status from there\n4. Report the phishing email to PayPal",
                
                "**Example: Tech support scam call**\n\n📞 **The call:** 'Hi, this is David from Windows Technical Support. We've detected multiple viruses coming from your computer. I need you to go to a website and download software so we can fix it.'\n\n**Why it's a scam:**\n• Microsoft doesn't make unsolicited support calls\n• They can't detect viruses remotely without software installed\n• 'Windows Technical Support' isn't a real department\n\n**What happens if you comply:**\n1. You download remote access software\n2. They 'find' fake problems\n3. They demand $300-500 for unnecessary 'repairs'\n4. They may install actual malware\n5. They have access to your computer anytime\n\n**What to do:** Hang up immediately. Real tech support only happens when YOU contact them through official channels."
            },
            ["privacy"] = new List<string>
            {
                "**Example: App permissions to watch for**\n\n📱 **Flashlight App asking for:**\n✓ Camera access? ❌ (Why?)\n✓ Contacts access? ❌ (Why?)\n✓ Location access? ❌ (Why?)\n\nA flashlight only needs... flashlight access! Any app asking for unrelated permissions is a privacy red flag.\n\n**Another example - Weather App:**\n✓ Location (makes sense for local weather)\n❌ Microphone (Why does weather need mic access?)\n❌ Contacts (Definitely not)\n\n**How to check permissions:**\n• Android: Settings → Apps → [App Name] → Permissions\n• iPhone: Settings → Privacy → [App Name]\n\n**Rule of thumb:** If the permission doesn't make sense for the app's function, deny it or uninstall the app!",
                
                "**Example: Social media privacy check**\n\n**Before posting anything, ask:**\n\n❓ Would I want my grandmother to see this?\n❓ Would I want my boss to see this?\n❓ Would I want a stranger to know this?\n❓ Could this be used to steal my identity?\n\n**Real example - What NOT to post:**\n❌ 'At the airport, heading to Hawaii for 2 weeks!'\n→ Your house is empty - invitation for burglars\n\n**What TO do instead:**\n✅ 'Had an amazing time in Hawaii!' (post after returning)\n\n**Photo privacy tips:**\n• Remove EXIF data (GPS coordinates) from photos before posting\n• Don't post your boarding pass (barcode contains personal info)\n• Blur or crop out:\n  - License plates\n  - House numbers\n  - Work ID badges\n  - Credit cards\n  - Children's school uniforms (shows school name)"
            }
        };
        
        #endregion
        
        #region Encouragement Messages
        
        public static readonly List<string> EncouragementMessages = new List<string>
        {
            "💪 You're doing great! Every cybersecurity tip you learn makes you safer online.",
            "⭐ Excellent question! Staying curious about security is the best defense.",
            "🛡️ Keep learning - you're building valuable skills to protect yourself!",
            "🎉 That's a smart question to ask! Most people never think about this stuff.",
            "🌟 You're ahead of 90% of people just by asking these questions!",
            "🔐 Every security step you take makes hackers' jobs harder. Keep going!",
            "💡 Great thinking! This is exactly the kind of awareness that prevents breaches."
        };
        
        #endregion
        
        #region Greeting and Farewell Collections
        
        public static readonly List<string> Greetings = new List<string>
        {
            "Hello! 👋 I'm your Cybersecurity Awareness Assistant. How can I help protect you today?",
            "Welcome back! 🛡️ Ready to learn about staying safe online?",
            "Hi there! 👨‍💻 Ask me anything about passwords, privacy, or avoiding scams!",
            "Greetings! 🔐 I'm here to help you stay secure online. What would you like to learn about?",
            "Hey! 👋 Concerned about online security? I've got tips that can help!"
        };
        
        public static readonly List<string> Farewells = new List<string>
        {
            "Stay safe online! 🔐 Remember: cybersecurity is everyone's responsibility.",
            "Goodbye! 👋 Keep those passwords strong and stay vigilant!",
            "Take care! 🛡️ Don't forget to enable 2FA on your important accounts.",
            "Until next time! 💪 Stay secure and keep learning about online safety.",
            "Be safe out there! 🔒 And remember - when in doubt, don't click!"
        };
        
        public static readonly List<string> FallbackResponses = new List<string>
        {
            "That's interesting! I specialize in cybersecurity topics like password safety, scam prevention, and privacy protection. What would you like to know?",
            "I'm here to help with cybersecurity awareness! Try asking me about passwords, scams, privacy, or malware protection.",
            "Great question! To better assist you, could you tell me more about your cybersecurity concern?",
            "I want to help! Would you like tips on passwords, avoiding scams, or protecting your privacy?",
            "Hmm, I'm not sure about that. But I can definitely help with cybersecurity - ask me about passwords, 2FA, or avoiding phishing scams!"
        };
        
        #endregion
        
        #region Sentiment-Specific Responses
        
        public static readonly Dictionary<string, List<string>> SentimentResponses = new Dictionary<string, List<string>>
        {
            ["worried"] = new List<string>
            {
                "I completely understand your concern. Cybersecurity can feel overwhelming, but you're taking the right first step by learning about it.",
                "It's normal to feel worried - online threats are real. But knowledge is power, and I'm here to help you stay protected.",
                "Your concern is valid! Let me share some practical steps that will help you feel more in control."
            },
            ["curious"] = new List<string>
            {
                "Great curiosity! That's exactly the right attitude for staying secure online.",
                "I love that you're asking questions! The more you learn, the safer you'll be.",
                "Excellent question! Curiosity about security is what separates safe users from vulnerable ones."
            },
            ["frustrated"] = new List<string>
            {
                "I hear your frustration. Security can sometimes feel like it gets in the way, but let me help simplify this for you.",
                "I understand it's frustrating. Let me break this down into simpler, actionable steps.",
                "You're right that security can be annoying - but these small inconveniences prevent major headaches later."
            }
        };
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Get random response from a list
        /// </summary>
        public static string GetRandomResponse(List<string> responses)
        {
            if (responses == null || responses.Count == 0)
                return null;
            
            var random = new Random();
            return responses[random.Next(responses.Count)];
        }
        
        /// <summary>
        /// Get random tip for a specific keyword
        /// </summary>
        public static string GetRandomTip(string keyword)
        {
            if (CybersecurityTips.ContainsKey(keyword))
                return GetRandomResponse(CybersecurityTips[keyword]);
            return null;
        }
        
        /// <summary>
        /// Get detailed explanation for a topic
        /// </summary>
        public static string GetDetailedExplanation(string topic)
        {
            if (DetailedExplanations.ContainsKey(topic))
                return GetRandomResponse(DetailedExplanations[topic]);
            return null;
        }
        
        /// <summary>
        /// Get example for a topic
        /// </summary>
        public static string GetExample(string topic)
        {
            if (Examples.ContainsKey(topic))
                return GetRandomResponse(Examples[topic]);
            return null;
        }
        
        /// <summary>
        /// Get random encouragement message
        /// </summary>
        public static string GetRandomEncouragement()
        {
            return GetRandomResponse(EncouragementMessages);
        }
        
        /// <summary>
        /// Get random greeting
        /// </summary>
        public static string GetRandomGreeting()
        {
            return GetRandomResponse(Greetings);
        }
        
        /// <summary>
        /// Get random farewell
        /// </summary>
        public static string GetRandomFarewell()
        {
            return GetRandomResponse(Farewells);
        }
        
        /// <summary>
        /// Get random fallback response
        /// </summary>
        public static string GetRandomFallback()
        {
            return GetRandomResponse(FallbackResponses);
        }
        
        /// <summary>
        /// Get sentiment-specific response
        /// </summary>
        public static string GetSentimentResponse(string sentiment)
        {
            if (SentimentResponses.ContainsKey(sentiment))
                return GetRandomResponse(SentimentResponses[sentiment]);
            return null;
        }
        
        #endregion
    }
}