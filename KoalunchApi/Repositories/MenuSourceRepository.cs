using System.Threading.Tasks;
using System.Linq;

using koalunch_api.MenuParsers;
using koalunch_api.Models;
using koalunch_api.Models.Api;
using System.Net.Http;

namespace koalunch_api.Repositories
{
	public class MenuSourceRepository : IRepository<MenuSource>
	{
		IRepository<Restaurant> _repository;
		MenuSource[] _menuSources;
		IHttpClientFactory _httpClientFactory;

		public MenuSourceRepository(IRepository<Restaurant> repository, IHttpClientFactory httpClientFactory)
		{
			_repository = repository;
			_httpClientFactory = httpClientFactory;
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
				var httpClient = _httpClientFactory.CreateClient();

				_menuSources = new MenuSource[] {
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "kometaPubArena"),
						MenuUrl = "https://www.kometapub.cz/arena.php",
						Type = MenuType.Standard,
						Parser = new Kometa()
					},
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "rebioHolandska"),
						MenuUrl = "http://www.rebio.cz/Holandska/Nase-nabidka/dW-ei.folder.aspx",
						Type = MenuType.Standard,
						Parser = new Rebio()
					},
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "tustoTitanium"),
						MenuUrl = "http://titanium.tusto.cz/tydenni-menu/",
						Type = MenuType.Standard,
						Parser = new Tusto()
					},
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "uHovezihoPupku"),
						MenuUrl = "http://www.uhovezihopupku.cz/menu/",
						Type = MenuType.Standard,
						Parser = new UHovezihoPupku()
					},
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "uTesare"),
						MenuUrl = "http://www.utesare.cz/poledni-nabidka/",
						Type = MenuType.Standard,
						Parser = new UTesare()
					},
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "buffaloAmericanSteakhouse"),
						MenuUrl = "",
						Type = MenuType.Standard,
						Parser = new Zomato("18491544", _httpClientFactory.CreateClient("zomato"))
					},
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "eatologyHolandska"),
						MenuUrl = "http://iqrestaurant.cz/brno/getData.svc?type=brnoMenuHTML2",
						Type = MenuType.PDF,
						PDFInfoProvider = new Eatology(SearchRestaurant(restaurants, "eatologyHolandska"), httpClient)
					},
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "iqMoravka"),
						MenuUrl = "http://www.iqrestaurant.cz/moravka.html?iframe=true",
						Type = MenuType.PDF,
						PDFInfoProvider = new IQMoravka(SearchRestaurant(restaurants, "iqMoravka"), httpClient)
					},
					new MenuSource {
						Restaurant = SearchRestaurant(restaurants, "jeanPaulBistro"),
						MenuUrl = "https://www.jpbistro.cz/menu-holandska/index.php",
						Type = MenuType.Standard,
						Parser = new JeanPaulBistro()
					},
					new MenuSource
                    {
						Restaurant = SearchRestaurant(restaurants, "grandKitchenVlnena"),
						MenuUrl = "https://www.grandkitchenvlnena.cz/menu/",
						Type = MenuType.Standard,
						Parser = new GrandKitchenVlnena()
					},
					new MenuSource
                    {
						Restaurant = SearchRestaurant(restaurants, "sharingham"),
						MenuUrl = "https://www.restaurace-sharingham.cz/",
						Type = MenuType.Standard,
						Parser = new Sharingham()
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