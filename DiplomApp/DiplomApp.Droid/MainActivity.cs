using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CommonClasses.Forms.Helpers;

namespace DiplomApp.Droid
{
    [Activity(Label = "RascetDO", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            DialogsHelper.Init(new Acr.UserDialogs.UserDialogsImpl(()=>this));

            LoadApplication(new App());
        }
    }
}

