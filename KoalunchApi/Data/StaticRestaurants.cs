using System;

using koalunch_api.Models.Api;

namespace koalunch_api.Data
{
	public static class StaticRestaurants
	{
		static Restaurant[] _restaurants = new Restaurant[] {
			new Restaurant {
				id = "eatologyHolandska",
				name = "Eatology (IQ Holandská)",
				url = "http://iqrestaurant.cz/brno/menu.html",
				position = new RestaurantPosition {
					lat = "49.181198",
					lng = "16.605766"
				}
			},
			new Restaurant {
				id = "iqMoravka",
				name = "IQ Morávka",
				url = "http://www.iqrestaurant.cz/moravka.html?iframe=true",
				position = new RestaurantPosition {
					lat = "49.1773279",
					lng = "16.6025084"
				}
			},
			new Restaurant {
				id = "tustoTitanium",
				name = "Tusto Titanium",
				url = "http://titanium.tusto.cz/tydenni-menu/",
				position = new RestaurantPosition {
					lat = "49.187562",
					lng = "16.606352"
				}
			},
			new Restaurant {
				id = "kometaPubArena",
				name = "Kometa Pub Arena",
				url = "https://www.kometapub.cz/arena.php",
				position = new RestaurantPosition {
					lat = "49.185013",
					lng = "16.602007"
				}
			},
			new Restaurant {
				id = "rebioHolandska",
				name = "Rebio Holandská",
				url = "http://www.rebio.cz/Holandska/Nase-nabidka/dW-ei.folder.aspx",
				position = new RestaurantPosition {
					lat = "49.180287",
					lng = "16.604408"
				}
			},
			new Restaurant {
				id = "uHovezihoPupku",
				name = "U Hovězího pupku",
				url = "http://www.uhovezihopupku.cz/menu/",
				position = new RestaurantPosition {
					lat = "49.180123",
					lng = "16.595575"
				}
			},
			new Restaurant {
				id = "uTesare",
				name = "Hostinec u Tesaře",
				url = "http://www.utesare.cz/poledni-nabidka/",
				position = new RestaurantPosition {
					lat = "49.1813984",
					lng = "16.5971371"
				}
			},
			new Restaurant {
				id = "buffaloAmericanSteakhouse",
				name = "Buffalo American Steakhouse",
				url = "http://www.restauracebuffalo.cz/",
				position = new RestaurantPosition {
					lat = "49.18393",
					lng = "16.579791"
				}
			},
			new Restaurant {
				id = "jeanPaulBistro",
				name = "Jean Paul's Bistro",
				url = "https://www.jpbistro.cz/menu-holandska/index.php",
				position = new RestaurantPosition {
					lat = "49.181198",
					lng = "16.605766"
				}
			},
			new Restaurant
            {
				id = "grandKitchenVlnena",
				name = "Grand Kitchen Vlněna",
				url = "https://www.grandkitchenvlnena.cz/menu/",
				dateAdded = new DateTime(2022, 9, 4),
				position = new RestaurantPosition
                {
					lat = "49.1891971",
					lng = "16.6143149"
				}
			},
			new Restaurant
            {
				id = "sharingham",
				name = "Sharingham",
				url = "https://www.restaurace-sharingham.cz/",
				dateAdded = new DateTime(2022, 9, 5),
				position = new RestaurantPosition
                {
					lat = "49.186083",
					lng = "16.5932156"
				}
			},
			new Restaurant
            {
				id = "goaSlatina",
				name = "Goa Slatina",
				url = "http://www.restaurant-goa-slatina.cz/lang-cs/denni-menu",
				dateAdded = new DateTime(2022, 9, 19),
				position = new RestaurantPosition
                {
					lat = "49.1775199",
					lng = "16.6829385"
				}
			}
		};

		public static Restaurant[] GetAll()
		{
			return _restaurants;
		}

		public static Restaurant GetById(string id)
		{
			return Array.Find(_restaurants, r => r.id == id);
		}
	}
}