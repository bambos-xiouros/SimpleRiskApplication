namespace BetDataAcquisition
{
    public abstract class BetDataProvider
    {
        public event BetsProvidedEventHandler BetsProvided;
        public event BetsProviderFinishedEventHandler BetsProviderFinished;

        protected bool Running;

        public void Start()
        {
            if (!Running)
            {
                Running = true;
                OnStart();
            }
        }

        public void Stop()
        {
            if (Running)
            {
                Running = false;
                OnStop();
            }
        }

        protected virtual void OnBetsProvided(BetsProvidedEventArgs e)
        {
            if (BetsProvided != null)
            {
                BetsProvided(this, e);
            }
        }

        protected virtual void OnBetsProvidedFinished(BetsProviderFinishedEventArgs e)
        {
            if (BetsProviderFinished != null)
            {
                BetsProviderFinished(this, e);
            }
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
    }
}