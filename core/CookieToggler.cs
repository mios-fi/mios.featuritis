using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mios.Featuritis {
	public class CookieToggler : IToggler {
		public Func<HttpContextBase> Context { get; set; }

		private string cookieName;
		public CookieToggler(string cookieName) {
			this.cookieName = cookieName;
			Context = () => new HttpContextWrapper(HttpContext.Current);
		}
		public IEnumerable<string> Toggles {
			get {
				var context = Context();
				if(context == null)
					return Enumerable.Empty<string>();
				var cookie = context.Request.Cookies[cookieName];
				return cookie == null
					? Enumerable.Empty<string>()
					: cookie.Values.Keys.Cast<string>();
			}
		}
		public bool? IsEnabled(string toggle) {
			var context = Context();
			if(context == null)
				return null;
			var cookie = context.Request.Cookies[cookieName];
			if(cookie == null || cookie.Values[toggle] == null)
				return null;
			return cookie.Values[toggle] == "1";
		}
	}
}
