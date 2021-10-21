using System.Threading.Tasks;

using AngleSharp.Dom;

namespace koalunch_api.MenuParsers
{
	public interface IHtmlDocumentContext
	{
		Task<IDocument> LoadDocument(string url);
	}
}