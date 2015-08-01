using System.Security.Cryptography.X509Certificates;

namespace BetDataAcquisition
{
    public class BetDataProviderFactory
    {
        public BetDataProvider CreateRandonBetDataProvider(float betsCreatedPerSecond)
        {
            var randomBetDataProvider = new RandomBetDataProvider(betsCreatedPerSecond);
            return randomBetDataProvider;
        }
    }
}
