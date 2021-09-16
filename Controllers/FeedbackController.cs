
using System.Threading.Tasks;
using luncher_api.Models;
using luncher_api.Models.Api;
using luncher_api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace luncher_api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class FeedbackController : ControllerBase
	{
		private FeedbackRepository _repository;

		public FeedbackController(FeedbackRepository repository)
		{
			_repository = repository;
		}

		[HttpPost("contact")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> PostContactFeedback(ContactFeedback feedback)
		{
			await _repository.Commit(new FeedbackItem
			{
				Name = feedback.name,
				Note = feedback.note
			});

			return Created("", feedback);
		}

		[HttpPost("addRestaurant")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> PostRestaurantFeedback(RestaurantFeedback feedback)
		{
			await _repository.Commit(new FeedbackItem
			{
				Name = feedback.name,
				Note = feedback.note,
				RestaurantName = feedback.restaurantName,
				RestaurantUrl = feedback.restaurantUrl
			});

			return Created("", feedback);
		}
	}
}