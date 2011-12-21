

namespace TestApplication
{
    using System;
    using Android.Locations;

    public class BackgroundRunner
    {
        private string _result;
        private readonly LocationManager _localtionManager;
        private readonly GenericLocationListener _listener;

        public BackgroundRunner(LocationManager localtionManager, GenericLocationListener listener)
        {
            _localtionManager = localtionManager;
            _listener = listener;
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public string Result
        {
            get { return this._result; }
            set { this._result = value; }
        }

        public void Run()
        {
            this._listener.GotLocation += this.OnGotLocation;
            this._localtionManager.RequestLocationUpdates(LocationManager.NetworkProvider, 0, 0, this._listener);
        }

        private void OnGotLocation(object sender, EventArgs e)
        {
            this._result = this._listener.LastReadLocation;
        }
    }
}