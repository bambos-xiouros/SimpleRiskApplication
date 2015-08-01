using System.Security.Cryptography.X509Certificates;

namespace BetDataAcquisition
{
    public class BetDataProviderFactory
    {
        public BetDataProvider CreateRandomBetDataProvider(int numberOfBets, int maxBetBatchSize)
        {
            var randomBetDataProvider = new RandomBetDataProvider(numberOfBets, maxBetBatchSize);
            return randomBetDataProvider;
        }
    }
}
