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
	public class RoleTogglerTests : IDisposable {
		IPrincipal defaultPrincipal;
		private IToggler wrapped;
		public RoleTogglerTests() {
			defaultPrincipal = Thread.CurrentPrincipal;
			Thread.CurrentPrincipal = Mock.Of<IPrincipal>(t => t.IsInRole("x") == true);
			wrapped = Mock.Of<IToggler>(t =>
				t.IsEnabled("a") == true &&
				t.IsEnabled("b") == false &&
				t.Toggles == new[] { "a", "b" }
			);
		}
		public void Dispose() {
			Thread.CurrentPrincipal = defaultPrincipal;
		}

		[Fact]
		public void EnumeratesWrappedTogglesWhenInRequiredRole() {
			var toggler = new RoleToggler("x",wrapped);
			var toggles = toggler.Toggles.ToArray();
			Assert.Equal(2, toggles.Length);
			Assert.Contains("a", toggles);
			Assert.Contains("b", toggles);
		}
		[Fact]
		public void ReturnsEmptyEnumerableWhenNotInRequiredRole() {
			var toggler = new RoleToggler("y",wrapped);
			Assert.Empty(toggler.Toggles);
		}

		[Fact]
		public void ReturnsWrappedToggleValuesWhenInRequiredRole() {
			var toggler = new RoleToggler("x",wrapped);
			Assert.Equal(true, toggler.IsEnabled("a"));
			Assert.Equal(false, toggler.IsEnabled("b"));
			Assert.Equal(null, toggler.IsEnabled("x"));
		}
		[Fact]
		public void ReturnsUndefinedWhenNotInRequiredRole() {
			var toggler = new RoleToggler("y", wrapped);
			Assert.Equal(null, toggler.IsEnabled("a"));
		}
	}	
}
