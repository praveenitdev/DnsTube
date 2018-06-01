﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CloudflareDynDNS
{
	public class Utility
	{
		public static void SaveSetting(string key, string value)
		{
			Properties.Settings.Default[key] = value;
			Properties.Settings.Default.Save();

		}

		public static string GetSetting(string key)
		{
			return Properties.Settings.Default[key]?.ToString();
		}

		public static string GetExternalAddress(HttpClient Client)
		{
			var req = new HttpRequestMessage(HttpMethod.Get, "http://checkip.dyndns.org");

			Client.DefaultRequestHeaders
				  .Accept
				  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = Client.SendAsync(req).Result;
			if (!response.IsSuccessStatusCode)
				return null;
			if (response == null)
				return null;

			var strResponse = response.Content.ReadAsStringAsync().Result;
			string[] strResponse2 = strResponse.Split(':');
			string strResponse3 = strResponse2[1].Substring(1);
			string newExternalAddress = strResponse3.Split('<')[0];

			if (newExternalAddress == null)
				return null; 

			return newExternalAddress;
		}
	}
}
