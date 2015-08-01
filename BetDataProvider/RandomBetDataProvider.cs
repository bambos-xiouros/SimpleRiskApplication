using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetModel;

namespace BetDataAcquisition
{
    internal class RandomBetDataProvider : BetDataProvider
    {
        private int _numberOfBetsProvided;
        private Task _task;

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
            _task = Task.Factory.StartNew(() =>
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
            });
        }

        protected override void OnStop()
        {
            _task.Wait();
            _task = null;
        }

        private Bet CreateRandomBet()
        {
            var bet = new Bet(_random.Next(), _random.Next(), _random.Next(), _random.Next(), _random.Next(), _random.Next(0, 1) == 1);
            return bet;
        }
    }
}