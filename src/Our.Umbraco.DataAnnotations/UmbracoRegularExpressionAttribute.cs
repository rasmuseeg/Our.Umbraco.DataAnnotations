using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    /// <summary>
    /// Specifies that a data field value in ASP.net Dynamic Data must match the specified regular expression.
    /// </summary>
    public class UmbracoRegularExpressionAttribute : RegularExpressionAttribute, IClientValidatable
    {
        public new string ErrorMessageString { get; internal set; }
        public string ResourceName { get; set; } = "RegexError";

        public UmbracoRegularExpressionAttribute(string pattern)
            : base(pattern)
        {
            ErrorMessageString = UmbracoDictionary.GetDictionaryValue(ResourceName);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRegexRule(FormatErrorMessage(metadata.GetDisplayName()), Pattern);
        }
    }
}
