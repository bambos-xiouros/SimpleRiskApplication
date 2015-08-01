using System;
using System.Collections.Generic;
using BetModel;

namespace BetDataAcquisition.Providers
{
    internal class RandomBetDataProvider : BetDataProvider
    {
        private int _numberOfBetsProvided;
        private readonly int _numberOfBets;
        private readonly int _maxBetBatchSize;
        private readonly Random _random = new Random();

        internal RandomBetDataProvider(int numberOfBets, int maxBetBatchSize = 1)
        {
            _numberOfBets = numberOfBets;
            _maxBetBatchSize = maxBetBatchSize;
        }

        protected override void OnStart()
        {
            var newBets = new List<Bet>();
            while (_numberOfBetsProvided < _numberOfBets)
            {
                if (!Running)
                    break;

                var bet = CreateRandomBet();
                newBets.Add(bet);

                _numberOfBetsProvided++;

                if (newBets.Count == _maxBetBatchSize || _numberOfBetsProvided == _numberOfBets)
                {
                    OnBetsProvided(new BetsProvidedEventArgs(newBets));
                    newBets.Clear();
                }
            }

            OnBetsProvidedFinished(new BetsProviderFinishedEventArgs());
        }

        private Bet CreateRandomBet()
        {
            var bet = new Bet(_random.Next(), _random.Next(), _random.Next(), _random.Next(), _random.Next(), _random.Next(0, 1) == 1);
            return bet;
        }
    }
}