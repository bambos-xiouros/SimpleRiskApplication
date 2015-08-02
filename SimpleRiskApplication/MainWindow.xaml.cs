using System;
using System.ComponentModel;
using System.Windows;
using BetDataAcquisition;
using BetDataAcquisition.Cache;
using SimpleRiskApplication.Config;
using SimpleRiskApplication.Data;
using SimpleRiskApplication.Rules;
using SimpleRiskApplication.ViewModel;

namespace SimpleRiskApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly BetDataProviderManager _betDataProviderManager;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();

            // IoC Layer would handle all of this
            var betDataProviderFactory = new BetDataProviderFactory();
            var betDataProviderConfigReader = new BetDataProviderConfigReader(betDataProviderFactory);
            var betDataProviders =  betDataProviderConfigReader.CreateBetDataProvidersFromConfig();

            var betDataCacheFactory = new BetDataCacheFactory();
            var betDataCache = betDataCacheFactory.CreateInMemoryBetDataCache();

            _betDataProviderManager = new BetDataProviderManager(betDataProviders, betDataCache);

            // todo - from config is better
            var applicationRulesApplier = new ApplicationRulesApplier
            {
                UnusualWinRateValue = 60
            };

            _mainWindowViewModel = new MainWindowViewModel(betDataCache, applicationRulesApplier);
            DataContext = _mainWindowViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _betDataProviderManager.StartAllDataProviders();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _betDataProviderManager.StopAllDataProviders();
        }
    }
}
