using Mios.Featuritis;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using Xunit;

namespace tests {
	public class LambdaTogglerTests {
		private IToggler wrapped;
		public LambdaTogglerTests() {
			this.wrapped = Mock.Of<IToggler>(t =>
				t.IsEnabled("a") == true &&
				t.IsEnabled("b") == false &&
				t.Toggles == new[] { "a", "b" }
			);
		}

		[Fact]
		public void EnumeratesWrappedTogglesWhenInFunctionReturnsTrue() {
			var toggler = new LambdaToggler(() => true, wrapped);
			var toggles = toggler.Toggles.ToArray();
			Assert.Equal(2, toggles.Length);
			Assert.Contains("a", toggles);
			Assert.Contains("b", toggles);
		}

		[Fact]
		public void ReturnsEmptyEnumerableWhenFunctionReturnsFalse() {
			var toggler = new LambdaToggler(() => false, wrapped);
			Assert.Empty(toggler.Toggles);
		}

		[Fact]
		public void ReturnsWrappedToggleValuesWhenFunctionReturnsTrue() {
			var toggler = new LambdaToggler(() => true, wrapped);
			Assert.Equal(true, toggler.IsEnabled("a"));
			Assert.Equal(false, toggler.IsEnabled("b"));
			Assert.Equal(null, toggler.IsEnabled("x"));
		}

		[Fact]
		public void ReturnsUndefinedWhenFunctionReturnsFalse() {
			var toggler = new LambdaToggler(() => false, wrapped);
			Assert.Equal(null, toggler.IsEnabled("a"));
		}
	}
}
