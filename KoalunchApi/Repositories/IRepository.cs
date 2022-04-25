using System.Threading.Tasks;

namespace koalunch_api.Repositories 
{
    public interface IRepository<T> 
    {
        Task<T[]> GetAll();

        Task<T> GetById(string id);
    }
}