using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public class Kometa : Parser
	{
		readonly string[] DayIDs = new string[] {
			"po",
			"ut",
			"st",
			"ct",
			"pa",
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
			var dayData = document.QuerySelectorAll($"div#{DivId} tr");
			var soupData = document.QuerySelectorAll($"div#{DivId} p")[0];

			return await Task.FromResult(new MenuSection[] {
				new MenuSection {
					meals = ProcessMenuList(dayData)
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