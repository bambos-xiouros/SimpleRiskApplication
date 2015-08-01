using System;
using System.Configuration;

namespace SimpleRiskApplication.Config
{
    [ConfigurationCollection(typeof(CsvBetDataProviders), AddItemName = "CsvBetDataProvider", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class CsvBetDataProviders : ConfigurationElementCollection
    {
        public CsvBetDataProvider this[int index]
        {
            get
            {
                return BaseGet(index) as CsvBetDataProvider;
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

        public new CsvBetDataProvider this[string responseString]
        {
            get { return (CsvBetDataProvider)BaseGet(responseString); }
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
            return new CsvBetDataProvider();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CsvBetDataProvider)element).File;
        }
    }
}