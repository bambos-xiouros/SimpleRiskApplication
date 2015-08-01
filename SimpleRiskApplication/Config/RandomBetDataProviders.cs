using System.Configuration;

namespace SimpleRiskApplication.Config
{
    [ConfigurationCollection(typeof(RandomBetDataProviders), AddItemName = "RandomBetDataProvider", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class RandomBetDataProviders : ConfigurationElementCollection
    {
        public RandomBetDataProvider this[int index]
        {
            get
            {
                return BaseGet(index) as RandomBetDataProvider;
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new RandomBetDataProvider this[string responseString]
        {
            get { return (RandomBetDataProvider)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RandomBetDataProvider();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RandomBetDataProvider)element).Id;
        }
    }
}