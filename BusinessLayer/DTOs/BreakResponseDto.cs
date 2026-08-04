using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class BreakResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int BreakLogId { get; set; }

        public DateTime? BreakInTime { get; set; }

        public DateTime? BreakOutTime { get; set; }

        public int BreakMinutes { get; set; }

        public bool IsExceeded { get; set; }
    }
}
