using System.Collections.Concurrent;

namespace InMemoryCache
{
    public class Cache
    {
        private ConcurrentDictionary<CacheKey, CacheValue> cache;
        private int TimeToLive;
        private int MaxSize;

        public Cache()
        {
            cache?.Clear();
            cache = new ConcurrentDictionary<CacheKey, CacheValue>();
        }

        public CacheValue Get(CacheKey cacheKey)
        {
            if(cache.ContainsKey(cacheKey))
            {
                cache[cacheKey].UpdateLastAccessedTime();
                cache[cacheKey].IncrementFrequency();
                return cache[cacheKey];
            }
            
            return null;
        }

        public void Put(CacheKey key, CacheValue value)
        {

        }

        public void Remove()
        {

        }
    }
}