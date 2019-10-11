using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    /// <summary>
    /// Specifies that a data field value is required.
    /// </summary>
    public class UmbracoRequiredAttribute : RequiredAttribute, IClientValidatable
    {

        public UmbracoRequiredAttribute(string dictionaryKey) :
            base()
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue(dictionaryKey);

        }

        public UmbracoRequiredAttribute() :
            base()
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue("RequiredError");

        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRequiredRule(FormatErrorMessage(metadata.GetDisplayName()));
        }
    }
}
