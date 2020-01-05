using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Analytics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinFireBase.Interfaces.GA;
using XamarinFireBase.iOS.Services.GA;

[assembly: Dependency(typeof(EventTrackerIOS))]
namespace XamarinFireBase.iOS.Services.GA
{
	public class EventTrackerIOS : IEventTracker
	{
		public void SendEvent(string eventId)
		{
			SendEvent(eventId, (IDictionary<string, string>)null);
		}

		public void SendEvent(string eventId, string paramName, string value)
		{
			SendEvent(eventId, new Dictionary<string, string>
			{
				{ paramName, value }
			});
		}

		public void SendEvent(string eventId, IDictionary<string, string> parameters)
		{
			if (parameters == null)
			{
				Analytics.LogEvent(eventId, (Dictionary<object, object>)null);
				return;
			}

			var keys = new List<NSString>();
			var values = new List<NSString>();

			//TODO: [Abi] Verify how Key and Value pairs are actially mapped (based on index?)
			foreach (var item in parameters)
			{
				keys.Add(new NSString(item.Key));
				values.Add(new NSString(item.Value));
			}

			var parametersDictionary =
				NSDictionary<NSString, NSObject>.FromObjectsAndKeys(values.ToArray(), keys.ToArray(), keys.Count);
			Analytics.LogEvent(eventId, parametersDictionary);
		}
	}
}