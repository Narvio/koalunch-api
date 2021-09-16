
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using luncher_api.Models.Api;

namespace luncher_api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RetaurantController : ControllerBase
	{
		public RetaurantController() { }

		[HttpGet]
		public IEnumerable<Restaurant> Get()
		{
			return new Restaurant[] {
			};
		}
	}
}