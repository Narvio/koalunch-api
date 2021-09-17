using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using AngleSharp.Dom;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public class MyFood : Parser
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
			var dayData = document.QuerySelectorAll("div.jidla>div")[ThisDay];


			return await Task.FromResult(new MenuSection[] {
				new MenuSection {
					meals = ProcessMenuList(dayData.Children[0])
					.Concat(ProcessMenuList(dayData.Children[1]))
					.ToArray()
				}
			});
		}

		private Meal[] ProcessMenuList(IElement list)
		{
			var meals = new List<Meal> { };
			var data = list.Children[1];

			foreach (var item in data.Children)
			{
				meals.Add(new Meal
				{
					name = item.Children[0].TextContent,
					price = NormalizePrice(item.Children[1].TextContent)
				});
			}

			return meals.ToArray();
		}

		private string NormalizePrice(string price)
		{
			return price.Replace(" Kč", ",-");
		}
	}
}