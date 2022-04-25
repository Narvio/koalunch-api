using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace koalunch_api.Models
{
	public class FeedbackItem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string RestaurantName { get; set; }
		public string RestaurantUrl { get; set; }
		public string Note { get; set; }
	}

}