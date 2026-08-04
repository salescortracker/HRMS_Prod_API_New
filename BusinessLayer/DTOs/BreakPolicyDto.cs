using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class BreakPolicyDto
    {
        public long BreakPolicyId { get; set; }

        public long CompanyId { get; set; }
        public string CompanyName { get; set; }

        public long RegionId { get; set; }
        public string RegionName { get; set; }

        public long ShiftId { get; set; }
        public string ShiftName { get; set; }

        public long UserId { get; set; }

        public string PolicyCode { get; set; }
        public string PolicyName { get; set; }
        public string BreakType { get; set; }

        public int DurationMinutes { get; set; }
        public int MaxBreaksPerDay { get; set; }
        public int? GraceMinutes { get; set; }

        public bool IsActive { get; set; }

        public TimeOnly? BreakFromTime { get; set; }
        public TimeOnly? BreakToTime { get; set; }
    }
}
