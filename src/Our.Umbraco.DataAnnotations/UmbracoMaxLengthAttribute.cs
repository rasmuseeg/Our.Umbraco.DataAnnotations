using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    /// <summary>
    /// Specifies the maximum length of array or string data allowed in a property.
    /// </summary>
    public class UmbracoMaxLengthAttribute : MaxLengthAttribute, IClientValidatable
    {
        public UmbracoMaxLengthAttribute(int length)
            : base(length)
        {
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue("MaxLengthError");
            yield return
                new ModelClientValidationMaxLengthRule(FormatErrorMessage(metadata.GetDisplayName()), Length);
        }

        public UmbracoMaxLengthAttribute(int length, string dictionaryKey)
            : base(length)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue(dictionaryKey);
        }
    }
}
