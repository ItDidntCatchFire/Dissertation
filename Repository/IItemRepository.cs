using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository {
	public interface IItemRepository<T, U> : IRepository<T, U>, IList<T>
		where T : class {
		Task<IEnumerable<T>> ListAsync();
		Task<IEnumerable<T>> InsertListAsync(IEnumerable<T> types);
	}
}