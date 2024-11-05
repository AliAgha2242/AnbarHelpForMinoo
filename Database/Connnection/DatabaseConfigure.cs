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
        private string CN { get; set; } = @"Server=asp;DataBase=God;Integrated Security=no;
                                                                        User ID = tavakoli;password=11;TrustServerCertificate=true  ";

        public void CSBuilder(string serverName ,string dbName)
        {
            
            string newDbName = dbName == "MIS_GHSETAD" ? "MIS" : dbName;
             CN=CN.Replace("God", newDbName ).Replace("asp", serverName);
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
                return new BaseResult<bool>(false,$"ارتباط برقرار نیست //n {e}",false);
            }
        }
        public IDbConnection ConnectionBuilder()
        {
            return new SqlConnection(CN);
        }
    }
}
