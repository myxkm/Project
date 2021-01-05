using System.Collections.Generic;
using System.Threading.Tasks;

namespace coreapi.Repository
{
    public interface IRepository<T,P>
        where T : class
        where P : class
    {
        Task<IEnumerable<T>> GetPageList(P item);
        Task<IEnumerable<T>> GetAllList();
        Task<T> GetOne(T item);
        Task AddOne(T item);
        Task<bool> RemoveOne(T item);
        Task<bool> UpdateOne(T item);
    }


}
