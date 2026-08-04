using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class EmployeeFamilyDetailsDto
    {
        public int FamilyId { get; set; }
        public int CompanyID { get; set; }
        public int RegionID { get; set; }
        public int? UserId { get; set; }

        public string? Name { get; set; }
        public int? RelationshipId { get; set; }
        public string? Relationship { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public int? GenderId { get; set; }
        public string? Gender { get; set; }

        public string? Occupation { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public bool IsDependent { get; set; }
    }
}
