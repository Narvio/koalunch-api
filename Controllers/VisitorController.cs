
using System.Threading.Tasks;
using luncher_api.Models.Api;
using luncher_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace luncher_api.Controllers
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