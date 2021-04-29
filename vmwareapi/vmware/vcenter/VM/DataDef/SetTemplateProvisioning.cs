using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.vcenter.VM.DataDef
{
    public class SetTemplateProvisioning
	{
		public string ClusterId { get; set; }
		public string ClusterName { get; set; }
		public string ResourcePoolId { get; set; }
		public string ResourcePoolName { get; set; }
		public string VMFolderId { get; set; }
		public string VMFolderName { get; set; }
		public string VMTemplateId { get; set; }
		public string VMTemplateName { get; set; }
		public string VMName { get; set; }
		public string DatastoreId { get; set; }
		public string DatastoreName { get; set; }
		public string SnapshotId { get; set; }
		public string SnapshotName { get; set; }
		public string CreateVMName { get; set; }
		public string LinkedVMName { get; set; }

		/*
		public List<Datastore> DatastoreList { get; set; }

		public class Datastore
		{
			public string DatastoreId { get; set; }
			public string DatastoreName { get; set; }
		}
		 */
	}
}
