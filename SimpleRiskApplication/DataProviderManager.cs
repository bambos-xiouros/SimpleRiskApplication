using System;
using System.Collections.Generic;
using System.Configuration;
using BetDataAcquisition;
using SimpleRiskApplication.Config;
using SimpleRiskApplication.ViewModel;

namespace SimpleRiskApplication
{
    internal class DataProviderManager
    {
        private readonly List<BetDataProvider> _betDataProviders = new List<BetDataProvider>();
        private readonly BetDataProviderFactory _betDataProviderFactory;
        private readonly IBetViewModels _betViewModels;

        public DataProviderManager(BetDataProviderFactory betDataProviderFactory, IBetViewModels betViewModels)
        {
            _betDataProviderFactory = betDataProviderFactory;
            _betViewModels = betViewModels;
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
                betDataProvider.BetsProvided += BetDataProviderOnBetsProvided;
                betDataProvider.Start();
            }
        }

        public void StopAllDataProviders()
        {
            foreach (var betDataProvider in _betDataProviders)
            {
                betDataProvider.BetsProvided -= BetDataProviderOnBetsProvided;
                betDataProvider.Stop();
            }
        }

        private void BetDataProviderOnBetsProvided(object sender, BetsProvidedEventArgs betsProvidedEventArgs)
        {
            _betViewModels.AddRange(betsProvidedEventArgs.Bets);
        }
    }
}
