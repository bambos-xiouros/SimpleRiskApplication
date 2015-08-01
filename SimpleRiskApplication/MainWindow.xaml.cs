using System.ComponentModel;
using System.Windows;
using BetDataAcquisition;
using SimpleRiskApplication.ViewModel;

namespace SimpleRiskApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly DataProviderManager _dataProviderManager;

        public MainWindow()
        {
            InitializeComponent();

            // IoC Layer would handle all of this
            var betDataProviderFactory = new BetDataProviderFactory();
            var betViewModels = new InMemoryBetViewModels();
            var mainWindowViewModel = new MainWindowViewModel(betViewModels);
            _dataProviderManager = new DataProviderManager(betDataProviderFactory, betViewModels);

            DataContext = mainWindowViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _dataProviderManager.CreateDataProvidersFromConfig();
            _dataProviderManager.StartAllDataProviders();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _dataProviderManager.StopAllDataProviders();
        }
    }
}
