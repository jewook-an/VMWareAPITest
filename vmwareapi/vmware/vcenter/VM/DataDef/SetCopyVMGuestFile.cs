using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.vcenter.VM.DataDef
{
    public class SetCopyVMGuestFile
    {
        public string Source { get; set; }

        public string Destination { get; set; }

        public string VMName { get; set; }

        public string GuestUser { get; set; }

        public string GuestPassword { get; set; }
    }
}
