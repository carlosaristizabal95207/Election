using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Election.UICrossAndroid.Services
{
    using Common.Interfaces;
    using global::Android.Content;
    using global::Android.Net.Wifi;
    using MvvmCross;
    using MvvmCross.Platforms.Android;

    public class NetworkProvider : INetworkProvider
    {
        private readonly Context context;

        public NetworkProvider()
        {
            context = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
        }

        public bool IsConnectedToWifi()
        {
            var wifi = (WifiManager)context.GetSystemService(Context.WifiService);
            return wifi.IsWifiEnabled;
        }
    }
}