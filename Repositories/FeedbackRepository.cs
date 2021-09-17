using System.Threading.Tasks;

using koalunch_api.Models;

namespace koalunch_api.Repositories
{
	public class FeedbackRepository
	{
		public async Task<FeedbackItem[]> GetAll()
		{
			return await Task.FromResult(new FeedbackItem[] { });
		}

		public async Task Commit(FeedbackItem feedback)
		{
			await Task.CompletedTask;
		}
	}
}