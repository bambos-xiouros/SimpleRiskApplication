using System;
using System.Collections.Generic;
using BetModel;

namespace BetDataAcquisition
{
    public class BetsProvidedEventArgs : EventArgs
    {
        public IEnumerator<Bet> Bets { get; private set; }

        internal BetsProvidedEventArgs(IEnumerator<Bet> bets)
        {
            Bets = bets;
        }
    }
}