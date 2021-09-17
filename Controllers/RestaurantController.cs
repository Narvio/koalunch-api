using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using koalunch_api.Models.Api;
using koalunch_api.Repositories;

namespace koalunch_api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RetaurantController : ControllerBase
	{
		private RestaurantRepository _repository;

		public RetaurantController(RestaurantRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<Restaurant[]> GetAll()
		{
			return await _repository.GetAll();
		}
	}
}