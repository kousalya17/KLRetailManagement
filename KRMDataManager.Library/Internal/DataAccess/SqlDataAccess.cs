using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRMDataManager.Library.Internal.DataAccess
{
    public class SqlDataAccess
    {
        public string GetConnectionString(string connectionString)
        {
            return ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;    
        }

        public List<T> LoadData<T, U>(string connectionStringName, U parameters, string storedProcedure)
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure,parameters,
                                commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }
        public void SaveData<T, U>(string connectionStringName, T parameters, string storedProcedure)
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
