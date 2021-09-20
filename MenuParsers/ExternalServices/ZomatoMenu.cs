namespace koalunch_api.MenuParsers
{
	class ZomatoMenu
	{
		public ZomatoMenuDailyMenus[] daily_menus { get; set; }
		public string status { get; set; }
	}
	class ZomatoMenuDishes
	{
		public ZomatoMenuDish dish { get; set; }
	}

	class ZomatoMenuDish
	{
		public string dish_id { get; set; }
		public string name { get; set; }
		public string price { get; set; }
	}

	class ZomatoMenuDailyMenus
	{
		public ZomatoMenuDailyMenu daily_menu { get; set; }
	}

	class ZomatoMenuDailyMenu
	{
		public string daily_menu_id { get; set; }
		public string start_date { get; set; }
		public string end_date { get; set; }
		public string name { get; set; }
		public ZomatoMenuDishes[] dishes { get; set; }
	}
}