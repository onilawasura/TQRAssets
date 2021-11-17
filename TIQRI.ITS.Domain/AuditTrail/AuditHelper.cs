using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TIQRI.ITS.Domain.Enums;
using TIQRI.ITS.Domain.Models;

namespace TIQRI.ITS.Domain.AuditTrail
{
    public class AuditHelper
    {
        readonly IAuditDbContext Db;

        public AuditHelper(IAuditDbContext db)
        {
            Db = db;
        }

        public void AddAuditLogs(string userName)
        {
            Db.ChangeTracker.DetectChanges();
            List<AuditEntry> auditEntries = new List<AuditEntry>();
            foreach (DbEntityEntry entry in Db.ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                {
                    continue;
                }
                var tableName = GetTableName(entry);
                var auditEntry = new AuditEntry(entry, userName, tableName);
                auditEntries.Add(auditEntry);
            }

            if (auditEntries.Any())
            {
                var logs = auditEntries.Select(x => x.ToAudit());
                Db.AuditLogs.AddRange(logs);
            }
        }

        private string GetTableName(DbEntityEntry ent)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)Db).ObjectContext;
            Type entityType = ent.Entity.GetType();

            if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
                entityType = entityType.BaseType;

            string entityTypeName = entityType.Name;

            EntityContainer container =
                objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);
            string entitySetName = (from meta in container.BaseEntitySets
                                    where meta.ElementType.Name == entityTypeName
                                    select meta.Name).First();
            return entitySetName;
        }

        public IList<AuditChange> GetAudit(string Id)
        {
            List<AuditChange> rslt = new List<AuditChange>();
            var AuditTrail = Db.AuditLogs
                    .SqlQuery(@"SELECT * FROM AuditLogs
                                    WHERE JSON_VALUE(KeyValues, '$.AssetNumber') = {0} and TableName='Assets' order by [AuditDateTimeUtc] desc", Id);
            var serializer = new XmlSerializer(typeof(AuditDelta));
            foreach (var record in AuditTrail)
            {
                AuditChange Change = new AuditChange();
                Change.DateTimeStamp = record.AuditDateTimeUtc.ToString();
                Enum.TryParse(record.AuditType, out AuditType auditType);
                Change.AuditActionType = auditType;
                Change.AuditActionTypeName = record.AuditType.ToString();
                Change.UserName = record.AuditUser;
                var oldDict = record.OldValues == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(record.OldValues);
                var newDict = record.NewValues == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(record.NewValues);
                int count = newDict.Count();
                List<AuditDelta> qaList = new List<AuditDelta>(newDict.Keys.Count);
                foreach (var item in newDict)
                {
                    qaList.Add(new AuditDelta { FieldName = item.Key, ValueAfter = item.Value });
                }

                if (oldDict != null)
                {
                    foreach (var auditData in qaList)
                    {
                        var resEntity = oldDict.FirstOrDefault(x => x.Key == auditData.FieldName);
                        if (resEntity.Key != null)
                        {
                            auditData.ValueBefore = resEntity.Value;
                        }
                    }

                }

                Change.Changes.AddRange(qaList);
                rslt.Add(Change);
            }
            return rslt;
        }
    }
}
