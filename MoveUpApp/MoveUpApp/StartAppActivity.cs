
using Android.App;
using Android.OS;

namespace MoveUpApp
{
    using System.Collections.Generic;
    using Android.Content;
    using Android.Views;
    using Android.Widget;

    [Activity(Label = "MoveUpApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class StartAppActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Main);

            this.BindButtons();

            //var nm = (NotificationManager)GetSystemService(NotificationService);
            //nm.Notify(123, new Notification(Resource.Drawable.Icon, "Bubaaa"));


            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} Click Hallo!", count++); };
        }

        private void BindButtons()
        {
            var button1 = this.FindViewById<Button>(Resource.Id.MooveBtn1);
            var button2 = this.FindViewById<Button>(Resource.Id.MooveBtn2);
            var button3 = this.FindViewById<Button>(Resource.Id.MooveBtn3);
            var button4 = this.FindViewById<Button>(Resource.Id.MooveBtn4);

            this.AddVibrationToButton(button1);
            this.AddVibrationToButton(button2);
            this.AddVibrationToButton(button3);
            this.AddVibrationToButton(button4);

            this.AddIntentToGiraffButton(button1);
        }

        private void AddIntentToGiraffButton(Button button)
        {
            button.Click += delegate
                                {
                                    var intent = new Intent(this, typeof(GiraffeActivity));
                                    StartActivity(intent);
                                };
        }

        private void AddVibrationToButton(Button button)
        {
            var vibrator = (Vibrator)GetSystemService(Context.VibratorService);
            button.Click += delegate
                                {
                                    vibrator.Vibrate(200);
                                };
        }
    }
}

