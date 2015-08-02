using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using BetDataAcquisition;
using BetDataAcquisition.Cache;

namespace SimpleRiskApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly Dictionary<int, CustomerViewModel> _customerIdToCustomerLookup = new Dictionary<int, CustomerViewModel>();
        
        public ObservableCollection<CustomerViewModel> CustomerViewModels { get; set; }

        public MainWindowViewModel(IBetDataCache betDataCache)
        {
            CustomerViewModels = new ObservableCollection<CustomerViewModel>();

            betDataCache.CustomerAddedEventHandler += BetDataCacheOnCustomerAddedEventHandler;
            betDataCache.BetsAddedEventHandler += BetDataCacheOnBetsAddedEventHandler;
        }

        private void BetDataCacheOnCustomerAddedEventHandler(object sender, CustomerAddedEventArgs customerAddedEventArgs)
        {
            Debug.Assert(!_customerIdToCustomerLookup.ContainsKey(customerAddedEventArgs.CustomerId));

            var customerViewModel = new CustomerViewModel(customerAddedEventArgs.CustomerId);
            _customerIdToCustomerLookup[customerAddedEventArgs.CustomerId] = customerViewModel;

            // todo - don't do this
            Application.Current.Dispatcher.Invoke(() =>
            {
                CustomerViewModels.Add(customerViewModel);
            });
        }

        private void BetDataCacheOnBetsAddedEventHandler(object sender, BetsAddedEventArgs betsAddedEventArgs)
        {
            var customerId = betsAddedEventArgs.CustomerId;
            Debug.Assert(_customerIdToCustomerLookup.ContainsKey(customerId));

            var customerViewModel = _customerIdToCustomerLookup[customerId];

            var betViewModels = betsAddedEventArgs.Bets.Select(bet => new BetDataViewModel(bet, customerViewModel)).ToList();

            // todo - don't do this
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var betViewModel in betViewModels)
                {
                    customerViewModel.BetDataViewModels.Add(betViewModel);
                }
            });
        }

        private CustomerViewModel _selectedCustomerViewModel;
        public CustomerViewModel SelectedCustomerViewModel
        {
            get { return _selectedCustomerViewModel; }
            set { _selectedCustomerViewModel = value; OnPropertyChanged(); }
        }
    }
}
