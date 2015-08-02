using SimpleRiskApplication.ViewModel;

namespace SimpleRiskApplication.Rules
{
    public interface IApplicationRulesApplier
    {
        void Apply(CustomerViewModel customerViewModel);
    }
}