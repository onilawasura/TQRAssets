using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIQRI.ITS.Domain.Enums;

namespace TIQRI.ITS.Domain.AuditTrail
{
    public class AuditChange
    {
        public string DateTimeStamp { get; set; }
        public AuditType AuditActionType { get; set; }
        public string AuditActionTypeName { get; set; }
        public List<AuditDelta> Changes { get; set; }
        public string UserName { get; set; }
        public AuditChange()
        {
            Changes = new List<AuditDelta>();
        }
    }
    public class AuditDelta
    {
        public string FieldName { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
    }
}
