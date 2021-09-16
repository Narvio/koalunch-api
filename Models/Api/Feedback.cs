
namespace luncher_api.Models.Api
{
	public class ContactFeedback
	{
		public string name { get; set; }
		public string note { get; set; }
	}

	public class RestaurantFeedback : ContactFeedback
	{
		public string restaurantName { get; set; }
		public string restaurantUrl { get; set; }
	}
}