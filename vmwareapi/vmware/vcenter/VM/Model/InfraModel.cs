using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.vcenter.VM.Model
{
    public class InfraModel
    {
        public string ClusterName { get; set; }
        // 
        public string ClusterId { get; set; }
        // 
        public string HostName { get; set; }
        // 
        public string HostId { get; set; }
        // 
        public string DataStoreName { get; set; }
        // 
        public string DataStoreId { get; set; }
        // 
        public string ResourcePoolName { get; set; }
        // 
        public string ResourcePoolId { get; set; }
        // 
        public string vLanName { get; set; }
        // 
        public string vLanId { get; set; }
        // 
        public string iLocation { get; set; }
        // 검색구분 : Id, Name, Location....
        public string Search { get; set; }
        // 
        public int C { get; set; }
    }
}