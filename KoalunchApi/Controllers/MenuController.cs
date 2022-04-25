using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using koalunch_api.Models;
using koalunch_api.Models.Api;
using koalunch_api.Repositories;
using koalunch_api.MenuParsers;

namespace koalunch_api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MenuController : ControllerBase
	{
		IRepository<MenuSource> _repository;
		IHtmlDocumentContext _htmlContext;

		public MenuController(IRepository<MenuSource> repository, IHtmlDocumentContext htmlContext)
		{
			_repository = repository;
			_htmlContext = htmlContext;
		}

		[HttpGet]
		public async Task<IEnumerable<Menu>> Get()
		{
			var sources = await _repository.GetAll();

			return sources
				.Select(ParseMenuFromSource)
				.Select(task => task.Result);
		}

		[HttpGet("{id}")]
		public async Task<Menu> GetSingle(string id)
		{
			var source = await _repository.GetById(id);

			return await ParseMenuFromSource(source);
		}

		private async Task<Menu> ParseMenuFromSource(MenuSource source)
		{
			var menu = new Menu
			{
				type = source.Type,
				restaurant = source.Restaurant
			};

			switch (source.Type)
			{
				case MenuType.Standard:
					{
						var document = await _htmlContext.LoadDocument(source.MenuUrl);
						menu.menus = await source.Parser.ParseDay(document);
						break;
					}
				case MenuType.PDF:
					{
						menu.pdfInfo = await source.PDFInfoProvider.GetDayInfo();
						break;
					}
			}

			return menu;
		}
	}
}