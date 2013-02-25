using Mios.Featuritis;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace tests {
	public class StaticTogglerTests {
		[Fact]
		public void ReflectsDefinedToggles() {
			var toggler = new StaticToggler { 
				{"a", true }, {"b", false}
			};
			Assert.Equal(true, toggler.IsEnabled("a"));
			Assert.Equal(false, toggler.IsEnabled("b"));
		}
		[Fact]
		public void ListsDefinedToggles() {
			var toggler = new StaticToggler { 
				{"a", true }, {"b", false}
			};
			Assert.Equal(true, toggler.IsEnabled("a"));
			Assert.Equal(false, toggler.IsEnabled("b"));
		}
	}
}
