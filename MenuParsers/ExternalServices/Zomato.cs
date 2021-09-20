using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using AngleSharp.Dom;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public class Zomato : Parser
	{
		private string _restaurantId;
		private HttpClient _client;

		public Zomato(string restaurantId, HttpClient client)
		{
			_restaurantId = restaurantId;
			_client = client;
		}

		public async Task<MenuSection[]> ParseDay(IDocument _document)
		{
			return ParseMenu(
				await DownloadMenu()
			);
		}

		private async Task<string> DownloadMenu()
		{
			var request = new HttpRequestMessage(HttpMethod.Get, $"?res_id={_restaurantId}");
			var response = await _client.SendAsync(request);
			return await response.Content.ReadAsStringAsync();
		}

		private MenuSection[] ParseMenu(string rawMenu)
		{
			var menu = JsonSerializer.Deserialize<ZomatoMenu>(rawMenu);

			return menu.daily_menus.Select(dailyMenu =>
			{
				return new MenuSection
				{
					name = dailyMenu.daily_menu.name,
					meals = dailyMenu.daily_menu.dishes.Select(dish => new Meal
					{
						name = dish.dish.name,
						price = dish.dish.price
					}).ToArray()
				};
			}).ToArray();
		}
	}
}