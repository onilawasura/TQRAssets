using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIQRI.ITS.Domain.Enums;
using TIQRI.ITS.Domain.Models;

namespace TIQRI.ITS.Domain.AuditTrail
{
    public class AuditEntry
    {
        public DbEntityEntry dbEntry { get; }
        public AuditType AuditType { get; set; }
        public string AuditUser { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object>
               KeyValues
        { get; } = new Dictionary<string, object>();
        public Dictionary<string, object>
               OldValues
        { get; } = new Dictionary<string, object>();
        public Dictionary<string, object>
               NewValues
        { get; } = new Dictionary<string, object>();
        public List<string> ChangedColumns { get; } = new List<string>();
        
        public AuditEntry(DbEntityEntry entry, string auditUser, string tableName)
        {
            dbEntry = entry;
            AuditUser = auditUser;
            TableName = tableName;
            SetChanges();

        }

        private void SetChanges()
        {
            string keyName = "Id";
            string assetTableName = "Assets";
            string assetNumberKeyName = "AssetNumber";
            string assetOwnerHistoryTableName = "AssetOwnerHistories";

            string primaryKeyValue = dbEntry.CurrentValues.GetValue<object>(keyName).ToString();
            KeyValues[keyName] = primaryKeyValue;
            if (TableName.Equals(assetTableName) || TableName.Equals(assetOwnerHistoryTableName))
            {
                string assetNumberValue = dbEntry.CurrentValues.GetValue<object>(assetNumberKeyName).ToString();
                KeyValues[assetNumberKeyName] = assetNumberValue;
            }

            if (dbEntry.State == EntityState.Added)
            {
                AuditType = AuditType.Create;
                foreach (string propertyName in dbEntry.CurrentValues.PropertyNames)
                {
                    if (propertyName == keyName)
                    {
                        continue;
                    }
                    NewValues[propertyName] = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
                }
            }
            else if (dbEntry.State == EntityState.Modified)
            {
                AuditType = AuditType.Update;
                foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                {
                    if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
                    {
                        if (propertyName == keyName)
                        {
                            continue;
                        }
                        ChangedColumns.Add(propertyName);
                        OldValues[propertyName] = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString();
                        NewValues[propertyName] = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
                    }
                }
            }
            else if (dbEntry.State == EntityState.Deleted)
            {
                AuditType = AuditType.Delete;
                foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                {
                    if (propertyName == keyName)
                    {
                        continue;
                    }
                    OldValues[propertyName] = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString();
                    }
                }
            }

        
        public AuditLog ToAudit()
        {
            var audit = new AuditLog();
            audit.Id = Guid.NewGuid();
            audit.AuditDateTimeUtc = DateTime.UtcNow;
            audit.AuditType = AuditType.ToString();
            audit.AuditUser = AuditUser;
            audit.TableName = TableName;
            audit.KeyValues = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count == 0 ?
                              null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ?
                              null : JsonConvert.SerializeObject(NewValues);
            audit.ChangedColumns = ChangedColumns.Count == 0 ?
                                   null : JsonConvert.SerializeObject(ChangedColumns);

            return audit;
        }
    }
    }


