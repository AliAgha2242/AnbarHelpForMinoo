using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class Queries
    {
        public static string GetVahedeTtejariBySanad { get; set; } = "SELECT VahedeTejariSN FROM abSanad WHERE sanadSN = MySanadSN";
        public static string GetServerNameAndDbNameByVahedeTejariSN { get; set; } = "SELECT ServerDS,DbName FROM mwServer Where VahedeTejariSN = MyVahedeTejariSN";
        public static string CheckKardexManfi { get; set; } = @"--select* from absanad where SanadNO='164001452'
                                                         Declare
                                                         @FromDate  As Varchar(8), 
                                                         @ToDate As Varchar(8)
                                                         Set @FromDate = '14030101'
                                                         Set @ToDate = '14031230'
                                                         IF(SELECT object_id('tempdb..#T1')) is not null Drop Table #T1    
                                                         Select AnbarSn, KalaPhizikiSN, SanadDate, ISNULL(MeghdareVaredeh, 0) VAredeh,ISNULL(MeghdareSadereh,0) Sadereh
                                                            into #T1
                                                         from absanad
                                                         inner join absanadha on abSanad.sanadsn=absanadha.SanadSN
                                                        Where
                                                         SanadDate Between @FromDate And @ToDate
                                                         And(Sanadstatus= 8 Or Tarakoneshsn>50)
                                                         and absanad.SanadSN not in (
                                                         Varible
                                                         )
                                                         --and SanadHaSN<>3101033.181
                                                         --and KalaPhizikiSN = 164627.413
                                                         Order by AnbarSn, KalaPhizikiSN , SanadDate, ISNULL(MeghdareVaredeh,0) Desc,ISNULL(MeghdareSadereh,0)
                                                         -- Select* from #T1                                                        
                                                         IF(SELECT object_id('tempdb..#T2')) is not null Drop Table #T2    
                                                         Select
                                                         ROW_NUMBER() OVER(ORDER BY AnbarSn, KalaPhizikiSN , SanadDate, Varedeh Desc, Sadereh) AS Row,
                                                             AnbarSn, KalaPhizikiSN, SanadDate, Varedeh, Sadereh
                                                         into #T2
                                                         From #T1                 
                                                         IF(SELECT object_id('tempdb..#T3')) is not null Drop Table #T3    
                                                         Select* ,
                                                         --Sum(ISNULL(Varedeh,0) - ISNULL(Sadereh,0)) Over(Partition BY Anbarsn , Kalaphizikisn Order BY row) Mandeh
                                                        (Select Sum(ISNULL(Varedeh,0))-Sum(ISNULL(Sadereh,0)) From #t2 Where Row<=tt.Row And AnbarSn=tt.AnbarSN And KalaPhizikiSN=tt.KalaphizikiSN) Mandeh
                                                         Into #T3
                                                         from #T2 tt                          
                                                         Select  #T3.*,pakala.KalaSN,KalaNo,KalaDs,ShomarehRahgiri,Mandeh/TedadAjza Karton  From #T3
                                                         Inner join abKalaPhiziki on #T3.KalaPhizikiSN=abKalaPhiziki.KalaPhizikiSN
                                                         Inner join paKala on abKalaPhiziki.KalaSN=paKala.KalaSN
                                                         Left Join pavahedeSanjeshkala On pavahedeSanjeshKala.KalaSN=paKala.KalaSN And Pishfarz= 1
                                                         Where Mandeh<0 
                                                         Order by KalaNo, KalaDs
                                                         IF (SELECT object_id('tempdb..#T1')) is not null Drop Table #T1    
                                                         IF(SELECT object_id('tempdb..#T2')) is not null Drop Table #T2    
                                                         IF(SELECT object_id('tempdb..#T3')) is not null Drop Table #T3   ";
    }
}
