using System.Linq;
using SimpleRiskApplication.ViewModel;

namespace SimpleRiskApplication.Rules
{
    public class ApplicationRulesApplier : IApplicationRulesApplier
    {
        public void Apply(CustomerViewModel customerViewModel)
        {
            ApplyUnusualWinRate(customerViewModel);
        }

        public int UnusualWinRateValue { get; set; }

        private void ApplyUnusualWinRate(CustomerViewModel customerViewModel)
        {
            var numberOfBetsWon = customerViewModel.BetDataViewModels.Count(betDataViewModel => betDataViewModel.Settled && betDataViewModel.Win > 0);
            var percentageOfBetsWon = numberOfBetsWon*100/customerViewModel.BetDataViewModels.Count;
            customerViewModel.HasUnusualWinRate = percentageOfBetsWon > UnusualWinRateValue;
        }
    }
}