using Mios.Featuritis;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using tests;

namespace Web.Core.Tests {
	public class MergeTogglerTests {
		[Fact]
		public void LastDefinedToggleDeterminesState() {
			Assert.Equal(false, new MergeToggler { 
				Mock.Of<IToggler>(t => t.IsEnabled("a") == true),
				Mock.Of<IToggler>(t => t.IsEnabled("a") == false)
			}.IsEnabled("a"));
		}
		[Fact]
		public void StateIsUndefinedIfNoTogglersDefineState() {
			Assert.Equal(null, new MergeToggler { 
				Mock.Of<IToggler>(t => t.IsEnabled("a") == null),
				Mock.Of<IToggler>(t => t.IsEnabled("a") == null)
			}.IsEnabled("a"));
		}
		[Fact]
		public void StateIsUndefinedIfNoTogglersAreDefined() {
			Assert.Equal(null, new MergeToggler {
			}.IsEnabled("a"));
		}
		[Fact]
		public void DefinedTogglesCanBeEnumerated() {
			var toggles = new MergeToggler {
				Mock.Of<IToggler>(t => t.Toggles==new [] { "a","b"}),
				Mock.Of<IToggler>(t => t.Toggles==new [] { "a","x"})
			}.Toggles.ToArray();
			Assert.Contains("a", toggles);
			Assert.Contains("b", toggles);
			Assert.Contains("x", toggles);
		}
		[Fact]
		public void EnumeratedTogglesAreDistinct() {
			var toggles = new MergeToggler {
				Mock.Of<IToggler>(t => t.Toggles==new [] { "a","b"}),
				Mock.Of<IToggler>(t => t.Toggles==new [] { "a","x"})
			}.Toggles.ToArray();
			Assert.Equal(3, toggles.Length);
		}
	}
}
