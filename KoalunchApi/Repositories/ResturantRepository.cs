using System.Threading.Tasks;

using koalunch_api.Data;
using koalunch_api.Models.Api;

namespace koalunch_api.Repositories
{
    public class RestaurantRepository : IRepository<Restaurant>
    {
        public async Task<Restaurant[]> GetAll()
        {
            return await Task.FromResult(StaticRestaurants.GetAll());
        }

        public async Task<Restaurant> GetById(string id)
        {
            return await Task.FromResult(StaticRestaurants.GetById(id));
        }
    }
}