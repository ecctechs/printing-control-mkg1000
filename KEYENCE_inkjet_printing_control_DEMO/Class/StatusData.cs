using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEYENCE_inkjet_printing_control_DEMO.Class
{
    public class StatusData
    {
        public Dictionary<string, string> StatusCodes { get; set; }
        public Dictionary<string, string> ErrorCodes { get; set; }
        public Dictionary<string, string> WarningCodes { get; set; }
    }
}
