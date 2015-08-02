using System.Collections.ObjectModel;

namespace SimpleRiskApplication.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        public int Id { get; }
        public ObservableCollection<BetDataViewModel> BetDataViewModels { get; private set; } 

        public CustomerViewModel(int id)
        {
            Id = id;
            BetDataViewModels = new ObservableCollection<BetDataViewModel>();
        }

        private bool _hasUnusualWinRate;
        public bool HasUnusualWinRate
        {
            get { return _hasUnusualWinRate; }
            set { _hasUnusualWinRate = value; OnPropertyChanged(); }
        }
    }
}