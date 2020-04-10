using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DataLogic {
	public static class DataFactory {
		public static string DBConnectionString { get; private set; }

		public static void SetConfig<T>(string dBConnectionString) {

			DBConnectionString = dBConnectionString;
		}

		internal static DbParameter GetParameterFromName(DbCommand cmd, string parameterName)
		{
			return cmd.Parameters[$"@{parameterName}"];
		}

		internal static DbParameter[] CreateParametersWithValue(DbCommand cmd, List<(DbType dbType, ParameterDirection direction, string parameterName, object value)> list) {
			List<DbParameter> retVal = new List<DbParameter>();
			foreach ((DbType dbType, ParameterDirection direction, string parameterName, object value) in list)
			{
				retVal.Add(CreateParameterWithValue(cmd, dbType, direction, parameterName, value));
			}

			return retVal.ToArray();
		}

		internal static DbParameter CreateParameterWithValue(DbCommand cmd, DbType dbType, ParameterDirection direction, string parameterName, object value)
		{
			DbParameter param = cmd.CreateParameter();
			param.DbType = dbType;
			param.ParameterName = $"@{parameterName}";
			param.Value = value ?? DBNull.Value;
			param.Direction = direction;
			return param;
		}
	}
}
