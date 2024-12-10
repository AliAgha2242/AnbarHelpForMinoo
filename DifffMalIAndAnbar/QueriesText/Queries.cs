using additions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifffMalIAndAnbar.QueriesText
{
    public class Queries
    {

        static Queries()
        {
            DiffMaliAndAnbarBuild();
        }

        public static string fromDate { get; set; }
        public static string toDate { get; set; }
        public static string GetServer { get; set; } = @"select 
                                                Concat(ServerDS,' _ ',
                                                (select VahedeTejariDs from paVahedeTejari where VahedeTejariSN = 
                                                mwServer.VahedeTejariSN)) as ServerFullName
                                                from  mwServer";
        public static string DiffMaliAndAnbarTotal { get; set; } =@"
                                                               Declare 
                                                                   @vahedetejariSn decimal(18,3) 
                                                                 , @TarakoneshSN varchar(8000)                    
                                                                 , @AsDate varchar(10)                  
                                                                 , @TaDate varchar(10)  
                                                               
                                                               Select 
                                                                   @vahedetejariSn  ='0'
                                                                      , @TarakoneshSN='0'
                                                                      , @AsDate = 'MyFromDate'
                                                                      , @TaDate = 'MyToDate'
                                                                      
                                                                 -----------------------------------------------------------------------------
                                                               if (select object_id('tempdb.dbo.#VahedeTejariSN')) is not null drop table dbo.#VahedeTejariSN        
                                                               SELECT cast(Col1 as decimal(18,3)) as VahedeTejariSN INTO #VahedeTejariSN From dbo.StrToTable(@VahedeTejariSN)  
                                                               
                                                               if (select object_id('tempdb.dbo.#TarakoneshSN')) is not null drop table dbo.#TarakoneshSN       
                                                               SELECT cast(Col1 as decimal(18,3)) as TarakoneshSN INTO #TarakoneshSN From dbo.StrToTable(@TarakoneshSN)  
                                                               -----------------------------------------------------------------------------------
                                                                      declare @Standard table    
                                                                      (    
                                                                      KalaSN decimal(18,3)    
                                                                      ,NerkhStandard float 
                                                               	   ,StartDate char(8)
                                                               	   ,EndDate char(8)
                                                                      )  
                                                                      insert into @Standard    
                                                                      Select KalaSN    
                                                                      , NerkhStandard = isnull(NerkhStandard,0)  
                                                               	   ,StartDate
                                                               	   ,EndDate
                                                                      From paKalaNerkh    
                                                                      where StartDate = left(@AsDate,4) + '0101'  
                                                                      --------------------------------------------------------------------------------  
                                                                     if (select object_id('tempdb.dbo.#abSanad')) is not null drop table dbo.#abSanad  
                                                                             Select
                                                                                    TarakoneshSN_Asli = 
                                                                                                        Case 
                                                                                                               when abSanad.TarakoneshSN in ( 116,117 ) then 91
                                                                                                               when abSanad.TarakoneshSN in ( 6,7 ) then 41
                                                                                                               when abSanad.TarakoneshSN in ( 25 ) then 38
                                                                                                               when abSanad.TarakoneshSN in (45 , 95) then
                                                                                                               (
                                                                                                                 Select Case when sq_absanad.TarakoneshSN in ( 25 ) then 38 else sq_absanad.TarakoneshSN end
                                                                                                                 From   absanad       sq_absanad
                                                                                                                 Where
                                                                                                                 sq_absanad.sanadSn = (Select       MarjaSanadSN 
                                                                                                                                       From   absanad sq 
                                                                                                                                        Where  sq.sanadsn = abSanad.SanadSN 
                                                                                                                                          )
                                                                                                                             and not sq_absanad.TarakoneshSN in (45 , 95)
                                                                                                               )      
                                                                                                               else abSanad.TarakoneshSN  
                                                                                                        end 
                                                                                                        , *
                                                                             into #abSanad
                                                                             From abSanad 
                                                                             Where  abSanad.SanadDate between @AsDate and @TaDate
                                                               			  --and abSanad.taeeddate<=@TaDate
                                                                             and ( @VahedeTejarisn = '0' or abSanad.VahedetejariSN in (select VahedeTejarisn  from  #VahedeTejariSN) )   
                                                                             and ( @TarakoneshSN = '0' or  abSanad.TarakoneshSN in (select TarakoneshSN  from #TarakoneshSN ) )
                                                               			  and SanadStatus=8 
                                                                             --------------------------------------------------------------------------------                             
                                                                             if (select object_id('tempdb.dbo.#abSanadha')) is not null drop table dbo.#abSanadha   
                                                                             SELECT   
                                                                                       absanadha.SanadHaSN
                                                                                    , absanadha.SanadSN
                                                                                    , absanadha.KalaSN
                                                                                    , Varedeh_T = sum(isnull(MeghdareVaredeh,0))
                                                                                    , Sadereh_T = sum(isnull(MeghdareSadereh,0))
                                                                                    , Varedeh_R_S = sum(isnull(MeghdareVaredeh,0) * isnull(tStandard.NerkhStandard,0))
                                                                                    , Sadereh_R_S = sum(isnull(MeghdareSadereh,0) * isnull(tStandard.NerkhStandard,0))
                                                                                    , MablaghStandard = sum((isnull(MeghdareVaredeh,0) - isnull(MeghdareSadereh,0)) * tStandard.NerkhStandard)
                                                                             into #absanadha
                                                                             From absanadha 
                                                                                          join abSanad  ON absanadha.SanadSN = abSanad.SanadSN
                                                                                          left join @Standard tStandard on absanadha.KalaSN = tStandard.KalaSN and SanadDate between StartDate And EndDate
                                                                             Where abSanad.SanadDate between @AsDate and @TaDate
                                                               			  --and abSanad.taeeddate<=@TaDate
                                                                             and ( @VahedeTejarisn = '0' or abSanad.VahedetejariSN in (select VahedeTejarisn  from  #VahedeTejariSN) )   
                                                                             and ( @TarakoneshSN = '0' or  abSanad.TarakoneshSN in (select TarakoneshSN  from #TarakoneshSN ) ) 
                                                                             Group by absanadha.SanadHaSN , absanadha.KalaSN, absanadha.SanadSN
                                                               
                                                               
                                                                             --------------------------------------------------------------------------------                       
                                                                             if (select object_id('tempdb.dbo.#Sanad_R_T')) is not null drop table dbo.#Sanad_R_T
                                                                             Select  
                                                                                             VahedetejariDS
                                                                                          , abSanad.VahedetejariSN
                                                                                          , TaminVahedeTejariNo = isnull(TaminVahedeTejariNo,'')
                                                                                          , TaminVahedeTejariDs = isnull(TaminVahedeTejariDs,'')
                                                                                          , abSanad.Sanadsn
                                                               						   , SanadHaSN
                                                                                          , abSanad.SanadNo                
                                                                                           , SanadDate         
                                                                                          , abAnbar.AnbarSN                              
                                                                                           , AnbarDS        
                                                                                           , TarakoneshSN_Asli
                                                                                          , TarakoneshDs=case when  TarakoneshDs in ('حواله کاهنده خريد','حواله برگشت از خريد داخلي') then 'حواله برگشت از خريد داخلي'
                                                                                           else TarakoneshDs end
                                                                                          , absanadha.KalaSN
                                                                                          , pakala.KalaNo
                                                                                          , pakala.KalaDS
                                                                                          , Varedeh_T = isnull(Varedeh_T,0)
                                                                                          , Sadereh_T = isnull(Sadereh_T,0)
                                                                                          , Varedeh_R_S = isnull(Varedeh_R_S,0)
                                                                                          , Sadereh_R_S = isnull(Sadereh_R_S,0)
                                                                                          , MablaghStandard_V = case when TarakoneshSN_Asli < 50 then MablaghStandard else 0 end
                                                                                          , MablaghStandard_S = case when TarakoneshSN_Asli >= 50 then MablaghStandard else 0 end
                                                                                          , abSanad.ShomarehSefaresh
                                                                                          , abSanad.Tozih
                                                                             into #Sanad_R_T
                                                                             From   #absanadha absanadha 
                                                                                                 JOIN #abSanad abSanad  ON absanadha.SanadSN = abSanad.SanadSN
                                                                                                 JOIN abAnbar ON abAnbar.AnbarSN = abSanad.AnbarSN        
                                                                                                 JOIN paVahedetejari ON paVahedetejari.VahedetejariSN = abSanad.VahedetejariSN        
                                                                                                 JOIN paKala   ON abSanadHa.KalaSN = paKala.KalaSN        
                                                                                                 JOIN abTarakonesh   ON abSanad.TarakoneshSN_Asli = abTarakonesh.TarakoneshSN        
                                                                                                 left join paVw_PaKalaTamin on  pakala.KalaSN =  paVw_PaKalaTamin.KalaSN
                                                               
                                                               --جهت کنترل موجودی ریالی استاندارد انبار با قیمت استاندارد اسناد انبار
                                                               --select sanadsn,sanadhasn,MablaghStandard_S,MablaghStandard_V from #Sanad_R_T 
                                                               --where TarakoneshSN_Asli=77 and SanadDate='14000504'
                                                               --order by sanadsn
                                                               
                                                               --select  * from #Sanad_R_T
                                                               
                                                               --select  distinct SanadSN from #Sanad_R_T
                                                               
                                                               --------------------------------------------------------------------------------------------------------------
                                                               Select anbar.VahedeTejariSN, Anbar_VahedeTejariDs=anbar.VahedeTejariDs
                                                               ,anbar.TarakoneshSN_Asli
                                                                     ,Anbar_TarakoneshDs=Convert(varchar,anbar.TarakoneshDs)
                                                                        ,Anbar_SanadDate=anbar.SanadDate
                                                                        ,Anbar_Mojoodi_Standard=isnull(anbar.Mojoodi_R_S,0)
                                                                        ,Mali_VahedeTejariDs=mali.VahedeTejariDs
                                                                     ,Mali_SharhSanad=mali.SharhSanad
                                                                        ,Mali_SanadDate=mali.SanadDate
                                                                        ,Mali_Mandeh_Standard=isnull(mali.Mandeh_S,0)
                                                                        ,Ekhtelaf_Standard=isnull(Mojoodi_R_S,0)-isnull(Mandeh_S,0)
                                                               from
                                                               (
                                                               Select VahedetejariSN 
                                                                      , VahedetejariDS 
                                                                      , TarakoneshDs
                                                               		,TarakoneshSN_Asli
                                                                      , SanadDate
                                                                      , Mojoodi_R_S=sum(Mojoodi_R_S)
                                                               from 
                                                                      (Select       VahedetejariSN 
                                                                      , VahedetejariDS  
                                                                      , SanadDate
                                                               	   ,TarakoneshSN_Asli
                                                                      , TarakoneshDs=case when  TarakoneshDs in ('حواله کاهنده خريد','حواله برگشت از خريد داخلي') then 'حواله برگشت از خريد داخلي'
                                                                                           else TarakoneshDs end
                                                                      , Mojoodi_R_S = Sum(isnull(Varedeh_R_S,0)) - Sum(isnull(Sadereh_R_S,0))    
                                                                      From   #Sanad_R_T
                                                                      where not TarakoneshSN_Asli in ( 44  ) 
                                                                      Group by VahedetejariSN,VahedetejariDS ,TarakoneshDs, SanadDate,TarakoneshSN_Asli
                                                                      having Sum(isnull(Varedeh_R_S,0)) - Sum(isnull(Sadereh_R_S,0))       <> 0 
                                                                      )t
                                                                      Group by VahedetejariSN 
                                                                      , VahedetejariDS 
                                                                      , TarakoneshDs
                                                               	   ,TarakoneshSN_Asli
                                                                      , SanadDate
                                                                      )anbar
                                                               full join
                                                               (
                                                               select Vahedetejarids
                                                               , VahedetejariSN
                                                               , SanadDate
                                                               , SharhSanad
                                                               , Mandeh_S = sum(Mandeh_S) 
                                                               from 
                                                               (
                                                               Select Vahedetejarids
                                                               , VahedetejariSN
                                                               , SanadDate
                                                               , SharhSanad=case when SharhSanad='حواله تحويل به شركتهاي گروه' then 'حواله تحويل به شرکتهاي گروه'
                                                                                      when SharhSanad='رسيد دريافت از شركتهاي گروه' then 'رسيد دريافت از شرکتهاي گروه'
                                                                                      when SharhSanad='حواله مصرف شركتهاي گروه' then 'حواله مصرف شرکت هاي گروه '
                                                                                      when SharhSanad in ('حواله کاهنده خريد','حواله برگشت از خريد داخلي') then 'حواله برگشت از خريد داخلي'
                                                                                      when SharhSanad in ('حواله مصرف قطعات به شرکتهاي گروه','حواله مصرف قطعات به  شركتهاي گروه ')
                                                                                                 then 'حواله مصرف قطعات به شرکتهاي گروه '
                                                                                     when SharhSanad='حواله مصرف شرکتهاي گروه'then 'حواله مصرف شرکت هاي گروه '
                                                                                      when SharhSanad='رسيد دريافت کالاي اماني بدون ريالي' then 'رسيد خريد داخلي'
                                                                                                 else SharhSanad end
                                                                      , Mandeh_S = sum(Mandeh_S) 
                                                               from
                                                               (
                                                               Select paVahedetejari.VahedetejariSN
                                                                             , Vahedetejarids
                                                                             , SanadDate
                                                                             , Replace(replace(RTrim(Ltrim(SharhSanad)),'_استاندارد',''),'_انحراف','') as SharhSanad 
                                                                             , tafsiliDS 
                                                                             , bed=sum(isnull(bed,0)) , bes=sum(isnull(bes,0))
                                                                             , Mandeh_E = Case When charindex('_انحراف',SharhSanad) > 0 then  sum(isnull(bed,0)) - sum(isnull(bes,0)) else 0 end 
                                                                             , Mandeh_S = Case When charindex('_استاندارد',SharhSanad) > 0 then  sum(isnull(bed,0)) - sum(isnull(bes,0)) else 0 end 
                                                               From masanad  with (nolock)
                                                                                    join masanadha with (nolock) on masanad.sanadsn = masanadha.sanadsn
                                                                                    --join maVwj_maMoin with (nolock) on maVwj_maMoin.moinsn = masanadha.moinsn
                                                                                    join maHesabdari with (nolock) on maHesabdari.HesabdariSN = masanad.HesabdariSN
                                                                                    join paVahedetejari with (nolock) on maHesabdari.VahedetejariSN = paVahedetejari.VahedetejariSN
                                                                                    join matafsili with (nolock)on matafsili.tafsiliSN = masanadha.TafsiliSN2
                                                               Where
                                                                      UserIDSabt = 5
                                                                      and sanaddate between @AsDate and @TaDate
                                                               	   and MoinSN=695.101
                                                                      --and GKMNo = 1612
                                                                      and tafsiliDS <> 'کنترل ضايعات'
                                                                      and tafsiliDS <> 'کنترل موجودي'
                                                               Group by Vahedetejarids,sharhSanad , tafsiliDS ,paVahedetejari.VahedetejariSN, SanadDate
                                                               ) t1
                                                               Group by Vahedetejarids,VahedetejariSN,SharhSanad,sanaddate--,sharhSanad , tafsiliDS 
                                                               )t2
                                                               Group by Vahedetejarids,VahedetejariSN,SharhSanad, SanadDate
                                                               )mali on anbar.VahedetejariSN=mali.VahedetejariSN 
                                                                         and anbar.TarakoneshDs=mali.SharhSanad
                                                                                and anbar.SanadDate=mali.SanadDate
                                                               Where isnull(anbar.Mojoodi_R_S,0)<>0
                                                               and abs(isnull(Mojoodi_R_S,0)-isnull(Mandeh_S,0))>0
                                                               --if (select object_id('tempdb.dbo.#Sanad_R_T')) is not null drop table dbo.#Sanad_R_T
                                                               --if (select object_id('tempdb.dbo.#VahedeTejariSN')) is not null drop table dbo.#VahedeTejariSN  
                                                               --if (select object_id('tempdb.dbo.#abSanadha')) is not null drop table dbo.#abSanadha  
                                                               --if (select object_id('tempdb.dbo.#abSanad')) is not null drop table dbo.#abSanad 
                                                               --if (select object_id('tempdb.dbo.#TarakoneshSN')) is not null drop table dbo.#TarakoneshSN 
                                                               
                                                               ";
        
        public static string DiffMaliAndAnbarBuild()
        {
            if (string.IsNullOrWhiteSpace(fromDate) || string.IsNullOrWhiteSpace(toDate))
            {

                var date = CalenderHelp.MiladiToShamsiBoth(DateTime.Now);
                fromDate = date.Item1;
                toDate = date.Item2;
            };
            return DiffMaliAndAnbarTotal.Replace("MyFromDate",fromDate).Replace("MyToDate",toDate);
        }
        private static string DiffAnbarAndMaliWithReverse { get; set; } = @"
                                                                        DROP TABLE IF EXISTS #Temp
                                                                        DECLARE 
                                                                        @TarakoneshDs AS VARCHAR(150) = 'TarakoneshDs',
                                                                        @SanadDate AS FoDateFull = SanadDate,
                                                                        @ErrMessage AS VARCHAR (300) = ''
                                                                        
                                                                        
                                                                        SELECT T.* 
                                                                        INTO #Temp 
                                                                        FROM(
                                                                        SELECT 
                                                                        	 SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('-',CAST (PeigiriNo AS VARCHAR),0)) AS PeigiriNo
                                                                        	,PeigiriDate 
                                                                        	,SUM(ISNULL(Bed,0)) AS Sum_MeghdarRiale
                                                                        	,SUM(ISNULL(maSanadHa.meghdar,0)) AS Sum_Meghdar
                                                                        	,SUM(ISNULL(Bed,0)) - SUM(ISNULL(Bes,0)) AS Ekhtelaf
                                                                        FROM maSanadHa 
                                                                        WHERE SanadSN IN (SELECT SanadSN FROM maSanad WHERE SharhSanad LIKE CONCAT('%',@TarakoneshDs,'%')
                                                                        				 AND SanadDate = @SanadDate)
                                                                        GROUP BY 
                                                                        	SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('-',CAST (PeigiriNo AS VARCHAR),0))
                                                                        	,PeigiriDate
                                                                        
                                                                        EXCEPT
                                                                        
                                                                        SELECT
                                                                        	SanadNO
                                                                        	,SanadDate
                                                                        	,CASE 
                                                                        		WHEN TarakoneshSN > 50 THEN SUM(ISNULL(MeghdareSadereh,0))*paKalaNerkh.NerkhStandard
                                                                        		ELSE SUM(ISNULL(MeghdareVaredeh,0))*paKalaNerkh.NerkhStandard
                                                                        	END AS Sum_MeghdarRiale
                                                                        	,SUM(ISNULL(MeghdareVaredeh,MeghdareSadereh)) AS Sum_Meghdar
                                                                        	,SUM(ISNULL(MeghdareVaredeh,0)) - SUM(ISNULL(MeghdareSadereh,0)) AS Ekhtelaf
                                                                        FROM abSanadHa
                                                                        JOIN paKalaNerkh ON paKalaNerkh.kalaSN = abSanadHa.KalaSN 
                                                                        	AND LEFT(StartDate,4) = LEFT(@SanadDate,4)
                                                                        JOIN abSanad ON abSanad.SanadSN = abSanadHa.SanadSN
                                                                        WHERe sanadDate = @SanadDate
                                                                        	AND TarakoneshSN = (SELECT TOP 1 TarakoneshSN FROM abTarakonesh 
                                                                        						WHERE TarakoneshDs LIKE CONCAT('%',@TarakoneshDs,'%'))
                                                                        GROUP BY SanadNO,SanadDate,abSanadHa.KalaSN,TarakoneshSN,NerkhStandard
                                                                        )AS T
                                                                        
                                                                        IF(SELECT COUNT(#Temp.Ekhtelaf) FROM #Temp WHERE Ekhtelaf <> 0) > 0
                                                                        BEGIN
                                                                        	SET @ErrMessage = 'سند دارای اختلاف یافت شد'
                                                                        END
                                                                        
                                                                        SELECT
                                                                        	TE.PeigiriNo,TE.PeigiriDate,TE.Sum_MeghdarRiale,TE.Sum_Meghdar,TE.Ekhtelaf,@ErrMessage
                                                                        FROM #Temp AS TE
                                                                        
                                                                        
                                                                        
                                                                        
                                                                        DROP TABLE IF EXISTS #Temp
                                                                        ---DECLARE 
                                                                        --@TarakoneshDs AS VARCHAR(150) = 'حواله فروش محصول',
                                                                        --@SanadDate AS FoDateFull = 14030208,
                                                                        --@ErrMessage AS VARCHAR (300) = ''
                                                                        
                                                                        
                                                                        SELECT T.* 
                                                                        INTO #TempReverse 
                                                                        FROM(
                                                                        SELECT
                                                                        	SanadNO
                                                                        	,SanadDate
                                                                        	,CASE 
                                                                        		WHEN TarakoneshSN > 50 THEN SUM(ISNULL(MeghdareSadereh,0))*paKalaNerkh.NerkhStandard
                                                                        		ELSE SUM(ISNULL(MeghdareVaredeh,0))*paKalaNerkh.NerkhStandard
                                                                        	END AS Sum_MeghdarRiale
                                                                        	,SUM(ISNULL(MeghdareVaredeh,MeghdareSadereh)) AS Sum_Meghdar
                                                                        	,SUM(ISNULL(MeghdareVaredeh,0)) - SUM(ISNULL(MeghdareSadereh,0)) AS Ekhtelaf
                                                                        FROM abSanadHa
                                                                        JOIN paKalaNerkh ON paKalaNerkh.kalaSN = abSanadHa.KalaSN 
                                                                        	AND LEFT(StartDate,4) = LEFT(@SanadDate,4)
                                                                        JOIN abSanad ON abSanad.SanadSN = abSanadHa.SanadSN
                                                                        WHERe sanadDate = @SanadDate
                                                                        	AND TarakoneshSN = (SELECT TOP 1 TarakoneshSN FROM abTarakonesh 
                                                                        						WHERE TarakoneshDs LIKE CONCAT('%',@TarakoneshDs,'%'))
                                                                        GROUP BY SanadNO,SanadDate,abSanadHa.KalaSN,TarakoneshSN,NerkhStandard
                                                                        
                                                                        Except
                                                                        SELECT 
                                                                        	 SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('-',CAST (PeigiriNo AS VARCHAR),0)) AS PeigiriNo
                                                                        	,PeigiriDate 
                                                                        	,SUM(ISNULL(Bed,0)) AS Sum_MeghdarRiale
                                                                        	,SUM(ISNULL(maSanadHa.meghdar,0)) AS Sum_Meghdar
                                                                        	,SUM(ISNULL(Bed,0)) - SUM(ISNULL(Bes,0)) AS Ekhtelaf
                                                                        FROM maSanadHa 
                                                                        WHERE SanadSN IN (SELECT SanadSN FROM maSanad WHERE SharhSanad LIKE CONCAT('%',@TarakoneshDs,'%')
                                                                        				 AND SanadDate = @SanadDate)
                                                                        GROUP BY 
                                                                        	SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('-',CAST (PeigiriNo AS VARCHAR),0))
                                                                        	,PeigiriDate
                                                                        
                                                                        
                                                                        )AS T
                                                                        
                                                                        IF(SELECT COUNT(#TempReverse.Ekhtelaf) FROM #TempReverse WHERE Ekhtelaf <> 0) > 0
                                                                        BEGIN
                                                                        	SET @ErrMessage = 'سند دارای اختلاف یافت شد'
                                                                        END
                                                                        
                                                                        SELECT
                                                                        	TE.SanadNO,TE.SanadDate,TE.Sum_MeghdarRiale,TE.Sum_Meghdar,TE.Ekhtelaf,@ErrMessage
                                                                        FROM #TempReverse AS TE";
        private static string DiffMaliAndAnbar { get; set; } = @"DROP TABLE IF EXISTS #Temp
														  DECLARE 
														  @TarakoneshDs AS VARCHAR(150) = 'MyTarakoneshDs',
														  @SanadDate AS FoDateFull = MySanadDate,
														  @ErrMessage AS VARCHAR (300) = ''
														  
														  SELECT T.* 
														  INTO #Temp 
														  FROM(
														  SELECT CASE
														  	WHEN Len(SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('_',CAST (PeigiriNo AS VARCHAR),0))) <> 0
														  		THEN SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('_',CAST (PeigiriNo AS VARCHAR),0))
														  	ELSE CONVERT(VARCHAR,PeigiriNo)
														  	END AS PeigiriNo
														  	 
														  	,PeigiriDate 
														  	,SUM(ISNULL(Bed,0)) AS Sum_MeghdarRiale
														  	,SUM(ISNULL(maSanadHa.meghdar,0)) AS Sum_Meghdar
														  	,SUM(ISNULL(Bed,0)) - SUM(ISNULL(Bes,0)) AS Ekhtelaf
														  FROM maSanadHa 
														  WHERE SanadSN IN (SELECT SanadSN FROM maSanad WHERE SharhSanad LIKE CONCAT('%',@TarakoneshDs,'%')
														  				 AND SanadDate = @SanadDate)
														  GROUP BY 
														  	CASE
														  	WHEN Len(SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('_',CAST (PeigiriNo AS VARCHAR),0))) <> 0
														  		THEN SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('_',CAST (PeigiriNo AS VARCHAR),0))
														  	ELSE CONVERT(VARCHAR,PeigiriNo)
														  	End
														  	,PeigiriDate
														  
														  EXCEPT
														  
														  
														  SELECT
														  	Convert(Varchar,SanadNO) as SanadNO
														  	,SanadDate
														  	,CASE 
														  		WHEN TarakoneshSN > 50 THEN SUM(ISNULL(MeghdareSadereh,0))*paKalaNerkh.NerkhStandard
														  		ELSE SUM(ISNULL(MeghdareVaredeh,0))*paKalaNerkh.NerkhStandard
														  	END AS Sum_MeghdarRiale
														  	,SUM(ISNULL(MeghdareVaredeh,MeghdareSadereh)) AS Sum_Meghdar
														  	,SUM(ISNULL(MeghdareVaredeh,0)) - SUM(ISNULL(MeghdareSadereh,0)) AS Ekhtelaf
														  FROM abSanadHa
														  JOIN paKalaNerkh ON paKalaNerkh.kalaSN = abSanadHa.KalaSN 
														  	AND LEFT(StartDate,4) = LEFT(@SanadDate,4)
														  JOIN abSanad ON abSanad.SanadSN = abSanadHa.SanadSN
														  WHERe sanadDate = @SanadDate
														  	AND TarakoneshSN = (SELECT TOP 1 TarakoneshSN FROM abTarakonesh 
														  						WHERE TarakoneshDs LIKE CONCAT('%',@TarakoneshDs,'%'))
														  GROUP BY SanadNO,SanadDate,abSanadHa.KalaSN,TarakoneshSN,NerkhStandard
														  )AS T
														  
														  IF(SELECT COUNT(#Temp.Ekhtelaf) FROM #Temp WHERE Ekhtelaf <> 0) > 0
														  BEGIN
														  	SET @ErrMessage = 'سند دارای اختلاف یافت شد'
														  END
														  
														  SELECT
														  	TE.PeigiriNo,TE.PeigiriDate,TE.Sum_MeghdarRiale,TE.Sum_Meghdar,TE.Ekhtelaf,@ErrMessage AS Error
														  FROM #Temp AS TE";
        private static string DiffAnbarAndMali { get; set; } = @"DROP TABLE IF EXISTS #TempReverse
														  DECLARE 
														  @TarakoneshDs AS VARCHAR(150) = 'MyTarakoneshDs',
														  @SanadDate AS FoDateFull = MySanadDate,
														  @ErrMessage AS VARCHAR (300) = ''
														  
														  SELECT T.*
														  INTO #TempReverse
														  FROM
														  (SELECT DrivedTable.SanadNO,SanadDate,SUM(Sum_MeghdarRiale)AS Sum_MeghdarRiale ,SUM(Ekhtelaf) AS Ekhtelaf
														  ,SUM(Sum_Meghdar) AS Sum_Meghdar
														   
														  FROM(
														  SELECT
														  	Convert(varchar,SanadNO) AS SanadNO
														  	,absanad.SanadDate
														  	,CASE 
														  		WHEN TarakoneshSN > 50 THEN SUM(ISNULL(MeghdareSadereh,0))*paKalaNerkh.NerkhStandard
														  		ELSE SUM(ISNULL(MeghdareVaredeh,0))*paKalaNerkh.NerkhStandard
														  	END AS Sum_MeghdarRiale
														  	,SUM(ISNULL(MeghdareVaredeh,MeghdareSadereh)) AS Sum_Meghdar
														  	,0 AS Ekhtelaf
														  FROM abSanadHa
														  JOIN paKalaNerkh ON paKalaNerkh.kalaSN = abSanadHa.KalaSN 
														  	AND LEFT(StartDate,4) = LEFT(@SanadDate,4)
														  JOIN abSanad ON abSanad.SanadSN = abSanadHa.SanadSN
														  WHERe sanadDate = @SanadDate
														  	AND TarakoneshSN = (SELECT TOP 1 TarakoneshSN FROM abTarakonesh 
														  						WHERE TarakoneshDs LIKE CONCAT('%',@TarakoneshDs,'%'))
														  GROUP BY SanadNO,SanadDate,abSanadHa.KalaSN,TarakoneshSN,NerkhStandard)AS DrivedTable
														  GROUP BY DrivedTable.SanadNO,SanadDate
														  
														  
														  Except
														  SELECT CASE
														  	WHEN Len(SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('_',CAST (PeigiriNo AS VARCHAR),0))) <> 0
														  		THEN SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('_',CAST (PeigiriNo AS VARCHAR),0))
														  	ELSE CONVERT(VARCHAR,PeigiriNo)
														  	END AS PeigiriNo
														  	,PeigiriDate 
														  	,SUM(ISNULL(Bed,0)) AS Sum_MeghdarRiale
														  	,SUM(ISNULL(maSanadHa.meghdar,0)) AS Sum_Meghdar
														  	,SUM(ISNULL(Bed,0)) - SUM(ISNULL(Bes,0)) AS Ekhtelaf
														  FROM maSanadHa 
														  WHERE SanadSN IN (SELECT SanadSN FROM maSanad WHERE SharhSanad LIKE CONCAT('%',@TarakoneshDs,'%')
														  				 AND SanadDate = @SanadDate)
														  GROUP BY 
														  	CASE
														  	WHEN Len(SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('_',CAST (PeigiriNo AS VARCHAR),0))) <> 0
														  		THEN SUBSTRING(CAST(PeigiriNo AS VARCHAR),0,CHARINDEX('_',CAST (PeigiriNo AS VARCHAR),0))
														  	ELSE CONVERT(VARCHAR,PeigiriNo)
														  	END 
														  	,PeigiriDate
														  
														  
														  )AS T
														  
														  IF(SELECT COUNT(#TempReverse.Ekhtelaf) FROM #TempReverse WHERE Ekhtelaf <> 0) > 0
														  BEGIN
														  	SET @ErrMessage = 'سند دارای اختلاف یافت شد'
														  END
														  
														  SELECT
														  	TE.SanadNO,TE.SanadDate,TE.Sum_MeghdarRiale,TE.Sum_Meghdar,TE.Ekhtelaf,@ErrMessage AS Error
														  FROM #TempReverse AS TE";
        private static string MultipleQueryForMaliAndAnbar { get; set; } = @"Declare @sanadNo AS Varchar = 'MySanadNo',
                                                                           @SanadDate AS Varchar = 'MySanadDate'
                                                                         
                                                                           Drop Table If Exists OutPutTable
                                                                           
                                                                           Create Table #OutPutTable
                                                                           (
                                                                               ErrMessage varchar(500),
                                                                           	SanadSn Decimal(18,3),
                                                                           	sanadnoIsNotFound Varchar(200)
                                                                           )
                                                                           
                                                                           SELECT SanadSN
                                                                           Into #SanadsTable
                                                                           from Log.._Log_abSanad  as Lab
                                                                           where SanadNO in (select* from dbo.StrToTable(@SanadNo)) And Left(Lab.SanadDate,4) = LEFT(@SanadDate,4)
                                                                           
                                                                           
                                                                           --if(select Count(sanadSn) from #SanadHaTable) < 1
                                                                           --Begin
                                                                           --	Insert #OutPutTable(ErrMessage)
                                                                           --	Values('سند یافت نشد')
                                                                           
                                                                           --	Select* from #OutPutTable
                                                                           --	Return
                                                                           --End
                                                                          
                                                                           IF
                                                                           (select Count(Distinct Col1) from dbo.StrToTable(@sanadNo) left outer Join Log.._LOG_abSanad
                                                                           on sanadNo = Col1 and Left(SanadDate,4) = Left(@SanadDate,4))>0 
                                                                           Begin
                                                                           
                                                                               select Distinct Col1 AS sanadNo
                                                                           
                                                                               Into #sanadThatsNotFound                                     --asnadi ke peida nashodan
                                                                           	from dbo.StrToTable(@sanadNo) left outer Join Log.._LOG_abSanad
                                                                               on sanadNo = Col1 and Left(SanadDate,4) = Left(@SanadDate,4)
                                                                           End
                                                                           -----------------------------------------------------------------------------
                                                                           
                                                                           Select Lab.SanadSN
                                                                           Into #sanadThatsRemoveFromSanad
                                                                           from Log.._LOG_abSanad Lab
                                                                           left outer join absanad on absanad.sanadSn = Lab.sanadSN
                                                                           where Lab.SanadSN in (select SanadSN from #SanadsTable)
                                                                           order by Lab.Log_DateTime desc
                                                                         
                                                                                
                                                                           ;with Cte
                                                                            AS
                                                                            (
                                                                           select sanadSN, SanadDate from Log.._LOG_abSanad lab
                                                                           where lab.SanadSN in (select SanadSN from #SanadsTable) 
                                                                           group by sanadSN , SanadDate
                                                                           )
                                                                           select SanadSN Into #SanadThatsUpdatedDate from Cte
                                                                           Group by SanadSN
                                                                           having Count(SanadDate) > 1
                                                                          
                                                                           ;with Cte
                                                                            AS
                                                                            (
                                                                           select sanadSN, sanadNo from Log.._LOG_abSanad lab
                                                                           where lab.SanadSN in (select SanadSN from #SanadsTable) 
                                                                           group by sanadSN , SanadNO
                                                                           )
                                                                           select SanadSN Into #SanadWith2SanadNo from Cte
                                                                           Group by SanadSN
                                                                           having Count(sanadNo) > 1
                                                                           
                                                                           select* from #sanadThatsNotFound --table 0
                                                                           select* from #sanadThatsRemoveFromSanad --table 1
                                                                           select* from #SanadThatsUpdatedDate --table 2
                                                                           select* from #SanadWith2SanadNo --table 3
                                                                           ";

        public static string DiffMaliAndAnbarBuilder(string tarakoneshDs, string sanadDate)
        {
            return DiffMaliAndAnbar.Replace("MyTarakoneshDs", tarakoneshDs).Replace("MySanadDate", sanadDate);
        }
        public static string DiffDiffAnbarAndMaliBuilder(string tarakoneshDs, string sanadDate)
        {
            return DiffAnbarAndMali.Replace("MyTarakoneshDs", tarakoneshDs).Replace("MySanadDate", sanadDate);
        }


        public static string DiffAnbarAndMaliWithReverseBuilder(string TarakoneshDs,string SanadDate)
        {
            return DiffAnbarAndMaliWithReverse.Replace("TarakoneshDs", TarakoneshDs).Replace("SanadDate", SanadDate);
        }

        public static string MultipleQueryForMaliAndAnbarBuilder(string sanadSno,string sanadDate)
        {
            return MultipleQueryForMaliAndAnbar.Replace("MySanadNo", sanadSno).Replace("MySanadDate", sanadDate);
        }
    }
}
