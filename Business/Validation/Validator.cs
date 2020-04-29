namespace Business.Validation {
	public static class Validator {
		public static ValidationResult ValidateModel<T>(this T subject) where T : Models.IModel {
			return new ValidationResult(subject.Validate());
		}
	}
}