using System.Threading.Tasks;

using luncher_api.Models.Api;

namespace luncher_api.Repositories
{
	public class VisitorRepository
	{
		public async Task<Visitors[]> GetAll()
		{
			return await Task.FromResult(new Visitors[] { });
		}
	}
}