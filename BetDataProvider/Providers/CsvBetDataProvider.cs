using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using BetModel;
using CsvHelper;

namespace BetDataAcquisition.Providers
{
    internal abstract class CsvBetDataProvider : BetDataProvider
    {
        private readonly string _filePath;
        private readonly int _maxBetBatchSize;

        internal CsvBetDataProvider(string filePath, int maxBetBatchSize = 1000)
        {
            _filePath = filePath;
            _maxBetBatchSize = maxBetBatchSize;
        }

        protected override void OnStart()
        {
            var newBets = new List<Bet>();

            using (TextReader textReader = File.OpenText(_filePath))
            {
                var csv = new CsvReader(textReader);
                while (csv.Read())
                {
                    if (!Running)
                        break;

                    var bet = CreateBet(csv);
                    newBets.Add(bet);

                    if (newBets.Count == _maxBetBatchSize)
                    {
                        OnBetsProvided(new BetsProvidedEventArgs(newBets));
                        newBets.Clear();
                    }
                }

                if (newBets.Any())
                { 
                    OnBetsProvided(new BetsProvidedEventArgs(newBets));
                }

                OnBetsProvidedFinished(new BetsProviderFinishedEventArgs());
            }
        }

        protected abstract Bet CreateBet(CsvReader csvReader);
    }
}