using System;

namespace BetDataAcquisition.Cache
{
    public class CustomerAddedEventArgs : EventArgs
    {
        public int CustomerId { get; private set; }

        internal CustomerAddedEventArgs(int customerId)
        {
            CustomerId = customerId;
        }
    }
}