using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    /// <summary>
    /// Specifies that a data field value in ASP.net Dynamic Data must match the specified regular expression.
    /// </summary>
    public class UmbracoRegularExpressionAttribute : RegularExpressionAttribute, IClientValidatable
    {

        public UmbracoRegularExpressionAttribute(string pattern, string dictionaryKey)
            : base(pattern)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue(dictionaryKey);

        }

        public UmbracoRegularExpressionAttribute(string pattern)
            : base(pattern)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue("RegexError");
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRegexRule(FormatErrorMessage(metadata.GetDisplayName()), Pattern);
        }
    }
}
