using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifffMalIAndAnbar.Dtos
{
    public class DiffrentDto
    {
        public IEnumerable<DiffAnbarAndMali> DiffAnbarAndMali { get; set; }
        public IEnumerable<DiffMaliAndAnbar> difffMalIAndAnbar { get; set; }
    }
}
