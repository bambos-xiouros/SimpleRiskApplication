using System.Collections.Generic;
using BetDataAcquisition.Cache;
using BetModel;

namespace BetDataAcquisition
{
    public interface IBetDataCache
    {
        event BetsAddedEventHandler BetsAddedEventHandler;
        event CustomerAddedEventHandler CustomerAddedEventHandler;

        void AddBets(IEnumerable<Bet> bets);
    }
}