namespace TestVmWareAPI
{
    public class VMProvisioningModel
    {
        public string MasterVMName { get; set; }
        public string TempVMName { get; set; }
        public string StorageName { get; set; }
        public string VMName { get; set; }
        public int CPU { get; set; }
        public int Memory { get; set; }
        public int Disk { get; set; }
        public string NIC { get; set; }
        public string Workflow { get; set; }
        public string Vcenter { get; set; }
        public string VcenterUUID { get; set; }
        public string ClusterName { get; set; }
        public string HostName { get; set; }
        public string DatastoreName { get; set; }
        public string CloneResource { get; set; }
        public string SetVMResource { get; set; }
        public string StorageFormat { get; set; }
        public string Language { get; set; }
    }
}