using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
    public class Sharingham : Parser
    {
        private readonly Dictionary<DayOfWeek, string> DayMapping = new Dictionary<DayOfWeek, string>
        {
            { DayOfWeek.Monday, "pondělí" },
            { DayOfWeek.Tuesday, "úterý" },
            { DayOfWeek.Wednesday, "středa" },
            { DayOfWeek.Thursday, "čtvrtek" },
            { DayOfWeek.Friday, "pátek" },
            { DayOfWeek.Saturday, "sobota" },
            { DayOfWeek.Sunday, "neděle" }
        };

        private string CurrentDaySelector
        {
            get => $"[data-groups='day-menu-{DayMapping.GetValueOrDefault(DateTime.Now.DayOfWeek)}']";
        }

        public async Task<MenuSection[]> ParseDay(IDocument document)
        {
            var todayData = document.QuerySelectorAll(CurrentDaySelector);
            var dayData = todayData[0];
            var weekData = todayData[1];


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
            var mealsData = data.QuerySelectorAll("table tr").ToList().FindAll(row => row.ChildElementCount == 2);
            var meals = new List<Meal> { };

            foreach(var item in mealsData)
            {
                meals.Add(new Meal
                {
                    name = Normalize(item.FirstElementChild.TextContent),
                    price = Normalize(item.LastElementChild.TextContent)
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
