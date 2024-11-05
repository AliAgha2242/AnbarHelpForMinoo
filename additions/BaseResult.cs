using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace additions
{
    public class BaseResult<T>
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public T TObject { get; set; }
        public BaseResult(T TObject , string ErrMes = "همه چیز مرتب است",bool IsSuccess= true)
        {
            this.TObject = TObject;
            ErrorMessage = ErrMes;
            this.IsSuccess = IsSuccess;
        }
    }
}
