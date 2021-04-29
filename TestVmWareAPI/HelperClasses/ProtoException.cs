using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NCCVDIAdminCCMS.HelperClasses {
	public class ProtoException : Exception {
		public ProtoException() : base() { }
		public ProtoException(string message) : base(message) { }
		public ProtoException(string message, Exception innerException) : base(message, innerException) { }
		public ProtoException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}

	public class UserExpireException : Exception {
		public UserExpireException() : base() { }
		public UserExpireException(string message) : base(message) { }
		public UserExpireException(string message, Exception innerException) : base(message, innerException) { }
		public UserExpireException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}