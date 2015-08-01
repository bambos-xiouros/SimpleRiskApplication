using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRiskApplication.ViewModel
{
    internal class BetViewModel
    {
        private readonly BetViewModel _bet;

        public BetViewModel(BetViewModel bet)
        {
            _bet = bet;
        }

        public int Customer => _bet.Customer;
        public int Event => _bet.Event;
        public int Participant => _bet.Participant;
        public int Stake => _bet.Stake;
        public int Win => _bet.Win;
    }
}
