using System.Threading.Tasks;

namespace BetDataAcquisition
{
    public abstract class BetDataProvider
    {
        private Task _task;

        public event BetsProvidedEventHandler BetsProvided;
        public event BetsProviderFinishedEventHandler BetsProviderFinished;

        protected bool Running;

        public void Start()
        {
            if (!Running)
            {
                Running = true;
                _task = Task.Factory.StartNew(OnStart);
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

        protected virtual void OnStop()
        {
            _task.Wait();
            _task = null;
        }
    }
}