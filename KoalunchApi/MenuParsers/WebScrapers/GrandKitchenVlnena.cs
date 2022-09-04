using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom;
using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
    public class GrandKitchenVlnena : Parser
    {
        private readonly int WeekDataIndex = 5;

        private int CurrentDayIndex
        {
            get => (int)DateTime.Now.DayOfWeek - 1;
        }

        public async Task<MenuSection[]> ParseDay(IDocument document)
        {
            var meals = new List<Meal>();
            var dayData = document.QuerySelectorAll(".jidel")[CurrentDayIndex];
            var weekData = document.QuerySelectorAll(".jidel")[WeekDataIndex];


            return await Task.FromResult(new MenuSection[] {
                new MenuSection
                {
                    name = "Denní menu",
                    meals = ProcessMenuSection(dayData)
                },
                new MenuSection
                {
                    name = "Týdenní menu",
                    meals = ProcessMenuSection(weekData)
                }
            });
        }

        private Meal[] ProcessMenuSection(IElement data)
        {
            var mealsData = data.QuerySelectorAll("li");
            var meals = new List<Meal> { };

            foreach(var item in mealsData)
            {
                meals.Add(new Meal
                {
                    name = Normalize(item.QuerySelector(".fly-dish-menu-description").FirstElementChild.TextContent),
                    price = Normalize(item.QuerySelector(".fly-dish-menu-price").TextContent)
                });
            }

            return meals.ToArray();
        }

        private string Normalize(string input)
        {
            return input.Trim();
        }
    }
}
