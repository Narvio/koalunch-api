using System.Threading.Tasks;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public class Kometa : Parser
	{
		public async Task<MenuSection[]> ParseDay(object dom)
		{
			return await Task.FromResult(new MenuSection[] {
				new MenuSection {
					meals = new Meal[] {
						new Meal { name = "Mašle s makem", price = "4,50" }
					}
				}
			});
		}
	}
}