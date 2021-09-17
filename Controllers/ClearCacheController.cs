
using Microsoft.AspNetCore.Mvc;

namespace koalunch_api.Controllers
{
	[ApiController]
	[Route("api/clearCache")]
	public class ClearCacheController : ControllerBase
	{
		public ClearCacheController() { }

		[HttpGet]
		public void Get()
		{
		}
	}
}