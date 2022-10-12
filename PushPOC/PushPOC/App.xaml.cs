using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PushPOC
{
    public partial class App : Application
    {
        public static string Data { get; set; }

        public bool IsNotification { get; }
        public IDictionary<string, object> NotificationData { get; }

        public App(bool hasNotification = false, IDictionary<string, object> notificationData = null)
        {
            InitializeComponent();

            if (!hasNotification)
                MainPage = new MainPage();
            else
            {
                foreach (var data in notificationData)
                {
                    if (data.Key == "LoginPage")
                    {
                        MainPage = new MainPage();
                        return;
                    }
                }
            }
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

            };
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }


            };
        }

        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
