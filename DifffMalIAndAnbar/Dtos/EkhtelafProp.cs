using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifffMalIAndAnbar.Dtos
{
    public class EkhtelafProp
    {
        public EkhtelafProp(string tarakoneshDs, string sanadDate)
        {
            TarakoneshDs = tarakoneshDs;
            SanadDate = sanadDate;
        }
        public string TarakoneshDs { get; set; }
        public string SanadDate { get; set; }
    }
}
