using System.Collections.Generic;
using BetModel;

namespace SimpleRiskApplication.ViewModel
{
    public interface IBetViewModels
    {
        void AddRange(IEnumerable<Bet> bets);
    }
}