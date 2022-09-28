using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public class Eatology : PDFInfoProvider
	{
		private HttpClient _client;
		private Restaurant _restaurant;
		private int ThisDay
		{
			get
			{
				return (int)DateTime.Now.DayOfWeek + OffsetThisDay();
			}
		}

		public Eatology(Restaurant restaurant, HttpClient client)
		{
			_client = client;
			_restaurant = restaurant;
		}

		public async Task<PDFInfo> GetDayInfo()
		{
			var request = new HttpRequestMessage(HttpMethod.Get, this._restaurant.url);
			var response = await _client.SendAsync(request);
			var content = await response.Content.ReadAsByteArrayAsync();

			if (!IsTodaySupported())
            {
				throw new Exception("No menu today");
            }

			return new PDFInfo
			{
				url = _restaurant.url,
				pages = new int[] { ThisDay * 2 - 1 },
				content = new PDFContentBuffer
				{
					data = content.Select(b => (int)b).ToArray()
				}
			};
		}

		private string GetTodayString()
        {
			var date = DateTime.Now;
			return $"{date.Day}.{date.Month}.{date.Year}";
		}

		private int OffsetThisDay()
        {
			switch (GetTodayString())
            {
				case "29.9.2022":
				case "30.9.2022":
                    {
						return -1;
                    }
				default:
                    {
						return 0;
                    }
            }
        }

		private bool IsTodaySupported()
        {
			switch (GetTodayString())
            {
				case "28.9.2022":
					{
						return false;
					} ;
				default:
					{
						return true;
					};
            }
        }
	}
}