namespace BetDataAcquisition
{
    public abstract class BetDataProvider
    {
        public abstract void Start();
        public abstract void Stop();
        public event BetsProvidedEventHandler BetsProvided;
        
        protected virtual void OnBetsProvided(BetsProvidedEventArgs e)
        {
            if (BetsProvided != null)
                BetsProvided(this, e);
        }
    }
}