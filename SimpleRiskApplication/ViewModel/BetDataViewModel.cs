using BetModel;

namespace SimpleRiskApplication.ViewModel
{
    public class BetDataViewModel : ViewModelBase
    {
        private readonly Bet _bet;

        public BetDataViewModel(Bet bet, CustomerViewModel customerViewModel)
        {
            _bet = bet;
            CustomerViewModel = customerViewModel;
        }

        private CustomerViewModel _customerViewModel;
        public CustomerViewModel CustomerViewModel
        {
            get { return _customerViewModel; }
            private set
            {
                _customerViewModel = value;
                OnPropertyChanged();
            } 
        }

        public int Event => _bet.EventId;
        public int Participant => _bet.ParticipantId;
        public double Stake => _bet.Stake;
        public double Win => _bet.Win;
        public bool Settled => _bet.Settled;
    }
}
