using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using IntroductionCore;

namespace IntroductionAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView outputUserName = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            EditText editTextUserName = FindViewById<EditText>(Resource.Id.EditUserName);
            outputUserName = FindViewById<TextView>(Resource.Id.TextOutputUserName);
            editTextUserName.TextChanged += EditTextUserName_TextChanged;
        }

        private void EditTextUserName_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            string userName = ((EditText)sender).Text;
            outputUserName.Text = userName == string.Empty ? "Welcome" : MessageSender.SendHelloToUser(userName);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}