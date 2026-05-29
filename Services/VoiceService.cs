using System;
using System.Threading.Tasks;

namespace CybersecurityChatbot.Services
{
    public class VoiceService
    {
        private object? _speechSynthesizer;
        private Type? _synthType;
        private bool _isEnabled;
        private bool _isSpeaking;

        public VoiceService()
        {
            try
            {
                // Try to load System.Speech.Synthesis.SpeechSynthesizer via reflection.
                _synthType = Type.GetType("System.Speech.Synthesis.SpeechSynthesizer, System.Speech");
                if (_synthType != null)
                {
                    _speechSynthesizer = Activator.CreateInstance(_synthType);
                    _synthType.GetMethod("SetOutputToDefaultAudioDevice")?.Invoke(_speechSynthesizer, null);
                    var rateProp = _synthType.GetProperty("Rate");
                    var volProp = _synthType.GetProperty("Volume");
                    rateProp?.SetValue(_speechSynthesizer, 0);
                    volProp?.SetValue(_speechSynthesizer, 100);
                    _isEnabled = true;
                    _isSpeaking = false;
                }
                else
                {
                    _isEnabled = false;
                }
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
                var cleanText = System.Text.RegularExpressions.Regex.Replace(text, @"[^\\w\\s\\.\\,\\!\\?]", "");

                // Use Task to avoid blocking UI; call Speak via reflection
                await Task.Run(() =>
                {
                    var speakMethod = _synthType?.GetMethod("Speak");
                    speakMethod?.Invoke(_speechSynthesizer, new object[] { cleanText });
                });
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
                var cleanText = System.Text.RegularExpressions.Regex.Replace(text, @"[^\\w\\s\\.\\,\\!\\?]", "");
                var speakAsyncMethod = _synthType?.GetMethod("SpeakAsync", new Type[] { typeof(string) });
                speakAsyncMethod?.Invoke(_speechSynthesizer, new object[] { cleanText });
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
                try
                {
                    var cancelMethod = _synthType?.GetMethod("SpeakAsyncCancelAll");
                    cancelMethod?.Invoke(_speechSynthesizer, null);
                }
                catch { }
                _isSpeaking = false;
            }
        }

        public bool IsVoiceEnabled => _isEnabled;

        public void SetVoiceRate(int rate)
        {
            if (_speechSynthesizer != null && _synthType != null)
            {
                rate = Math.Clamp(rate, -10, 10);
                var rateProp = _synthType.GetProperty("Rate");
                rateProp?.SetValue(_speechSynthesizer, rate);
            }
        }

        public void SetVoiceVolume(int volume)
        {
            if (_speechSynthesizer != null && _synthType != null)
            {
                volume = Math.Clamp(volume, 0, 100);
                var volProp = _synthType.GetProperty("Volume");
                volProp?.SetValue(_speechSynthesizer, volume);
            }
        }
    }
}
