using System;
using System.Collections;
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
            var betDataProviders = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).Sections["BetDataProviders"] as BetDataProviders;
            if (betDataProviders != null)
            {
                ProcessBetDataProviders(betDataProviders.CsvBetDataProviders);
                ProcessBetDataProviders(betDataProviders.RandomBetDataProviders);
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

        private void ProcessBetDataProviders(IEnumerable configurationElementCollection)
        {
            foreach (var csvBetDataProvider in configurationElementCollection)
            {
                var betDataProvider = GetDataProviderSection(csvBetDataProvider);
                if (betDataProvider == null)
                {
                    Console.WriteLine("Unknown BetDataProviderType " + betDataProvider);
                }
                else
                {
                    _betDataProviders.Add(betDataProvider);
                }
            }
        }

        private BetDataProvider GetDataProviderSection(object dataProviderSection)
        {
            BetDataProvider betDataProvider = null;
            if (dataProviderSection is RandomBetDataProvider)
            {
                var randomBetDataProviderSection = dataProviderSection as RandomBetDataProvider;
                betDataProvider = _betDataProviderFactory.CreateRandomBetDataProvider(randomBetDataProviderSection.NumberOfBets, randomBetDataProviderSection.MaxBetBatchSize);
            }
            else if (dataProviderSection is CsvBetDataProvider)
            {
                var csvBetDataProviderSection = dataProviderSection as CsvBetDataProvider;
                betDataProvider = _betDataProviderFactory.CreateCsvBetDataProvider(csvBetDataProviderSection.File, csvBetDataProviderSection.Settled, csvBetDataProviderSection.MaxBetBatchSize);
            }
            return betDataProvider;
        }

        private void BetDataProviderOnBetsProvided(object sender, BetsProvidedEventArgs betsProvidedEventArgs)
        {
            _betViewModels.AddRange(betsProvidedEventArgs.Bets);
        }
    }
}
