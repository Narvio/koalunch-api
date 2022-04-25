using System.Linq;
using System.Threading.Tasks;

using koalunch_api.Models;

namespace koalunch_api.Repositories
{
	public class DatabaseFeedbackRepository : FeedbackRepository
	{
		private FeedbackContext _context;

		public DatabaseFeedbackRepository(FeedbackContext context)
		{
			_context = context;
		}

		public async Task<FeedbackItem[]> GetAll()
		{
			return await Task.FromResult(
				_context.FeedbackItems.ToArray()
			);
		}

		public async Task Commit(FeedbackItem feedback)
		{
			_context.Add(feedback);
			await _context.SaveChangesAsync();
		}
	}
}