using System.Threading.Tasks;

using AngleSharp;
using AngleSharp.Dom;

namespace koalunch_api.MenuParsers
{
	public class HtmlDocumentContext: IHtmlDocumentContext
	{

		private BrowsingContext _browsingContext;

		public HtmlDocumentContext(BrowsingContext browsingContext)
		{
			_browsingContext = browsingContext;
		}

		public async Task<IDocument> LoadDocument(string url)
		{
			return await _browsingContext.OpenAsync(url);
		}

		public static BrowsingContext CreateDefaultBrowsingContext()
		{
			return new BrowsingContext(Configuration.Default.WithDefaultLoader());
		}
	}
}