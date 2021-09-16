using System.Threading.Tasks;
using System.Linq;

using luncher_api.MenuParsers;
using luncher_api.Models;

namespace luncher_api.Repositories
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
				_menuSources = new MenuSource[] {
					new MenuSource {
						Restaurant = await _repository.GetById("kometaPubArena"),
						MenuUrl = "https://www.kometapub.cz/arena.php",
						Type = Models.Api.MenuType.Standard,
						Parser = new Kometa()
					}
				};
			}

			return _menuSources;
		}
	}
}