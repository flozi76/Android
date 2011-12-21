
using Android.App;
using Android.OS;
using Android.Views;

namespace MoveUpApp
{
    using Android.Content;

    [Activity(Label = "My Activity")]
    public class GiraffeActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Giraffe);

            StartService(new Intent(this, typeof(MusicService)));
            // Create your application here
        }
    }
}