using System;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace CybersecurityChatbot.Services
{
    public class VoiceService
    {
        private SpeechSynthesizer _speechSynthesizer;
        private bool _isEnabled;
        private bool _isSpeaking;
        
        public VoiceService()
        {
            try
            {
                _speechSynthesizer = new SpeechSynthesizer();
                _speechSynthesizer.SetOutputToDefaultAudioDevice();
                _speechSynthesizer.Rate = 0;
                _speechSynthesizer.Volume = 100;
                _isEnabled = true;
                _isSpeaking = false;
            }
            catch (Exception)
            {
                // Speech not available on all systems
                _isEnabled = false;
            }
        }
        
        public async Task SpeakAsync(string text)
        {
            if (!_isEnabled || _isSpeaking || string.IsNullOrWhiteSpace(text))
                return;
                
            try
            {
                _isSpeaking = true;
                var cleanText = System.Text.RegularExpressions.Regex.Replace(text, @"[^\w\s\.\,\!\?]", "");
                
                // Use Task to avoid blocking UI
                await Task.Run(() => _speechSynthesizer.Speak(cleanText));
            }
            catch (Exception)
            {
                // Silently fail - voice is optional feature
            }
            finally
            {
                _isSpeaking = false;
            }
        }
        
        public void Speak(string text)
        {
            if (!_isEnabled || _isSpeaking || string.IsNullOrWhiteSpace(text))
                return;
                
            try
            {
                _isSpeaking = true;
                var cleanText = System.Text.RegularExpressions.Regex.Replace(text, @"[^\w\s\.\,\!\?]", "");
                _speechSynthesizer.SpeakAsync(cleanText);
            }
            catch (Exception)
            {
                // Silently fail
            }
            finally
            {
                // Don't set _isSpeaking false immediately for async
                Task.Delay(100).ContinueWith(_ => _isSpeaking = false);
            }
        }
        
        public void ToggleVoice()
        {
            _isEnabled = !_isEnabled;
            if (!_isEnabled && _isSpeaking)
            {
                _speechSynthesizer?.SpeakAsyncCancelAll();
                _isSpeaking = false;
            }
        }
        
        public bool IsVoiceEnabled => _isEnabled;
        
        public void SetVoiceRate(int rate)
        {
            if (_speechSynthesizer != null)
            {
                rate = Math.Clamp(rate, -10, 10);
                _speechSynthesizer.Rate = rate;
            }
        }
        
        public void SetVoiceVolume(int volume)
        {
            if (_speechSynthesizer != null)
            {
                volume = Math.Clamp(volume, 0, 100);
                _speechSynthesizer.Volume = volume;
            }
        }
    }
}