using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEYENCE_inkjet_printing_control_DEMO.Class
{
    public class CurrentInkjetStatus
    {
        public DateTime Timestamp { get; set; }
        public string InkjetName { get; set; }
        public string Status { get; set; }
        public string ErrorDetail { get; set; }
        public string ErrorCode { get; set; }
        public string CurrentMessage { get; set; }
    }
}
