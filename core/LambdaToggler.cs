using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mios.Featuritis {
	public class LambdaToggler : IToggler {
		private readonly Func<bool> predicate;
		private readonly IToggler wrapped;
		public LambdaToggler(Func<bool> predicate, IToggler wrapped) {
			this.predicate = predicate;
			this.wrapped = wrapped;
		}
		public IEnumerable<string> Toggles {
			get {
				return predicate()
					? wrapped.Toggles
					: Enumerable.Empty<string>();
			}
		}
		public bool? IsEnabled(string toggle) {
			return predicate()
				? wrapped.IsEnabled(toggle)
				: null;
		}
	}
}
