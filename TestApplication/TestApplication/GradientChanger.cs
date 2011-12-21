
using Android.Widget;

namespace TestApplication
{
    using System;
    using Android.Hardware;

    public class GradientChanger : Java.Lang.Object, ISensorEventListener
    {
        private readonly SensorManager _sensorManager;
        private readonly SensorType _sensorType;
        private readonly AbsoluteLayout _layout;


        public GradientChanger(SensorManager sensorManager, SensorType sensorType, AbsoluteLayout layout)
        {
            this._sensorManager = sensorManager;
            this._sensorType = sensorType;
            _layout = layout;
            this.StartListening();
        }

        public void StartListening()
        {
            var sensor = this._sensorManager.GetDefaultSensor(this._sensorType);
            this._sensorManager.RegisterListener(this, sensor, SensorDelay.Ui);
        }

        public void OnAccuracyChanged(Sensor sensor, int accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            var values = e.Values;
            int valueBg = (int)Math.Round(values[0], 0);

            //_layout.SetBackgroundColor(valueBg);
        }
    }
}