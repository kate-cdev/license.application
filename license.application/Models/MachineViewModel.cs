using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license.application.Models
{
    public class MachineViewModel
    {
        public Machine Machine { get; set; }
        public List<License> Licenses { get; set; }
    }
}
