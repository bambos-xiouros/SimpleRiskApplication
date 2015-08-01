using System.Configuration;

namespace SimpleRiskApplication.Config
{
    public class BetDataProviders : ConfigurationSection
    {
        [ConfigurationProperty("CsvBetDataProviders")]
        public CsvBetDataProviders CsvBetDataProviders
        {
            get
            {
                return (CsvBetDataProviders)this["CsvBetDataProviders"];
            }
        }

        [ConfigurationProperty("RandomBetDataProviders")]
        public RandomBetDataProviders RandomBetDataProviders
        {
            get
            {
                return (RandomBetDataProviders)this["RandomBetDataProviders"];
            }
        }
    }
}