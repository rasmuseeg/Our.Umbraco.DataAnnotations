using Our.Umbraco.DataAnnotations.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    /// <summary>
    /// Specifies the maximum length of array or string data allowed in a property.
    /// </summary>
    public sealed class UmbracoMaxLengthAttribute : MaxLengthAttribute, IClientValidatable, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; } = "MaxLengthError";

        public UmbracoMaxLengthAttribute(int length)
            : base(length)
        {
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoDictionary.DictionaryValue(DictionaryKey);

            yield return
                new ModelClientValidationMaxLengthRule(FormatErrorMessage(metadata.GetDisplayName()), Length);
        }
    }
}
