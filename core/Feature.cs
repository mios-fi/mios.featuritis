using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Mios.Featuritis {
	public class Feature {
		static Feature() {
			Toggler = new NullToggler();
		}
		class NullToggler : IToggler {
			public IEnumerable<string> Toggles {
				get { return Enumerable.Empty<string>(); }
			}
			public bool? IsEnabled(string toggle) {
				return null;
			}
		}
		public static IToggler Toggler { get; set; }

		public bool IsEnabled {
			get { return Toggler.IsEnabled(Name).GetValueOrDefault(false); } 
		}
		public string Name { get; private set; }
		public string Description { get; private set; }

		public Feature(string name, string description = null) {
			Name = name;
			Description = description;
		}

		public static IEnumerable<Feature> DefinedIn<T>() {
			return DefinedIn(typeof(T));
		}
		public static IEnumerable<Feature> DefinedIn(Type type) {
			return type
				.GetFields(BindingFlags.Static|BindingFlags.Public)
				.Where(t => typeof(Feature).IsAssignableFrom(t.FieldType))
				.Select(t => t.GetValue(null))
				.Cast<Feature>()
				.ToArray();
		}
	}
}