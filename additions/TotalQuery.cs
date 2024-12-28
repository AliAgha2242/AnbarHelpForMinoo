using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace additions
{
    public class TotalQuery
    {
        public string GetEmptyUserCode { get; set; } = @"declare @From  as int = 100
                                                         Declare @To as Int = @From + 500
                                                         Declare @T as Table(UserEmptyCode  int)
                                                         while (@From < @To)
                                                         begin
                                                         	insert @T(UserEmptyCode)
                                                         	Values(@From)
                                                         
                                                         
                                                         	select @From = @From+1 ;
                                                         ENd
                                                         
                                                         select UserEmptyCode from Users 
                                                         right join @T as T on Cast(UserEmptyCode as varchar) = users.userCode 
                                                         where UserCode is null";
    }
}
