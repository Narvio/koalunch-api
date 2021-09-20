using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AngleSharp.Dom;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public class UHovezihoPupku : Parser
	{
		private int ThisDay
		{
			get
			{
				return (int)DateTime.Now.DayOfWeek - 1;
			}
		}

		public async Task<MenuSection[]> ParseDay(IDocument document)
		{

			var menus = document.QuerySelectorAll("table.menu_den");
			var dayMenu = menus[ThisDay];
			var weekMenu = menus[menus.Length - 1];

			return await Task.FromResult(new MenuSection[] {
				new MenuSection {
					meals = ProcessMenuList(dayMenu)
				},
				new MenuSection {
					name = "Stálé jídlo na menu",
					meals = ProcessMenuList(weekMenu)
				}
			});
		}

		private Meal[] ProcessMenuList(IElement section)
		{
			var meals = new List<Meal> { };

			var names = section.QuerySelectorAll(".menu_jidlo_text");
			var prices = section.QuerySelectorAll(".menu_jidlo_cena");

			for (var i = 0; i < names.Length; ++i)
			{
				meals.Add(new Meal
				{
					name = names[i].TextContent,
					price = names[i].TextContent
				});
			}

			return meals.ToArray();
		}
	}
}