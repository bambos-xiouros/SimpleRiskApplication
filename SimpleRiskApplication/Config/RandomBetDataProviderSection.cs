using System.Configuration;

namespace SimpleRiskApplication.Config
{
    public class RandomBetDataProviderSection : ConfigurationSection
    {
        [ConfigurationProperty("numberOfBets", IsRequired = true)]
        public int NumberOfBets
        {
            get
            {
                return (int)this["numberOfBets"];
            }
            set
            {
                this["numberOfBets"] = value;
            }
        }

        [ConfigurationProperty("maxBetBatchSize", IsRequired = false)]
        public int MaxBetBatchSize
        {
            get
            {
                return (int)this["maxBetBatchSize"];
            }
            set
            {
                this["maxBetBatchSize"] = value;
            }
        }
    }
}
