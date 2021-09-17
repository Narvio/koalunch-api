using System.Threading.Tasks;
using AngleSharp.Dom;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public interface Parser
	{
		Task<MenuSection[]> ParseDay(IDocument document);
	}
}