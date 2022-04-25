using Microsoft.EntityFrameworkCore;

namespace koalunch_api.Models
{
	public class FeedbackContext: DbContext
	{
        private bool _created = false;

		public DbSet<FeedbackItem> FeedbackItems { get; set; }

        public FeedbackContext(DbContextOptions<FeedbackContext> options) 
            :base(options)
        {
            if (!_created) 
            {
                _created = true;
                Database.EnsureCreated();
            }
        }
	}

}