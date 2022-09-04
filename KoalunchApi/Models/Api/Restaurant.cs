using System;

namespace koalunch_api.Models.Api
{
	public class RestaurantPosition
	{
		public string lat { get; set; }
		public string lng { get; set; }
	}
	public class Restaurant
	{
		public string id { get; set; }
		public string name { get; set; }
		public string url { get; set; }
		public DateTime? dateAdded { get; set; }
		public RestaurantPosition position { get; set; }
	}

}