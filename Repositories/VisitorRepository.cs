using System.Threading.Tasks;

using koalunch_api.Models.Api;

namespace koalunch_api.Repositories
{
	public class VisitorRepository
	{
		public async Task<Visitors[]> GetAll()
		{
			return await Task.FromResult(new Visitors[] { });
		}
	}
}