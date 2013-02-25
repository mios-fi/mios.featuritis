using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mios.Featuritis {
	public class MergeToggler : IToggler, IEnumerable<IToggler> {
		private List<IToggler> togglers;
		public MergeToggler() {
			togglers = new List<IToggler>();
		}
		public void Add(IToggler toggler) {
			togglers.Add(toggler);
		}
		public IEnumerator<IToggler> GetEnumerator() {
			return togglers.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
		public IEnumerable<string> Toggles {
			get { return togglers.SelectMany(t => t.Toggles).Distinct(); }
		}
		public bool? IsEnabled(string toggle) {
			return togglers.Aggregate((bool?)null, (acc, t) => t.IsEnabled(toggle) ?? acc);
		}
	}
}
