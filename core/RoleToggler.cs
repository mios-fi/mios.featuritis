using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Mios.Featuritis {
	public class RoleToggler : LambdaToggler {
		public RoleToggler(string role, IToggler wrapped)
			: base(() => Thread.CurrentPrincipal.IsInRole(role), wrapped) {
		}
	}
}
