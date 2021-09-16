using System.Threading.Tasks;
using luncher_api.Models.Api;

namespace luncher_api.MenuParsers
{
	public interface Parser
	{
		Task<MenuSection[]> ParseDay(object dom);
	}
}