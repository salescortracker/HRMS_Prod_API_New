using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class EmployeePersonalDetailsDto
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        public int UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public int? GenderId { get; set; }

        public string? MobileNumber { get; set; }

        public string? PersonalEmail { get; set; }

        public string? PermanentAddress { get; set; }

        public string? PresentAddress { get; set; }

        public string? PANNumber { get; set; }

        public string? AadhaarNumber { get; set; }

        public string? PassportNumber { get; set; }

        public string? PlaceOfBirth { get; set; }

        public string? UAN { get; set; }

        public string? BloodGroup { get; set; }

        public string? Citizenship { get; set; }

        public string? Religion { get; set; }

        public string? DrivingLicence { get; set; }

        public int? MaritalStatusId { get; set; }

        public DateOnly? MarriageDate { get; set; }

        public string? WorkPhone { get; set; }

        public string? LinkedInProfile { get; set; }

        public string? PreviousExperienceText { get; set; }

        public decimal? PreviousExperienceYears { get; set; }

        public string? BandGrade { get; set; }

        public string? EsicNumber { get; set; }

        public string? PFNumber { get; set; }

        public DateTime? DateOfJoining { get; set; }

        public string? EmployeeType { get; set; }

        public string? ProfilePicture { get; set; }
    }
}
