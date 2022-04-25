using System.Threading.Tasks;

using koalunch_api.Models.Api;

namespace koalunch_api.MenuParsers
{
	public interface PDFInfoProvider
	{
		Task<PDFInfo> GetDayInfo();
	}
}