

namespace TestApplication
{
    using System;
    using Android.Locations;
    using Android.OS;

    public class GenericLocationListener : Java.Lang.Object, ILocationListener
    {
        private string _lastReadLocation;

        public event EventHandler GotLocation = delegate { };

        public string LastReadLocation
        {
            get { return this._lastReadLocation; }
            set { this._lastReadLocation = value; }
        }

        public void OnLocationChanged(Location location)
        {
            this._lastReadLocation = string.Format("Alt: {0}, Lat: {1}, Lon: {2}, Bear: {3}, Prov: {4}", location.Altitude, location.Latitude, location.Longitude, location.Bearing, location.Provider);
            GotLocation(this, EventArgs.Empty);
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
        }
    }
}