using System;
using System.Collections.Generic;

namespace DataAccessLayer.DBContext;

public partial class EmployeeOfferLetter
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

    public decimal? AnnualPackage { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public string? OfferLetterPath { get; set; }

    public bool? IsSent { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
