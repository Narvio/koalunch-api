using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public class IQMoravka : PDFInfoProvider
	{
		private HttpClient _client;
		private Restaurant _restaurant;
		private int ThisDay
		{
			get
			{
				return (int)DateTime.Now.DayOfWeek;
			}
		}

		private readonly string BaseUrl = "http://www.iqrestaurant.cz/moravka";
		private readonly string[] DaysMapping = {
			"01 PONDĚLÍ",
			"02 ÚTERÝ",
			"03 STŘEDA",
			"04 ČTVRTEK",
			"05 PÁTEK"
		};
		private string PDFUrl
		{
			get
			{
				return $"{BaseUrl}/{DaysMapping[ThisDay - 1]}.pdf";
			}
		}

		public IQMoravka(Restaurant restaurant, HttpClient client)
		{
			_client = client;
			_restaurant = restaurant;
		}

		public async Task<PDFInfo> GetDayInfo()
		{
			var request = new HttpRequestMessage(HttpMethod.Get, PDFUrl);
			var response = await _client.SendAsync(request);
			var content = await response.Content.ReadAsByteArrayAsync();

			return new PDFInfo
			{
				url = PDFUrl,
				pages = new int[] { 1 },
				content = new PDFContentBuffer
				{
					data = content.Select(b => (int)b).ToArray()
				}
			};
		}
	}
}