using System.Collections.Generic;
using BetDataAcquisition;

namespace SimpleRiskApplication.Data
{
    internal class BetDataProviderManager
    {
        private readonly IEnumerable<BetDataProvider> _betDataProviders;
        private readonly IBetDataCache _betDataCache;

        public BetDataProviderManager(IEnumerable<BetDataProvider> betDataProviders, IBetDataCache betDataCache)
        {
            _betDataProviders = betDataProviders;
            _betDataCache = betDataCache;
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
            var bets = betsProvidedEventArgs.Bets;
            _betDataCache.AddBets(bets);
        }
    }
}
