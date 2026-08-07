using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class EmployeeOfferLetterDto
    {
        public int EmployeeOfferLetterId { get; set; }

        public int CompanyId { get; set; }

        public int RegionId { get; set; }

        public int UserId { get; set; }

        public int EmployeeId { get; set; }

        public string? EmployeeCode { get; set; }

        public string? EmployeeName { get; set; }

        public string? Email { get; set; }

        public string? Department { get; set; }

        public string? Designation { get; set; }

        public decimal AnnualPackage { get; set; }

        public DateTime JoiningDate { get; set; }

        public string? OfferLetterPath { get; set; }

        public bool IsSent { get; set; }
    }
}
