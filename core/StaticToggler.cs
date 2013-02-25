using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mios.Featuritis {
	public class StaticToggler : IToggler, IEnumerable<KeyValuePair<string, bool>> {
		private readonly IDictionary<string, bool> toggles;
		public StaticToggler() {
			toggles = new Dictionary<string, bool>();
		}

		public IEnumerable<string> Toggles {
			get { return toggles.Keys; }
		}

		public bool? IsEnabled(string toggle) {
			bool state = false;
			if(toggles.TryGetValue(toggle, out state))
				return state;
			return null;
		}

		public void Add(string key, bool state) {
			toggles[key] = state;
		}

		public IEnumerator<KeyValuePair<string, bool>> GetEnumerator() {
			return toggles.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
