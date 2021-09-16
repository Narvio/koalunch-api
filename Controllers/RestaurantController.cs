
using Microsoft.AspNetCore.Mvc;

using luncher_api.Models.Api;
using luncher_api.Repositories;
using System.Threading.Tasks;

namespace luncher_api.Controllers
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