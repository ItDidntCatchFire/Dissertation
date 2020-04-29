using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository {
	public interface ICheckoutRepository<T, U> : IList<T>
		where T : class {
		Task ClearAsync();

		Task InsertListAsync(IEnumerable<T> types);
	}
}