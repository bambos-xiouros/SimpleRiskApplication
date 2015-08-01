using BetDataAcquisition.Providers;

namespace BetDataAcquisition
{
    public class BetDataProviderFactory
    {
        public BetDataProvider CreateRandomBetDataProvider(int numberOfBets, int maxBetBatchSize)
        {
            var randomBetDataProvider = new RandomBetDataProvider(numberOfBets, maxBetBatchSize);
            return randomBetDataProvider;
        }

        public BetDataProvider CreateCsvBetDataProvider(string file, bool settled, int maxBetBatchSize)
        {
            BetDataProvider csvBetDataProvider;
            if (settled)
            {
                csvBetDataProvider = new CsvSettledBetDataProvider(file, maxBetBatchSize);
            }
            else
            {
                csvBetDataProvider = new CsvUnsettledBetDataProvider(file, maxBetBatchSize);
            }
            return csvBetDataProvider;
        }
    }
}
