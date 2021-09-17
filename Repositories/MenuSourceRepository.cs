using System.Threading.Tasks;
using System.Linq;

using koalunch_api.MenuParsers;
using koalunch_api.Models;
using koalunch_api.Models.Api;

namespace koalunch_api.Repositories
{
	public class MenuSourceRepository
	{
		RestaurantRepository _repository;
		MenuSource[] _menuSources;

		public MenuSourceRepository(RestaurantRepository repository)
		{
			_repository = repository;
		}

		public async Task<MenuSource[]> GetAll()
		{
			return await InitializeMenuSources();
		}

		public async Task<MenuSource> GetById(string id)
		{
			var menuSources = await InitializeMenuSources();

			return menuSources.First(source => source.Restaurant.id == id);
		}

		private async Task<MenuSource[]> InitializeMenuSources()
		{
			if (_menuSources == null)
			{
				var restaurants = await _repository.GetAll();

				_menuSources = new MenuSource[] {
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "kometaPubArena"),
						MenuUrl = "https://www.kometapub.cz/arena.php",
						Type = MenuType.Standard,
						Parser = new Kometa()
					},
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "myFoodHolandska"),
						MenuUrl = "http://www.sklizeno.cz/o-nas/brno-holandska/",
						Type = MenuType.Standard,
						Parser = new MyFood()
					},
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "rebioHolandska"),
						MenuUrl = "http://www.rebio.cz/Holandska/Nase-nabidka/dW-ei.folder.aspx",
						Type = MenuType.Standard,
						Parser = new Rebio()
					}
				};
			}

			return _menuSources;
		}

		private Restaurant SearchRestaurant(Restaurant[] restaurants, string id)
		{
			return restaurants.First(r => r.id == id);
		}
	}
}