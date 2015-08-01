using System;
using System.Collections.Generic;
using System.Configuration;
using BetDataAcquisition;
using SimpleRiskApplication.Config;

namespace SimpleRiskApplication
{
    internal class DataProviderManager
    {
        private readonly List<BetDataProvider> _betDataProviders = new List<BetDataProvider>();
        private readonly BetDataProviderFactory _betDataProviderFactory;

        public DataProviderManager(BetDataProviderFactory betDataProviderFactory)
        {
            _betDataProviderFactory = betDataProviderFactory;
        }

        public void CreateDataProvidersFromConfig()
        {
            var dataProviderSectionGroup = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).SectionGroups["DataProviderGroup"] as DataProviderSectionGroup;
            if (dataProviderSectionGroup != null)
            {
                foreach (var dataProviderSection in dataProviderSectionGroup.Sections)
                {
                    BetDataProvider betDataProvider = null;
                    if (dataProviderSection is RandomBetDataProviderSection)
                    {
                        var randomBetDataProviderSection = dataProviderSection as RandomBetDataProviderSection;
                        betDataProvider = _betDataProviderFactory.CreateRandomBetDataProvider(randomBetDataProviderSection.NumberOfBets, randomBetDataProviderSection.MaxBetBatchSize);
                    }

                    if (betDataProvider == null)
                    {
                        Console.WriteLine("Unknown DataProviderType " + dataProviderSection);
                    }
                    else
                    {
                        _betDataProviders.Add(betDataProvider);
                    }
                }
            }
        }

        public void StartAllDataProviders()
        {
            foreach (var betDataProvider in _betDataProviders)
            {
                betDataProvider.Start();
            }
        }

        public void StopAllDataProviders()
        {
            foreach (var betDataProvider in _betDataProviders)
            {
                betDataProvider.Stop();
            }
        }
    }
}
