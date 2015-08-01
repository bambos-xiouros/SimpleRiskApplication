using System.Configuration;

namespace SimpleRiskApplication.Config
{
    public class DataProviderSectionGroup : ConfigurationSectionGroup
    {
        [ConfigurationProperty("randomBetDataProvider", IsRequired = true)]
        public RandomBetDataProviderSection RandomBetDataProvider => (RandomBetDataProviderSection)Sections["randomBetDataProvider"];
    }
}