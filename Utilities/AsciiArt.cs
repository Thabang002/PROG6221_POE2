using System;

namespace CybersecurityChatbot.Utilities
{
    public static class AsciiArt
    {
        public static string GetCyberSecurityArt()
        {
            return @"
    ╔══════════════════════════════════════════════════════════════════════════════╗
    ║                         🛡️  CYBERSECURITY AWARENESS  🛡️                         ║
    ║                                                                              ║
    ║       ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄     ║
    ║      ▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌    ║
    ║      ▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀█░▌    ║
    ║      ▐░▌       ▐░▌▐░▌       ▐░▌▐░▌       ▐░▌▐░▌          ▐░▌       ▐░▌    ║
    ║      ▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄█░▌    ║
    ║      ▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌    ║
    ║      ▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀█░▌    ║
    ║      ▐░▌          ▐░▌       ▐░▌▐░▌          ▐░▌          ▐░▌       ▐░▌    ║
    ║      ▐░▌          ▐░▌       ▐░▌▐░▌          ▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄█░▌    ║
    ║      ▐░▌          ▐░▌       ▐░▌▐░▌          ▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌    ║
    ║       ▀            ▀         ▀  ▀            ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀     ║
    ║                                                                              ║
    ║                   『 STAY SAFE  ◆  STAY SECURE  ◆  STAY VIGILANT 』            ║
    ║                                                                              ║
    ║              ╔════════════════════════════════════════════════╗              ║
    ║              ║  🔐  Password Security  │  ⚠️  Scam Prevention  ║              ║
    ║              ║  🛡️  Privacy Protection │  🦠  Malware Defense  ║              ║
    ║              ║  🔑  Two-Factor Auth    │  📡  Wi-Fi Security   ║              ║
    ║              ╚════════════════════════════════════════════════╝              ║
    ║                                                                              ║
    ║                         Type 'help' for topics or ask away!                   ║
    ╚══════════════════════════════════════════════════════════════════════════════╝";
        }
        
        public static string GetSmallShieldArt()
        {
            return @"
          .--.
         /    \
        /      \
       |  🛡️   |
       |      |
        \    /
         \  /
          '--'";
        }
        
        public static string GetLockArt()
        {
            return @"
         .---.
        |     |
        |  🔒  |
        |     |
         '---'";
        }
        
        public static string GetWarningArt()
        {
            return @"
          ⚠️
         /   \
        | !!! |
         \   /
          ‾‾‾";
        }
        
        public static string GetTopicHeader(string topic)
        {
            var header = topic.ToUpper();
            var border = new string('═', header.Length + 4);
            
            return $@"
    ╔{border}╗
    ║  {header}  ║
    ╚{border}╝";
        }
    }
}