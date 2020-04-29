using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository {
	public interface IInventoryRepository<T, U> : IRepository<IEnumerable<T>, U>, IList<T>
		where T : class {
		Task<IEnumerable<T>> ListAsync();
	}
}