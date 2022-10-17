﻿using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Globalization;

namespace PushConsole
{
    public class PushConsole
    {
        private static TimeSpan Start { get; set; }
        private static TimeSpan Eind { get; set; }
        private static TimeSpan Interval { get; set; }

        public static void Main(string[] args)
        {
            Start = DateTime.Now.TimeOfDay;
            Timer t = new Timer(DisplayTimeEvent, null, 0, 100);
            FirebaseApp.Create(new AppOptions()
            {
                //location to the google-service.json
                // Project Settings -> Service Account -> Generate new key
                Credential = GoogleCredential.FromFile(@"C:\Users\MikevanNieuwburg\Downloads\service-account-file.json"),
            });

            //Token for the device that will receive the notifications
            var registrationToken = "eSbIVp1TH87jlSwF_tQEVx:APA91bF6uiON48UXG-93TcjIzqbVO2YOsrlnrDnP9uCUe4Cv-PtKSOVIfSiSGa_Uv0W9PNBXeuo-BJv24qwTlgV0C5Rjogx33M89fHxwaOm6oc3HQN2-_FbVWOQ2cofu7vvrc8MIL3yD";

            for (int i = 0; i < 7000; i++)
            {
                var message = new Message()
                {
                    Token = registrationToken,
                    Notification = new Notification() { Title = $"This is notification [{i}]", Body = "Imagine there is a body here which can be interesting to some." }
                };
                string response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
                Console.WriteLine($"Successfully sent message [{i}]: " + response);
            }
            Eind = DateTime.Now.TimeOfDay;
            Console.WriteLine(Eind - Start);
        }

        private static void DisplayTimeEvent(object? state)
        {
            Interval = DateTime.Now.TimeOfDay;
            Console.WriteLine(Interval - Start);
        }
    }
}