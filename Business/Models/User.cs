using System;
using System.Collections.Generic;

namespace Business.Models {
	public class User : IModel {
		public enum Roles {
			Owner = 1
		}

		public Guid UserId { get; set; }
		public Roles Role { get; set; }

		public IEnumerable<string> Validate() {
			var invalidReasons = new List<string>();

			return invalidReasons;
		}
	}
}