using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BetModel;

namespace BetDataAcquisition.Cache
{
    internal class InMemoryConcurrentGetDataCache : IBetDataCache
    {
        private readonly object _lockObject = new object();
        private readonly Dictionary<int, List<Bet>> _customerIdToBetsMap = new Dictionary<int, List<Bet>>();

        public event BetsAddedEventHandler BetsAddedEventHandler;
        public event CustomerAddedEventHandler CustomerAddedEventHandler;

        public void AddBets(IEnumerable<Bet> bets)
        {
            lock (_lockObject)
            {
                var newItemsDictionary = AddToCache(bets);
                FireNewBetsEvents(newItemsDictionary);
            }
        }

        protected virtual void OnBetsAdded(BetsAddedEventArgs e)
        {
            if (BetsAddedEventHandler != null)
            {
                BetsAddedEventHandler(this, e);
            }
        }

        protected virtual void OnCustomerAdded(CustomerAddedEventArgs e)
        {
            if (CustomerAddedEventHandler != null)
            {
                CustomerAddedEventHandler(this, e);
            }
        }

        private Dictionary<int, List<Bet>> AddToCache(IEnumerable<Bet> bets)
        {
            var newItemsDictionary = new Dictionary<int, List<Bet>>();
            foreach (var bet in bets)
            {
                var customerId = bet.CustomerId;
                EnsureCustomerInMap(customerId);
                _customerIdToBetsMap[customerId].Add(bet);

                if (!newItemsDictionary.ContainsKey(customerId))
                {
                    newItemsDictionary[customerId] = new List<Bet>();
                }
                newItemsDictionary[customerId].Add(bet);
            }
            return newItemsDictionary;
        }

        private void FireNewBetsEvents(Dictionary<int, List<Bet>> newItemsDictionary)
        {
            foreach (var customerBets in newItemsDictionary)
            {
                var eventArgs = new BetsAddedEventArgs(customerBets.Key, customerBets.Value);
                OnBetsAdded(eventArgs);
            }
        }

        private void EnsureCustomerInMap(int customerId)
        {
            List<Bet> bets;
            if (!_customerIdToBetsMap.TryGetValue(customerId, out bets))
            {
                bets = new List<Bet>();
                _customerIdToBetsMap[customerId] = bets;
                
                var eventArgs = new CustomerAddedEventArgs(customerId);
                OnCustomerAdded(eventArgs);
            }
        }
    }
}