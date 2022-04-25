using System.Threading.Tasks;

using koalunch_api.Models.Api;

namespace koalunch_api.Repositories
{
	public class VisitorRepository : IRepository<Visitors>
	{
		public async Task<Visitors[]> GetAll()
		{
			return await Task.FromResult(new Visitors[] { });
		}

		public Task<Visitors> GetById(string id)
		{
			throw new System.NotImplementedException();
		}
	}
}