using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom;
using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
    public class GoaSlatina : Parser
    {
        private int CurrentDayIndex
        {
            get => (int)DateTime.Now.DayOfWeek - 1;
        }

        public async Task<MenuSection[]> ParseDay(IDocument document)
        {
            var todayDataStart = document.QuerySelectorAll("table tr h2")[CurrentDayIndex];


            return await Task.FromResult(new MenuSection[] {
                new MenuSection
                {
                    meals = ProcessMenuSection(todayDataStart)
                }
            });
        }

        private Meal[] ProcessMenuSection(IElement dayTitleElement)
        {
            var current = dayTitleElement.NextSibling;
            var meals = new List<Meal> { };


            while (current != null && current.NodeName != "H2" && current.NodeName != "P")
            {
                if (current.NodeName == "#text") {
                    var price = System.Text.RegularExpressions.Regex.Match(current.TextContent, "\\d+Kč");
                    var name = current.TextContent.Remove(price.Index, price.Length);
                    
                    meals.Add(new Meal
                    {
                        name = Normalize(name),
                        price = Normalize(price.Value)
                    });
                }
                current = current.NextSibling;
            }

            return meals.ToArray();
        }

        private string Normalize(string input)
        {
            return input.Trim();
        }
    }
}
