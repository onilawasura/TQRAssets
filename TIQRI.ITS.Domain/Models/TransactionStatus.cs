using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIQRI.ITS.Domain.Models
{
    [Serializable]
    public class TransactionStatus
    {
        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
