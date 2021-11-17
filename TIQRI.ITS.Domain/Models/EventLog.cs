using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIQRI.ITS.Domain.Models
{
    [Serializable]
    public class EventLog : EntityBase
    {
        public string Form { get; set; }
        public string AssetNumber { get; set; }
        public string Action { get; set; }
        public string Log { get; set; }
        public string Username { get; set; }
        public DateTime EventTime { get; set; }

    }
}
