using System;
using System.Collections.Generic;
using BetModel;

namespace BetDataAcquisition
{
    public class BetsProvidedEventArgs : EventArgs
    {
        public IEnumerable<Bet> Bets { get; private set; }

        internal BetsProvidedEventArgs(IEnumerable<Bet> bets)
        {
            Bets = bets;
        }
    }
}