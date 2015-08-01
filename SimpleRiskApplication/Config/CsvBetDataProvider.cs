using System.Configuration;

namespace SimpleRiskApplication.Config
{
    public class CsvBetDataProvider : ConfigurationSection
    {
        [ConfigurationProperty("file", IsRequired = true, IsKey = true)]
        public string File
        {
            get
            {
                return (string)this["file"];
            }
            set
            {
                this["file"] = value;
            }
        }

        [ConfigurationProperty("settled", IsRequired = true)]
        public bool Settled
        {
            get
            {
                return (bool)this["settled"];
            }
            set
            {
                this["settled"] = value;
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