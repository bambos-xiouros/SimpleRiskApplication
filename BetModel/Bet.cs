using System.ComponentModel;

namespace BetModel
{
    public class Bet
    {
        public Bet (int customerId, int eventId, int participantId, double stake, double win, bool settled)
        {
            CustomerId = customerId;
            EventId = eventId;
            ParticipantId = participantId;
            Stake = stake;
            Win = win;
            Settled = settled;
        }

        public int CustomerId { get; private set; }
        public int EventId { get; private set; }
        public int ParticipantId { get; private set; }
        public double Stake { get; private set; }

        public double Win { get; set; }
        public bool Settled { get; set; }
    }
}
