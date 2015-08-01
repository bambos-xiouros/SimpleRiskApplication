namespace BetDataAcquisition
{
    internal class RandomBetDataProvider : BetDataProvider
    {
        private float _betsCreatedPerSecond;
        private bool _running;

        internal RandomBetDataProvider(float betsCreatedPerSecond)
        {
            _betsCreatedPerSecond = betsCreatedPerSecond;
        }

        public override void Start()
        {
            if (!_running)
            {
                _running = true;
            }
        }

        public override void Stop()
        {
            if (_running)
            {
                
            }
        }
    }
}