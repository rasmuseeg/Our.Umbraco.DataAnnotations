using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Our.Umbraco.DataAnnotations.Conditional
{
    public class RegularExpressionIfAttribute : ConditionalValidationAttribute
    {
        private readonly string pattern;
        protected override string ValidationName
        {
            get { return "regularexpressionif"; }
        }

        public RegularExpressionIfAttribute(string pattern, string dependentProperty, object targetValue)
            : base(new RegularExpressionAttribute(pattern), dependentProperty, targetValue)
        {
            this.pattern = pattern;
        }

        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            // Set the rule RegEx and the rule param pattern
            return new Dictionary<string, object>()
            {
                {"rule", "regex"},
                { "ruleparam", pattern }
            };
        }
    }
}
