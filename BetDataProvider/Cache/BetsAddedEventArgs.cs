using System;
using System.Collections.Generic;
using BetModel;

namespace BetDataAcquisition.Cache
{
    public class BetsAddedEventArgs : EventArgs
    {
        public int CustomerId { get; private set; }
        public IEnumerable<Bet> Bets { get; set; }

        internal BetsAddedEventArgs(int customerId, IEnumerable<Bet> bets)
        {
            CustomerId = customerId;
            Bets = bets;
        }
    }
}