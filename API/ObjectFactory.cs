using DataLogic.DataBase;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace API {
	public static class ObjectFactory {
		public static void Init(IConfiguration configuration) {
			DataFactory.SetConfig<MySqlConnection>("server=192.168.0.114;database=Dissertation;user=dan;password=dan;");
		}
	}
}