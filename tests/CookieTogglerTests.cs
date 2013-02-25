using Mios.Featuritis;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Xunit;

namespace tests {
	public class CookieTogglerTests {
		HttpContextBase CreateContext() {
			var cookies = new HttpCookieCollection { 
				new HttpCookie("flags","a=1&b=0")
			};
			return Mock.Of<HttpContextBase>(t => t.Request.Cookies == cookies);
		}
		[Fact]
		public void EnumeratesTogglesInCookie() {
			var toggler = new CookieToggler("flags") { Context = CreateContext };
			var toggles = toggler.Toggles.ToArray();
			Assert.Equal(2, toggles.Length);
			Assert.Contains("a", toggles);
			Assert.Contains("b", toggles);
		}
		[Fact]
		public void ReflectsToggleValuesInCookie() {
			var toggler = new CookieToggler("flags") { Context = CreateContext };
			Assert.Equal(true, toggler.IsEnabled("a"));
			Assert.Equal(false, toggler.IsEnabled("b"));
			Assert.Equal(null, toggler.IsEnabled("x"));
		}
		[Fact]
		public void WithNoCookieAllTogglesAreUndefined() {
			var toggler = new CookieToggler("notFound") { Context = CreateContext };
			Assert.Equal(null, toggler.IsEnabled("x"));
			Assert.Empty(toggler.Toggles);
		}
	}	
}
