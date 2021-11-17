using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIQRI.ITS.Domain.Models
{
    [Serializable]
    public class UserProfile : EntityBase
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string CurrentProject { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string ExpericeYears { get; set; }
        public string Age { get; set; }
        public string Expertise { get; set; }
        public string YearsAtExilesoft { get; set; }

    }
}
