using BetDataAcquisition.Cache;

namespace BetDataAcquisition
{
    public class BetDataCacheFactory
    {
        public IBetDataCache CreateInMemoryBetDataCache()
        {
            return new InMemoryConcurrentGetDataCache();
        }
    }
}