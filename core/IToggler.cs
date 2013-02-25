using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mios.Featuritis {
	public interface IToggler {
		IEnumerable<string> Toggles { get; }
		bool? IsEnabled(string toggle);
	}
}
