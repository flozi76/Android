
using Android.App;

namespace MoveUpApp
{
    using System;
    using Android.Content;
    using Android.Media;
    using Android.OS;

    [Service]
    public class MusicService : Service
    {
        private MediaPlayer _player;
        private NotificationManager _notificationManager;

        private enum Notifications { Started };

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            _player = MediaPlayer.Create(this, Resource.Raw.M77_Bombay_Street_Up_In_The_Sky);
            _player.Looping = false;

            _notificationManager = (NotificationManager)GetSystemService(NotificationService);

            var notification = new Notification(Resource.Drawable.Icon, "Service started", DateTime.Now.Ticks);
            notification.Flags = NotificationFlags.NoClear;

            PendingIntent notificationIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(GiraffeActivity)), 0);
            notification.SetLatestEventInfo(this, "Music Service", "The music service has been started", notificationIntent);

            _notificationManager.Notify((int)Notifications.Started, notification);
        }

        public override void OnStart(Intent intent, int startId)
        {
            base.OnStart(intent, startId);

            //_player.Start();
            //this.PutNotificationOnBar();
        }

        private void PutNotificationOnBar()
        {
            var notification = new Notification(Resource.Drawable.Icon,
                    "Buuubaaaaaxxx",
                    System.Environment.TickCount);
            PendingIntent contentIntent = PendingIntent.GetActivity(this, 0,
                    new Intent(this, typeof(StartAppActivity)),
                    0);
            notification.SetLatestEventInfo(this,
                    GetText(Resource.String.local_service_started),
                    "Baaabaaaayyyy",
                    contentIntent);
            var nm = (NotificationManager)GetSystemService(NotificationService);
            nm.Notify(Resource.String.local_service_started, notification);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _player.Stop();
            _notificationManager.CancelAll();
        }
    }
}