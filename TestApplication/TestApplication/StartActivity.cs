
using Android.App;
using Android.OS;
using Android.Widget;

namespace TestApplication
{
    using System.Threading.Tasks;
    using Android.Hardware;
    using Android.Locations;

    [Activity(Label = "TestApplication", MainLauncher = true, Icon = "@drawable/icon")]
    public class StartActivity : Activity
    {
        int count = 1;
        private Task _task;
        private ProgressDialog _progressDialog;
        private LoginService _loginService;
        private TextView _statusLabel;
        private BackgroundRunner _backgroundRunner;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var labelGyro = FindViewById<TextView>(Resource.Id.helloLabel);
            this._statusLabel = FindViewById<TextView>(Resource.Id.sensor2);
            var layot = this.FindViewById<AbsoluteLayout>(Resource.Id.widget39);

            //var refreshButton = this.FindViewById<Button>(Resource.Id.RefreshButton);
            var clock = this.FindViewById<AnalogClock>(Resource.Id.Clock);

            //var sensorService = (SensorManager)this.GetSystemService(SensorService);
            //this.StartListening(sensorService, labelGyro, SensorType.Accelerometer);
            //this.StartListening(sensorService, label2, SensorType.Orientation);
            //this.StartListening(sensorService, FindViewById<TextView>(Resource.Id.sensor3), SensorType.Gyroscope);
            //this.StartListening(sensorService, FindViewById<TextView>(Resource.Id.sensor4), SensorType.Pressure);
            //this.StartListening(sensorService, FindViewById<TextView>(Resource.Id.sensor5), SensorType.Light);



            this._loginService = new LoginService();
            this._progressDialog = new ProgressDialog(this) { Indeterminate = true };
            this._progressDialog.SetTitle("Is in Progress");
            this._progressDialog.SetMessage("Is processing plelase wait ...");

            this.RequestUpdatesForLocationManager();
            clock.Click += (sender, e) =>
                                       {
                                           FindViewById<TextView>(Resource.Id.sensor3).Text = this._backgroundRunner.Result;
                                       };

            //this.DoLogin();

            //new GradientChanger(sensorService, SensorType.Gyroscope, layot);
        }

        private void RequestUpdatesForLocationManager()
        {
            //this._progressDialog.Show();
            Task.Factory.StartNew(() =>
            {
                var sensorService = (SensorManager)this.GetSystemService(SensorService);
                this.StartListening(sensorService, null, SensorType.Accelerometer);
                RunOnUiThread(this.OnProcessDone);
            });

            var localtionManager = (LocationManager)this.GetSystemService(LocationService);
            var listener = new GenericLocationListener();
            this._backgroundRunner = new BackgroundRunner(localtionManager, listener);
            this._backgroundRunner.Run();
        }

        private void DoLogin()
        {
            this._progressDialog.Show();
            Task.Factory.StartNew(() =>
                                      {
                                          this._loginService.Login("MyName");
                                          RunOnUiThread(this.OnProcessDone);
                                      });
        }

        private void OnProcessDone()
        {
            this._progressDialog.Hide();
            //new AlertDialog.Builder(this).SetTitle("Done").SetMessage("Well done yo're log'din").Show();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this._task.Dispose();
        }

        private void StartListening(SensorManager sensorService, TextView label, SensorType sensorType)
        {
            var sensorListener = new GenericSensorListener(sensorService, sensorType, label);
            sensorListener.StartListening();
        }
    }
}

