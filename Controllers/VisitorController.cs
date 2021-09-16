
using Microsoft.AspNetCore.Mvc;

namespace luncher_api.Controllers
{
	[ApiController]
	[Route("api/stats/visitors")]
	public class VisitorController : ControllerBase
	{
		public VisitorController() { }

		[HttpGet]
		public void Get()
		{
		}
	}
}