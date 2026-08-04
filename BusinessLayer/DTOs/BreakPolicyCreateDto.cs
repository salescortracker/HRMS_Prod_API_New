using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class BreakPolicyCreateDto
    {
        public int CompanyId { get; set; }
        public int RegionId { get; set; }
        public int UserId { get; set; }

        public string PolicyCode { get; set; }
        public string PolicyName { get; set; }
        public string BreakType { get; set; }

        public int DurationMinutes { get; set; }
        public int MaxBreaksPerDay { get; set; }
        public int GraceMinutes { get; set; }

        public int ShiftId { get; set; }

        public bool IsActive { get; set; }

        public TimeOnly? BreakFromTime { get; set; }
        public TimeOnly? BreakToTime { get; set; }
    }
}
