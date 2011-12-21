
using Android.App;
using Android.Content;
using Android.OS;

namespace TestApplication
{
    public class BackgroundService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }
    }
}