using System;
using System.Collections.Generic;

namespace BetModel
{
    public class Customer
    {
        public int Id { get; }
        public List<Bet> Bets { get; }

        public Customer(int id)
        {
            Id = id;
            Bets = new List<Bet>();
        }

        protected bool Equals(Customer other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Customer) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}