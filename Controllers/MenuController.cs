using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using koalunch_api.Models;
using koalunch_api.Models.Api;
using koalunch_api.Repositories;

namespace koalunch_api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MenuController : ControllerBase
	{
		MenuSourceRepository _repository;

		public MenuController(MenuSourceRepository repository)
		{
			_repository = repository;
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
			return new Menu
			{
				menus = await source.Parser.ParseDay(""),
				type = source.Type,
				restaurant = source.Restaurant
			};
		}
	}
}