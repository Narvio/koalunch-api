using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AngleSharp.Dom;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	enum Position
	{
		Other,
		Soup,
		MainCourse
	}

	class DeterminationResult
	{
		public Position NewPosition;
		public int Offset;

		public DeterminationResult(Position position, int offset)
		{
			NewPosition = position;
			Offset = offset;
		}
	}

	public class UTesare : Parser
	{
		public async Task<MenuSection[]> ParseDay(IDocument document)
		{
			var items = document.QuerySelectorAll(".elementor-text-editor")[1].Children;
			var meals = new List<Meal> { };
			var position = Position.Other;

			for (var i = 0; i < items.Length; ++i)
			{
				var item = items[i];

				if (item.TagName.ToUpper() != "P")
				{
					var result = DeterminePosition(item);
					position = result.NewPosition;
					i += result.Offset;
					continue;
				}

				switch (position)
				{
					case Position.Soup:
					case Position.MainCourse:
						{
							meals.Add(ParseMeal(item));
							break;
						}
					default: continue;
				}
			}

			return await Task.FromResult(new MenuSection[] {
				new MenuSection {
					meals = meals.ToArray()
				}
			});
		}

		private DeterminationResult DeterminePosition(IElement item)
		{
			switch (item.TextContent.Trim())
			{
				case "Polévky": return new DeterminationResult(Position.Soup, 1);
				case "Hlavní chody": return new DeterminationResult(Position.MainCourse, 0);
				default: return new DeterminationResult(Position.Other, 0);
			}
		}

		private Meal ParseMeal(IElement item)
		{
			var text = item.TextContent.Trim();
			var priceStart = System.Text.RegularExpressions.Regex.Match(text, "\\d+,-");

			return new Meal
			{
				name = text.Substring(0, priceStart.Index),
				price = text.Substring(priceStart.Index)
			};
		}
	}
}