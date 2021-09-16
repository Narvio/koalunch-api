
using Microsoft.AspNetCore.Mvc;

namespace luncher_api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class FeedbackController : ControllerBase
	{
		public FeedbackController() { }

		[HttpPost("contact")]
		public void PostContactFeedback()
		{
		}

		[HttpPost("addRestaurant")]
		public void PostRestaurantFeedback()
		{
		}
	}
}