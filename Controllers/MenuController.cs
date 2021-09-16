
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using luncher_api.Models.Api;

namespace luncher_api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MenuController : ControllerBase
	{
		public MenuController() { }

		[HttpGet]
		public IEnumerable<Menu> Get()
		{
			return new Menu[] {
				new Menu {
					id = "asd"
				}
			};
		}

		[HttpGet("{id}")]
		public Menu GetSingle(string id)
		{
			return new Menu
			{
				id = id
			};
		}
	}
}