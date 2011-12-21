

namespace TestApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Android.Hardware;
    using Android.Widget;

    public class GenericSensorListener : Java.Lang.Object, ISensorEventListener
    {
        private readonly SensorManager _sensorManager;
        private readonly SensorType _sensorType;
        private readonly TextView _label;
        private double _cumulatedValues;

        public GenericSensorListener(SensorManager sensorManager, SensorType sensorType, TextView label)
        {
            this._sensorManager = sensorManager;
            this._sensorType = sensorType;
            _label = label;
        }

        public void StartListening()
        {
            var sensor = this._sensorManager.GetDefaultSensor(this._sensorType);
            if (sensor == null)
            {
                this.SetText(string.Format("Sensor '{0}' is null", this._sensorType));
            }
            this._sensorManager.RegisterListener(this, sensor, SensorDelay.Ui);
            this._cumulatedValues = 0;
        }

        private void SetText(string format)
        {
            if (this._label != null)
            {
                this._label.Text = format;
            }
        }

        public void OnAccuracyChanged(Sensor sensor, int accuracy)
        {
            var type = sensor.Type;
        }

        public void OnSensorChanged(SensorEvent e)
        {
            var values = e.Values;

            double cumulatedValues = values.Aggregate<float, float>(0, (current, value) => Math.Abs(current) + Math.Abs(value));

            this._cumulatedValues = this._cumulatedValues + cumulatedValues;

            var roundedValues = this.RoundValues(values);

            //string valueString = roundedValues.Aggregate(string.Empty, (current, value) => string.Format("{0}; {1}", current, value));
            //_label.Text = string.Format("Cumulated: '{0}' -> Current: {1}", this._cumulatedValues, valueString);
            //_label.Text = string.Format("Cumulated: '{0}' For Sensor: '{1}'", Math.Round(this._cumulatedValues, 0), this._sensorType);
        }

        private IList<int> RoundValues(IList<float> values)
        {
            return values.Select(value => Math.Round(value, 0)).Select(dummy => (int)dummy).ToList();
        }
    }
}