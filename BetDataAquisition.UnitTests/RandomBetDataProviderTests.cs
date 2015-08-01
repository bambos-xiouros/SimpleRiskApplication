using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BetDataAcquisition;
using BetModel;
using NUnit.Framework;

namespace BetDataAquisition.UnitTests
{
    [TestFixture]
    public class RandomBetDataProviderTests
    {
        private const int TimeOut = 1000;
        private readonly object _lockObject = new object();

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10000)]
        public void CreateRandomBets_GivenXBets_ReturnsXBets(int numberOfBets)
        {
            // Given
            var betsRecieved = new List<Bet>();
            var randomBetDataProvider = new RandomBetDataProvider(numberOfBets);
            randomBetDataProvider.BetsProvided += (sender, args) => { betsRecieved.AddRange(args.Bets); };
            randomBetDataProvider.BetsProviderFinished += (sender, args) =>
            {
                lock (_lockObject)
                {
                    Monitor.Pulse(_lockObject);
                }
            };

            // When
            randomBetDataProvider.Start();
            lock (_lockObject)
            {
                Monitor.Wait(_lockObject, TimeOut);
            }

            // Then
            Assert.That(betsRecieved.Count, Is.EqualTo(numberOfBets));
        }
        
        [TestCase(1, 5, Description="Batch Size Larger than DataSet size")]
        [TestCase(10, 9, Description = "Batch Size Smaller than DataSet size")]
        [TestCase(10, 10, Description = "Batch Size Equal than DataSet size")]
        public void CreateRandomBets_GivenXBatchSize_ReturnsXInBatchSize(int numberOfBets, int maxBetsBatchSize)
        {
            // Given
            var betsRecieved = new List<List<Bet>>();
            var randomBetDataProvider = new RandomBetDataProvider(numberOfBets, maxBetsBatchSize);
            randomBetDataProvider.BetsProvided += (sender, args) => { betsRecieved.Add(new List<Bet>(args.Bets)); };
            randomBetDataProvider.BetsProviderFinished += (sender, args) =>
            {
                lock (_lockObject)
                {
                    Monitor.Pulse(_lockObject);
                }
            };

            // When
            randomBetDataProvider.Start();
            lock (_lockObject)
            {
                Monitor.Wait(_lockObject, TimeOut);
            }

            // Then
            var lastBatch = betsRecieved.Last();
            foreach (var betsRecievedBatch in betsRecieved)
            {
                if(betsRecievedBatch != lastBatch)
                    Assert.That(betsRecievedBatch.Count, Is.EqualTo(maxBetsBatchSize));
                else
                    Assert.That(betsRecievedBatch.Count, Is.LessThanOrEqualTo(maxBetsBatchSize));
            }
        }
    }
}
