using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public class Kometa : Parser
	{
		readonly string[] DayIDs = new string[] {
			"Mon",
			"Tue",
			"Wed",
			"Thu",
			"Fri",
		};

		private string DivId
		{
			get
			{
				var currentDay = (int)DateTime.Now.DayOfWeek - 1;
				return $"menu-day-{this.DayIDs[currentDay]}";
			}
		}

		public async Task<MenuSection[]> ParseDay(IDocument document)
		{
			var meals = new List<Meal>();
			var dayData = document.QuerySelectorAll($"div#{DivId} tr");
			var soupData = document.QuerySelectorAll($"div#{DivId} p")[1];

			if (soupData != null && soupData.TextContent.StartsWith("Polévka:"))
			{
				meals.Add(new Meal
				{
					name = soupData.TextContent,
					price = ""
				});
			}

			return await Task.FromResult(new MenuSection[] {
				new MenuSection {
					meals = meals.Concat(ProcessMenuList(dayData))
								.ToArray()
				}
			});
		}

		private Meal[] ProcessMenuList(IHtmlCollection<IElement> list)
		{
			var meals = new List<Meal> { };

			foreach (var item in list)
			{
				meals.Add(new Meal
				{
					name = NormalizeName(item.Children[1].TextContent),
					price = item.Children[2].TextContent
				});
			}

			return meals.ToArray();
		}

		private string NormalizeName(string name)
		{
			return name.Trim();
		}
	}
}