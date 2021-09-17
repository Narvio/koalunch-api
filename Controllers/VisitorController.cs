using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using koalunch_api.Models.Api;
using koalunch_api.Repositories;

namespace koalunch_api.Controllers
{
	[ApiController]
	[Route("api/stats/visitors")]
	public class VisitorController : ControllerBase
	{
		private VisitorRepository _repository;

		public VisitorController(VisitorRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<Visitors[]> GetAll()
		{
			return await _repository.GetAll();
		}
	}
}