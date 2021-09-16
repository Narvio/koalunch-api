using System.Threading.Tasks;

using luncher_api.Data;
using luncher_api.Models.Api;

namespace luncher_api.Repositories
{
	public class RestaurantRepository
	{
		public async Task<Restaurant[]> GetAll()
		{
			return await Task.FromResult(StaticRestaurants.GetAll());
		}
	}
}