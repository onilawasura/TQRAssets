using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIQRI.ITS.Domain.SearchQueries
{
    public class ModelOrManufSearchQuery : SearchQueryBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
