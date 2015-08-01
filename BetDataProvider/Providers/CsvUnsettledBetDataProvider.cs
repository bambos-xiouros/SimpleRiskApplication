using BetModel;
using CsvHelper;

namespace BetDataAcquisition.Providers
{
    internal class CsvUnsettledBetDataProvider : CsvBetDataProvider
    {
        public CsvUnsettledBetDataProvider(string filePath, int maxBetBatchSize = 1000) : base(filePath, maxBetBatchSize) { }

        protected override Bet CreateBet(CsvReader csv)
        {
            var customerId = csv.GetField<int>("Customer");
            var eventId = csv.GetField<int>("Event");
            var participantId = csv.GetField<int>("Participant");
            var stake = csv.GetField<double>("Stake");
            var win = csv.GetField<double>("To Win");
            var bet = new Bet(customerId, eventId, participantId, stake, win, false);
            return bet;
        }
    }
}