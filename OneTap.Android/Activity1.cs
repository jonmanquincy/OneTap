using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;

namespace OneTap.Android
{
    [Activity(
        Label = "Pocket Destroyer",
        Icon = "@mipmap/ic_launcher",
        RoundIcon = "@mipmap/ic_launcher_round",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
    )]
    public class Activity1 : AndroidGameActivity
    {
        private TapGame _game;
        private View _view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _game = new TapGame();
            _view = _game.Services.GetService(typeof(View)) as View;

            SetContentView(_view);
            _game.Run();
        }
    }
}
