using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using BetDataAcquisition;

namespace SimpleRiskApplication.Config
{
    internal class BetDataProviderConfigReader
    {
        private readonly BetDataProviderFactory _betDataProviderFactory;

        public BetDataProviderConfigReader(BetDataProviderFactory betDataProviderFactory)
        {
            _betDataProviderFactory = betDataProviderFactory;
        }

        public IEnumerable<BetDataProvider> CreateBetDataProvidersFromConfig()
        {
            var betDataProviders = new List<BetDataProvider>();
            var betDataProvidersConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).Sections["BetDataProviders"] as BetDataProviders;
            if (betDataProvidersConfig != null)
            {
                betDataProviders.AddRange(CreateBetDataProviders(betDataProvidersConfig.CsvBetDataProviders));
                betDataProviders.AddRange(CreateBetDataProviders(betDataProvidersConfig.RandomBetDataProviders));
            }
            return betDataProviders;
        }

        private IEnumerable<BetDataProvider> CreateBetDataProviders(IEnumerable configurationElementCollection)
        {
            var betDataProviders = new List<BetDataProvider>();
            foreach (var csvBetDataProvider in configurationElementCollection)
            {
                var betDataProvider = GetDataProviderSection(csvBetDataProvider);
                if (betDataProvider == null)
                {
                    Console.WriteLine("Unknown BetDataProviderType " + betDataProvider);
                }
                else
                {
                    betDataProviders.Add(betDataProvider);
                }
            }
            return betDataProviders;
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
    }
}