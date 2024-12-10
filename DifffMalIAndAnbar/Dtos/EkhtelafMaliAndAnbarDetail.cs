using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifffMalIAndAnbar.Dtos
{
    public class EkhtelafMaliAndAnbarDetail
    {
        public IList<Sanads> sanadThatsNotFound { get; set; }
        public IList<Sanads> sanadThatsRemoveFromSanad { get; set; }
        public IList<Sanads> SanadThatsUpdatedDate { get; set; }
        public IList<Sanads> SanadWith2SanadNo { get; set; }
    }

    public class Sanads
    {
        public string sanadSN { get; set; }
        public string sanadNo { get; set; }
    }
}
