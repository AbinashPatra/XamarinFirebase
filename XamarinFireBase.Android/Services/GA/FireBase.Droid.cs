using System.Collections.Generic;
using Android.OS;
using Firebase;
using Firebase.Analytics;
using Xamarin.Forms;
using XamarinFireBase.Droid.Services.GA;
using XamarinFireBase.Interfaces.GA;

[assembly: Dependency(typeof(EventTrackerDroid))]
namespace XamarinFireBase.Droid.Services.GA
{
    public class EventTrackerDroid : IEventTracker
    {
        public void SendEvent(string eventId)
        {
            SendEvent(eventId, null);
        }

        public void SendEvent(string eventId, string paramName, string value)
        {
            SendEvent(eventId, new Dictionary<string, string>
            {
                {paramName, value}
            });
        }

        public void SendEvent(string eventId, IDictionary<string, string> parameters)
        {
            //FirebaseOptions fileopts = new FirebaseOptions();
            //FirebaseApp.InitializeApp(Android.App.Application.Context, )

            var firebaseAnalytics = FirebaseAnalytics.GetInstance(Android.App.Application.Context);

            if (parameters == null)
            {
                firebaseAnalytics.LogEvent(eventId, null);
                return;
            }

            var bundle = new Bundle();
            foreach (var param in parameters)
            {
                bundle.PutString(param.Key, param.Value);
            }

            firebaseAnalytics.LogEvent(eventId, bundle);
        }
    }
}