
using luncher_api.MenuParsers;
using luncher_api.Models.Api;

namespace luncher_api.Models
{
	public class MenuSource
	{
		public Restaurant Restaurant { get; set; }
		public string MenuUrl { get; set; }
		public MenuType Type { get; set; }
		public Parser Parser { get; set; }
	}
}