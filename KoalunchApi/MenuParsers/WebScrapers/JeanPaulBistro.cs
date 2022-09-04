using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public class JeanPaulBistro : Parser
	{
		private int CurrentDayIndex
		{
			// get => (int)DateTime.Now.DayOfWeek - 1;
			get => 0;
		}
		public async Task<MenuSection[]> ParseDay(IDocument document)
		{
			var meals = new List<Meal>();
			var dayData = document.QuerySelectorAll(".title-date")[CurrentDayIndex].NextElementSibling.QuerySelectorAll("tr");
			var weekMenuParts = document.QuerySelector(".tydenni-menu").QuerySelectorAll("table");


			return await Task.FromResult(new MenuSection[] {
				new MenuSection {
					name = "Denní menu",
					meals = meals.Concat(ProcessMenuList(dayData))
								.ToArray()
				}
			}.Concat(
				weekMenuParts.Select(ProcessWeekMenuList)
			).ToArray());
		}

		private MenuSection ProcessWeekMenuList(IElement table)
		{
			return new MenuSection
			{
				name = Normalize(table.PreviousElementSibling.TextContent),
				meals = ProcessMenuList(table.QuerySelectorAll("tr"))
			};
		}

		private Meal[] ProcessMenuList(IHtmlCollection<IElement> list)
		{
			var meals = new List<Meal> { };

			foreach (var item in list)
			{
				meals.Add(new Meal
				{
					name = Normalize(item.QuerySelector(".text").TextContent),
					price = Normalize(item.QuerySelector(".price").TextContent)
				});
			}

			return meals.ToArray();
		}

		private string Normalize(string name)
		{
			return name.Trim();
		}
	}
}