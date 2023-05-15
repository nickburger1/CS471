using A_FGMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class VolunteerDemographicsModel
    {
        public DateTime DateOfBirth { get; set; }
        public DateTime? SeparationDate { get; set; }
        public DateTime StartDate { get; set; }
        public string? Status { get; set; }
        public bool IsActive { get; set; }
        public string? Veteran { get; set; }
        public bool IsVeteran { get; set; }
        public string? FamilyOfMilitary { get; set; }
        public bool IsFamilyOfMilitary { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public int? GenderTuid { get; set; }
        public string? Ethnicity { get; set; }
        public int? EthnicityTuid { get; set; }
        public string? RacialGroup { get; set; }
        public int? RacialGroupTuid { get; set; }
        public string? IdentifiesAs { get; set; }
        public int? IdentifiesAsTuid { get; set; }
        public string? ReasonsSeparated { get; set; }
        public int? ReasonSeparatedTuid { get; set; }
    }
}
