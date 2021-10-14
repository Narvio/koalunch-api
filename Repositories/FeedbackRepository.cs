using System.Threading.Tasks;

using koalunch_api.Models;

namespace koalunch_api.Repositories
{
	public interface FeedbackRepository
	{
		Task<FeedbackItem[]> GetAll();

		Task Commit(FeedbackItem feedback);
	}
}