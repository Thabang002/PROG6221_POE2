using System;
using System.IO;
using System.Text;

namespace CybersecurityChatbot.Utilities
{
    /// <summary>
    /// Centralized error handling for professional-grade application
    /// Prevents crashes and logs errors for debugging
    /// </summary>
    public static class ErrorHandler
    {
        private static readonly string LogFilePath = "error_log.txt";
        private static readonly object _lockObject = new object();
        
        /// <summary>
        /// Safely executes an action with automatic error handling
        /// </summary>
        public static bool SafeExecute(Action action, string errorContext = "")
        {
            try
            {
                action?.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex, errorContext);
                return false;
            }
        }
        
        /// <summary>
        /// Safely executes a function with automatic error handling
        /// </summary>
        public static T SafeExecute<T>(Func<T> func, T defaultValue = default, string errorContext = "")
        {
            try
            {
                return func != null ? func() : defaultValue;
            }
            catch (Exception ex)
            {
                LogError(ex, errorContext);
                return defaultValue;
            }
        }
        
        /// <summary>
        /// Logs error to file for debugging
        /// </summary>
        private static void LogError(Exception ex, string context)
        {
            lock (_lockObject)
            {
                try
                {
                    var logEntry = new StringBuilder();
                    logEntry.AppendLine($"═══════════════════════════════════════════════════════════");
                    logEntry.AppendLine($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    logEntry.AppendLine($"Context: {(string.IsNullOrEmpty(context) ? "General" : context)}");
                    logEntry.AppendLine($"Error Type: {ex.GetType().Name}");
                    logEntry.AppendLine($"Message: {ex.Message}");
                    logEntry.AppendLine($"Stack Trace: {ex.StackTrace}");
                    
                    if (ex.InnerException != null)
                    {
                        logEntry.AppendLine($"Inner Error: {ex.InnerException.Message}");
                    }
                    
                    logEntry.AppendLine();
                    
                    File.AppendAllText(LogFilePath, logEntry.ToString(), Encoding.UTF8);
                }
                catch
                {
                    // Cannot log error - fail silently to prevent cascading failures
                }
            }
        }
        
        /// <summary>
        /// Validates user input for safety
        /// </summary>
        public static bool IsValidInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
                
            // Prevent excessively long inputs (DoS protection)
            if (input.Length > 1000)
                return false;
                
            // Allow Unicode characters but filter control characters
            foreach (char c in input)
            {
                if (char.IsControl(c) && c != '\n' && c != '\r' && c != '\t')
                    return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// Sanitizes user input for safe display
        /// </summary>
        public static string SanitizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
                
            // Trim and limit length
            input = input.Trim();
            if (input.Length > 500)
                input = input.Substring(0, 500);
                
            // Remove any potential dangerous characters
            var sanitized = new StringBuilder();
            foreach (char c in input)
            {
                if (!char.IsControl(c) || c == '\n' || c == '\r')
                    sanitized.Append(c);
            }
            
            return sanitized.ToString();
        }
        
        /// <summary>
        /// Gets user-friendly error message
        /// </summary>
        public static string GetUserFriendlyMessage(string errorType)
        {
            return errorType?.ToLower() switch
            {
                "network" => "Network connection issue detected. Please check your internet and try again.",
                "speech" => "Voice feature is unavailable on this system. You can still type your messages!",
                "file" => "Unable to access saved data. The application will continue to work normally.",
                "timeout" => "The operation took too long. Please try again.",
                _ => "Something unexpected happened. Please try again or rephrase your question."
            };
        }
        
        /// <summary>
        /// Creates a safe fallback response when errors occur
        /// </summary>
        public static string GetFallbackResponse()
        {
            var fallbacks = new[]
            {
                "I'm here to help with cybersecurity! Could you please rephrase your question?",
                "Let me try that again. What would you like to know about online safety?",
                "I want to help you stay secure online. Could you ask your question another way?",
                "I'm ready to help with passwords, scams, privacy, or any cybersecurity topic!"
            };
            
            var random = new Random();
            return fallbacks[random.Next(fallbacks.Length)];
        }
        
        /// <summary>
        /// Checks if the application is in a valid state
        /// </summary>
        public static bool ValidateApplicationState()
        {
            try
            {
                // Check if we can write to temp directory (basic permissions check)
                string tempPath = Path.GetTempPath();
                string testFile = Path.Combine(tempPath, "cyber_test.txt");
                File.WriteAllText(testFile, "test");
                File.Delete(testFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Gets error log contents for debugging
        /// </summary>
        public static string GetErrorLog()
        {
            if (File.Exists(LogFilePath))
            {
                try
                {
                    return File.ReadAllText(LogFilePath);
                }
                catch
                {
                    return "Unable to read error log.";
                }
            }
            return "No errors logged.";
        }
        
        /// <summary>
        /// Clears the error log
        /// </summary>
        public static void ClearErrorLog()
        {
            try
            {
                if (File.Exists(LogFilePath))
                    File.Delete(LogFilePath);
            }
            catch
            {
                // Silently fail
            }
        }
    }
}