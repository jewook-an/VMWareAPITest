using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.vcenter.VM.DataDef
{
	public class SetResourceChange
	{
		public string VMHost { get; set; }
		public string VMName { get; set; }
		public int vCPU { get; set; }
		public int MemoryMB { get; set; }

		public List<Disk> Disks { get; set; }

		public class Disk
		{
			public enum DiskActionKind { None = 1, Add = 2, Change = 3, Delete = 4, }
			public string DiskName { get; set; }
			public long DiskCapacityKB { get; set; }
			public DiskActionKind DiskAction { get; set; }

			public Disk()
			{
				DiskAction = DiskActionKind.None;
			}
		}

		public SetResourceChange()
		{
			if (Disks == null)
			{
				Disks = new List<Disk>();
			}
		}
	}
}
