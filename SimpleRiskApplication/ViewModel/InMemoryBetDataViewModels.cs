using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using BetModel;

namespace SimpleRiskApplication.ViewModel
{
    internal class InMemoryBetViewModels : ObservableCollection<BetViewModel>, IBetViewModels
    {
        public void AddRange(IEnumerable<Bet> bets)
        {
            // rubbish, should be using a background worker thread properly
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                foreach (var bet in bets)
                {
                    Add(new BetViewModel(bet));
                }
            }));
        }
    }
}