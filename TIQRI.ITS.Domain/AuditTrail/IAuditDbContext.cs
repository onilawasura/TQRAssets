using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIQRI.ITS.Domain.Models;

namespace TIQRI.ITS.Domain.AuditTrail
{
    public interface IAuditDbContext
    {
        DbSet<AuditLog> AuditLogs { get; set; }
        DbChangeTracker ChangeTracker { get; }
    }
}
