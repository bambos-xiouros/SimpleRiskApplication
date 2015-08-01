using System.Configuration;

namespace SimpleRiskApplication.Config
{
    public class RandomBetDataProvider : ConfigurationSection
    {
        [ConfigurationProperty("id", IsRequired = true, IsKey = true)]
        public int Id
        {
            get
            {
                return (int)this["id"];
            }
            set
            {
                this["id"] = value;
            }
        }

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
