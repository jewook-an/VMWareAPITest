using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.horizon.Model
{
    public class AgentSettingModel
    {
        public string DomainNetbios { get; set; }
        public string DomainFQDN { get; set; }
        public string DomainAdminID { get; set; }
        public string DomainAdminPW { get; set; }
        public string DomainSetUserID { get; set; }
        public string DomainOUDC { get; set; }
        public string AddressIP { get; set; }
        public string AddressSubnet { get; set; }
        public string AddressGateway { get; set; }
        public string AddressDNS1 { get; set; }
        public string AddressDNS2 { get; set; }
        public string AddressStateCallBack { get; set; }
    }
}
