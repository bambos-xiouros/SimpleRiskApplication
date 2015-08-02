using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

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
            BetDataViewModels.CollectionChanged += BetDataViewModelsOnCollectionChanged;
        }

        private void BetDataViewModelsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            Recalculate();
        }

        private float _winRatePercentage;
        public float WinRatePercentage
        {
            get { return _winRatePercentage; }
            set { _winRatePercentage = value; OnPropertyChanged(); }
        }

        private double _averageStake;
        public double AverageStake
        {
            get { return _averageStake; }
            set { _averageStake = value; OnPropertyChanged(); }
        }

        private bool _hasUnusualWinRate;
        public bool HasUnusualWinRate
        {
            get { return _hasUnusualWinRate; }
            set { _hasUnusualWinRate = value; OnPropertyChanged(); }
        }

        private void Recalculate()
        {
            if (BetDataViewModels.Any())
            {
                CalculateAverageStake();
                CalulateWinRatePercentage();
            }
        }

        private void CalulateWinRatePercentage()
        {
            var settledBets = BetDataViewModels.Where(bet => bet.Settled).ToList();
            if (settledBets.Any())
            {
                var numberOfBetsWon = settledBets.Count(betDataViewModel => betDataViewModel.Win > 0);
                var percentageOfBetsWon = numberOfBetsWon*100/settledBets.Count();

                WinRatePercentage = percentageOfBetsWon;
            }
        }

        private void CalculateAverageStake()
        {
            var totalStake = BetDataViewModels.Sum(betDataViewModel => betDataViewModel.Stake);
            var averageStake = totalStake/BetDataViewModels.Count;
            AverageStake = averageStake;
        }
    }
}