using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AngleSharp.Dom;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public class Rebio : Parser
	{
		public async Task<MenuSection[]> ParseDay(IDocument document)
		{
			var date = DateTime.Now;
			var id = $"{date.Day}.{date.Month}.{date.Year}";
			var dayHeader = document.QuerySelector($"h3[id='{id}']");

			if (dayHeader == null)
			{
				dayHeader = document.QuerySelector($"h3[id='{date.Day}. {date.Month}. {date.Year}']");
			}

			return await Task.FromResult(new MenuSection[] {
				new MenuSection {
					meals = GetMeals(dayHeader)
				}
			});
		}

		private Meal[] GetMeals(IElement dayHeader)
		{
			return dayHeader != null ? ProcessMenuList(dayHeader.NextElementSibling) : new Meal[] { };
		}

		private Meal[] ProcessMenuList(IElement list)
		{
			var meals = new List<Meal> { };

			foreach (var item in list.Children)
			{
				meals.Add(new Meal
				{
					name = item.Children[0].TextContent,
					price = ""
				});
			}

			return meals.ToArray();
		}
	}
}