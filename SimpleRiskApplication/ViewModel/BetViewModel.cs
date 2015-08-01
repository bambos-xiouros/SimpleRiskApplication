using BetModel;

namespace SimpleRiskApplication.ViewModel
{
    public class BetViewModel : ViewModelBase
    {
        private readonly Bet _bet;

        public BetViewModel(Bet bet)
        {
            _bet = bet;
        }

        public int Customer => _bet.CustomerId;
        public int Event => _bet.EventId;
        public int Participant => _bet.ParticipantId;
        public double Stake => _bet.Stake;
        public double Win => _bet.Win;
        public bool Settled => _bet.Settled;
    }
}
