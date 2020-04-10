using DataLogic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace API {
	public static class ObjectFactory {

		public static void Init(IConfiguration configuration) {

			DataFactory.SetConfig<SqlConnection>(@"Data Source=DESKTOP-1MQKJ3E\SQLEXPRESS;Initial Catalog=Dissertation;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
		}
	}
}
