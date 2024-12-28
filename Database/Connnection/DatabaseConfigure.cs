using additions;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Connnection
{
    public class DatabaseConfigure
    {
        private string CN { get; set; } = @"Server=cds;DataBase=MIS_Ghasem;Integrated Security=no;
                                                                        User ID = tavakoli;password=11;TrustServerCertificate=true ;
                                                                        Connection Timeout=30";

        public void CSBuilder(string serverName, string dbName)
        {
            CN = @"Server=cds;DataBase=MIS_Ghasem;Integrated Security=no;
                                                                        User ID = tavakoli;password=11;TrustServerCertificate=true ;
                                                                        Connection Timeout=30";

            string newDbName = dbName == "MIS_GHSETAD" ? "MIS" : dbName;
            string newServerName = serverName == "ArdebilServer " ? "Ardebils" : serverName;
            CN = CN.Replace("MIS_Ghasem", newDbName).Replace("cds", newServerName);
        }
        public BaseResult<bool> checkConnection()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(CN))
                {
                    connection.Query("select 1");
                }
                return new BaseResult<bool>(true);
            }
            catch (Exception e)
            {
                return new BaseResult<bool>(false, $"ارتباط برقرار نیست //n {e}", false);
            }
        }
        public IDbConnection ConnectionBuilder()
        {
            return new SqlConnection(CN);
        }


        public BaseResult<bool> checkConnection(out IDbConnection Conn)
        {
            try
            {
                IDbConnection connection = new SqlConnection(CN);
                connection.Query("select 1");
                Conn = connection;
                return new BaseResult<bool>(true);
            }
            catch (Exception e)
            {
                Conn = null;
                return new BaseResult<bool>(false, $"ارتباط برقرار نیست //n {e}", false);
            }
        }

        public IEnumerable<T> GetAllT<T>(string query, IDbConnection connection)
        {
            IEnumerable<T> Objects = connection.Query<T>(query);
            return Objects;
        }
        public IEnumerable<T> GetAllTWithSp<T>(string sp_Name, IDbConnection connection)
        {
            IEnumerable<T> Objects = connection.Query<T>(string.Concat(sp_Name),commandType:CommandType.StoredProcedure);
            return Objects;
        }


        
    }
}
