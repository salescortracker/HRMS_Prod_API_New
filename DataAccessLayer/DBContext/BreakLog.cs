using System;
using System.Collections.Generic;

namespace DataAccessLayer.DBContext;

public partial class BreakLog
{
    public int BreakLogId { get; set; }

    public int ClockInOutId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public int UserId { get; set; }

    public int BreakPolicyId { get; set; }

    public DateTime BreakInTime { get; set; }

    public DateTime? BreakOutTime { get; set; }

    public int? BreakMinutes { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool? IsDeleted { get; set; }
}
