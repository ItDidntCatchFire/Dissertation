namespace Business.Validation
{
    public static class Validator 
    {
        /// <summary>
        /// Can be called from a controller to avoid referencing SGS.DATA directly.
        /// </summary>
        public static ValidationResult ValidateModel<T>(this T subject) where T : Models.IModel
        {
            return new ValidationResult(subject.Validate());
        }
    }
}