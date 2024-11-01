namespace InMemoryCache
{
    public class CacheValue
    {
        public string Value { get; set; }

        private int Frequency;
        private DateTime LastAccessed;

        public int GetFrequency()
        {
            return Frequency;
        }
        public DateTime GetLastAccessedTime()
        {
            return LastAccessed;
        }

        public void IncrementFrequency()
        {
            Frequency++;
        }
        public void UpdateLastAccessedTime()
        {
            LastAccessed = DateTime.UtcNow;
        }
    }

}