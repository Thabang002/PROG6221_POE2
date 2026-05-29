using System;
using System.Collections.Generic;

namespace CybersecurityChatbot.Services
{
    // Generic collection cache for performance optimization
    public class ResponseCache<T>
    {
        private readonly Dictionary<string, T> _cache;
        private readonly int _maxSize;
        private readonly Queue<string> _accessOrder;
        
        public ResponseCache(int maxSize = 100)
        {
            _cache = new Dictionary<string, T>();
            _maxSize = maxSize;
            _accessOrder = new Queue<string>();
        }
        
        public void Add(string key, T value)
        {
            if (_cache.Count >= _maxSize)
            {
                // Remove least recently used
                var oldest = _accessOrder.Dequeue();
                _cache.Remove(oldest);
            }
            
            if (!_cache.ContainsKey(key))
            {
                _cache.Add(key, value);
                _accessOrder.Enqueue(key);
            }
        }
        
        public bool TryGet(string key, out T value)
        {
            if (_cache.TryGetValue(key, out value))
            {
                // Update access order - move to end
                var newQueue = new Queue<string>();
                foreach (var item in _accessOrder)
                {
                    if (item != key)
                        newQueue.Enqueue(item);
                }
                newQueue.Enqueue(key);
                
                // Reassign
                while (_accessOrder.Count > 0)
                    _accessOrder.Dequeue();
                foreach (var item in newQueue)
                    _accessOrder.Enqueue(item);
                    
                return true;
            }
            
            return false;
        }
        
        public void Clear()
        {
            _cache.Clear();
            _accessOrder.Clear();
        }
        
        public int Size => _cache.Count;
    }
}