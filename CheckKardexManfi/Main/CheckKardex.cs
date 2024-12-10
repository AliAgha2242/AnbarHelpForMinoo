using additions;
using Dapper;
using Database;
using Database.Connnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckKardexManfi
{
    public class CheckKardex
    {
        private readonly DatabaseConfigure DbConfigure;

        public CheckKardex()
        {
            this.DbConfigure = new DatabaseConfigure();
        }
        public BaseResult<(string, string, double)> GetServerAndDbNameBySanad(double[] sanadSn)
        {
            if (DbConfigure.checkConnection().TObject == false)
            {
                return new BaseResult<(string, string, double)>(("", "", 0), "ارتباط شما برقرار نیست", false);
            }
            using (var con = DbConfigure.ConnectionBuilder())
            {
                List<double> VahedeTejariSn = new List<double>();
                try
                {
                    foreach (int item in sanadSn)
                    {
                        VahedeTejariSn.Add(con.QuerySingle<double>(Queries.GetVahedeTtejariBySanad.Replace("MySanadSN", sanadSn[item].ToString())));
                    }
                    if (VahedeTejariSn.Count() <= 0)
                        return new BaseResult<(string, string, double)>(("", "", 0), "واحد تجاری برای اولین سند یافت نشد ", false);
                    else if (VahedeTejariSn.Count() > 2)
                    {
                        return new BaseResult<(string, string, double)>(("", "", 0), "اسناد از چند واحد تجاری میباشد. لطفا از یک واحد تجاری سند جدا کنید", false);
                    }
                }
                catch (Exception e)
                {
                    return new BaseResult<(string, string, double)>(("", "", 0), "سند وارد شده نا معتبر است \n واحد تجاری ، سرور ، یا پایگاه داده سند یافت نشد ", false);
                }


                var ServerName = con.Query<(string, string)>(Queries.GetServerNameAndDbNameByVahedeTejariSN.Replace("MyVahedeTejariSN", VahedeTejariSn.First().ToString())).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(ServerName.Item1))
                    return new BaseResult<(string, string, double)>(("", "", 0), "سرور واحد تجاری شما یافت نشد", false);


                return new BaseResult<(string, string, double)>((ServerName.Item1, ServerName.Item2, VahedeTejariSn.First()));
            }
        }

        public BaseResult<List<CheckKardexManfiResult>> CheckKardexMain(double[] SanadS, string serverName, string DbName)
        {
            try
            {
                DbConfigure.CSBuilder(serverName, DbName);
                using (var conn = DbConfigure.ConnectionBuilder())
                {
                    string sanadStringForQuery = string.Join(',', SanadS);
                    string query = Queries.CheckKardexManfi.Replace("Varible", sanadStringForQuery);
                    List<CheckKardexManfiResult> result = conn.Query<CheckKardexManfiResult>(query).ToList();
                    return new BaseResult<List<CheckKardexManfiResult>>(result);
                }
            }
            catch (Exception e)
            {
                return new BaseResult<List<CheckKardexManfiResult>>(new List<CheckKardexManfiResult>(), e.Message, false);
            }

        }
    }
}
