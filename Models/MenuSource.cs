
using koalunch_api.MenuParsers;
using koalunch_api.Models.Api;

namespace koalunch_api.Models
{
	public class MenuSource
	{
		public Restaurant Restaurant { get; set; }
		public string MenuUrl { get; set; }
		public MenuType Type { get; set; }
		public Parser Parser { get; set; }
	}
}