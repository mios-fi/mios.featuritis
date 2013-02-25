using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Mios.Featuritis {
	public class RoleToggler : IToggler {
		private readonly IToggler wrapped;
		private readonly string role;
		public RoleToggler(string role, IToggler wrapped) {
			this.wrapped = wrapped;
			this.role = role;
		}
		public IEnumerable<string> Toggles {
			get {
				return Thread.CurrentPrincipal.IsInRole(role)
					? wrapped.Toggles
					: Enumerable.Empty<string>();
			}
		}
		public bool? IsEnabled(string toggle) {
			return Thread.CurrentPrincipal.IsInRole(role)
				? wrapped.IsEnabled(toggle)
				: null;
		}
	}
}
