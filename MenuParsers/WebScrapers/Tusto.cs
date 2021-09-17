using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AngleSharp.Dom;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public class Tusto : Parser
	{
		public async Task<MenuSection[]> ParseDay(IDocument document)
		{
			var sections = document.QuerySelectorAll(".weekly-list");

			return await Task.FromResult(new MenuSection[] {
				new MenuSection {
					name = "Denní menu",
					meals = ProcessSoupList(document)
					.Concat(ProcessMenuList(sections[0]))
					.ToArray()
				},
				new MenuSection {
					name = "Týdenní menu",
					meals = ProcessMenuList(sections[1])
				}
			});
		}

		private Meal[] ProcessSoupList(IDocument document)
		{
			var meals = new List<Meal>();
			var soups = document.QuerySelectorAll(".soap .soap-list li");

			soups.ToList().ForEach(soup => meals.Add(new Meal
			{
				name = soup.Children[0].ChildNodes[0].TextContent,
				price = soup.Children[1].TextContent
			}));

			return meals.ToArray();
		}

		private Meal[] ProcessMenuList(IElement section)
		{
			var meals = new List<Meal> { };
			var items = section.QuerySelectorAll("li");

			items.ToList().ForEach(item => meals.Add(new Meal
			{
				name = item.Children[1].ChildNodes[0].TextContent,
				price = item.Children[2].TextContent
			}));

			return meals.ToArray();
		}
	}
}