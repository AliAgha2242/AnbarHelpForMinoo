﻿using additions;
using Dapper;
using Database.Connnection;
using DifffMalIAndAnbar.Dtos;
using DifffMalIAndAnbar.QueriesText;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DifffMalIAndAnbar.Classes
{
    public class DiffMain
    {
        private DatabaseConfigure Db { get; set; }
        public DiffMain()
        {
            Db = new DatabaseConfigure();
        }

        public BaseResult<IEnumerable<ServerDto>> GetServerMain()
        {
            IDbConnection Conn;

            if (!Db.checkConnection(out Conn).IsSuccess)
            {
                return new BaseResult<IEnumerable<ServerDto>>(ErrMes: "اتصال شما به سرور بر قرار نیست",
                    IsSuccess: false,
                    TObject: new List<ServerDto>());
            }

            var servers = Db.GetAllT<ServerDto>(QueriesText.Queries.GetServer, Conn);
            return new BaseResult<IEnumerable<ServerDto>>(servers);

        }

        public BaseResult<IEnumerable<Dtos.DiffrentMaliAndAnbarTotalDto>> ShowDiffrent(string ServerName, string DBName, string fromDate, string toDate)
        {
            IDbConnection Con;
            Db.CSBuilder(ServerName, DBName);


            if (!Db.checkConnection(out Con).IsSuccess)
            {
                return new BaseResult<IEnumerable<DiffrentMaliAndAnbarTotalDto>>(null, "ارتباط شما به سرور انتخاب شده وصل نشد", false);
            }
            try
            {
                QueriesText.Queries.fromDate = fromDate;
                QueriesText.Queries.toDate = toDate;
                var queries = QueriesText.Queries.DiffMaliAndAnbarBuild();
                var result = Db.GetAllT<DiffrentMaliAndAnbarTotalDto>(queries, Con);
                return new BaseResult<IEnumerable<DiffrentMaliAndAnbarTotalDto>>(result);
            }
            catch (System.Exception e)
            {
                return new BaseResult<IEnumerable<DiffrentMaliAndAnbarTotalDto>>(new List<DiffrentMaliAndAnbarTotalDto>(), ErrMes: e.Message, false);
            }

        }

        public BaseResult<string> FindDb(string serverName)
        {
            Db.CSBuilder(serverName, "master");
            IDbConnection Conn;

            if (!Db.checkConnection(out Conn).IsSuccess)
            {
                return new BaseResult<string>(ErrMes: "اتصال شما به سرور بر قرار نیست",
                    IsSuccess: false,
                    TObject: "");
            }
            IEnumerable<DbDto> dbDto = Db.GetAllTWithSp<DbDto>("sp_helpdb", Conn);
            if (dbDto.Count() <= 0)
                return new BaseResult<string>("", "پایگاه داده ای برای این سرور یافت نشد", false);

            string dbName = dbDto.ToList().OrderByDescending(p => p.db_Size).First().name;

            return new BaseResult<string>(dbName);
        }
        public List<Dtos.DiffrentMaliAndAnbarTotalDto>? ControllTarakonesh111And61(IEnumerable<Dtos.DiffrentMaliAndAnbarTotalDto> diff)
        {
            if (diff.Count() < 2 || !diff.Any(d => d.TarakoneshSN_Asli == 61) || !diff.Any(d => d.TarakoneshSN_Asli == 111))
                return null;
            var DiffWithGroupBy = diff.AsQueryable().Where(p=>p.TarakoneshSN_Asli == 61 || p.TarakoneshSN_Asli == 111)
                .GroupBy(p=>p.Anbar_SanadDate);

            List<Dtos.DiffrentMaliAndAnbarTotalDto>? Result = null;
            foreach (var group in DiffWithGroupBy)
            { 
                decimal total = 0;
                foreach (var item in group)
                {
                    total = decimal.Add((decimal)item.Anbar_Mojoodi_Standard, total);
                }
                if (total != group.First().Mali_Mandeh_Standard)
                {
                    Result = new List<DiffrentMaliAndAnbarTotalDto>();
                    Result.AddRange(group.ToList());
                }
            }
            return Result;
        }

        
        public BaseResult<DiffrentDto> FindDiffrentReturnSanad(string TarakoneshDs, string SanadDate, string serverName, string dbName)
        {
            Db.CSBuilder(serverName, dbName);
            IDbConnection Conn;
            if (!Db.checkConnection(out Conn).IsSuccess)
            {
                return new BaseResult<DiffrentDto>(ErrMes: "اتصال شما به سرور بر قرار نیست",
                    IsSuccess: false,
                    TObject: new DiffrentDto());
            }
            string queryMaliAndAnbar = Queries.DiffMaliAndAnbarBuilder(TarakoneshDs, SanadDate);
            string queryAnbarAndMali = Queries.DiffDiffAnbarAndMaliBuilder(TarakoneshDs, SanadDate);
            using (Conn)
            {
                try
                {
                    var Result1 = Db.GetAllT<DiffMaliAndAnbar>(queryMaliAndAnbar, Conn);
                    var Result2 = Db.GetAllT<DiffAnbarAndMali>(queryAnbarAndMali, Conn);
                    return new BaseResult<DiffrentDto>(new DiffrentDto() { difffMalIAndAnbar = Result1, DiffAnbarAndMali = Result2 });
                }
                catch (System.Exception e)
                {

                    return new BaseResult<DiffrentDto>(new DiffrentDto(), "مشکلی در فرخوانی داده پیش آمده" + Environment.NewLine + e.Message,false);
                }
               
            }

                
        }

        public BaseResult<EkhtelafMaliAndAnbarDetail> EkhtelafMaliAndAnbarDetail(string sanadNo ,string sanadDate, string serverName, string dbName)
        {

            Db.CSBuilder(serverName, dbName);
            IDbConnection Conn;
            if (!Db.checkConnection(out Conn).IsSuccess)
            {
                return new BaseResult<EkhtelafMaliAndAnbarDetail>(ErrMes: "اتصال شما به سرور بر قرار نیست",
                    IsSuccess: false,
                    TObject: new EkhtelafMaliAndAnbarDetail());
            }

            string query = Queries.MultipleQueryForMaliAndAnbarBuilder(sanadNo, sanadDate);
            using (Conn)
            {
                //var result = Db.GetAllT<EkhtelafMaliAndAnbarDetail>(query, Conn);
                var result = Conn.QueryMultiple(query);
                var res1 = result.Read<Sanads>().ToList();
                var res2 = result.Read<Sanads>().ToList();
                var res3 = result.Read<Sanads>().ToList();
                var res4 = result.Read<Sanads>().ToList();

                return new BaseResult<EkhtelafMaliAndAnbarDetail>(new Dtos.EkhtelafMaliAndAnbarDetail()
                {
                    sanadThatsNotFound =  res1,
                    sanadThatsRemoveFromSanad = res2,
                    SanadThatsUpdatedDate = res3,
                    SanadWith2SanadNo = res4
                });
            }
        }

    }

}