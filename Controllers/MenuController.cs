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
		MenuSourceRepository _repository;
		HtmlDocumentContext _htmlContext;

		public MenuController(MenuSourceRepository repository, HtmlDocumentContext htmlContext)
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
			var document = await _htmlContext.LoadDocument(source.MenuUrl);

			return new Menu
			{
				menus = await source.Parser.ParseDay(document),
				type = source.Type,
				restaurant = source.Restaurant
			};
		}
	}
}