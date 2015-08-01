namespace SimpleRiskApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IBetViewModels BetViewModels { get; set; }

        public MainWindowViewModel(IBetViewModels betViewModels)
        {
            BetViewModels = betViewModels;
        }
    }
}
