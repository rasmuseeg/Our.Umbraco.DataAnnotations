using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Our.Umbraco.DataAnnotations.Conditionals
{
    public sealed class UmbracoRequiredIfAttribute : ConditionalValidationAttribute
    {
        protected override string ValidationName
        {
            get { return "requiredif"; }
        }

        public UmbracoRequiredIfAttribute(string dependentProperty, object targetValue)
            : base(new RequiredAttribute(), dependentProperty, targetValue)
        {
        }

        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object>()
            {
                { "rule", "required" }
            };
        }
    }
}
