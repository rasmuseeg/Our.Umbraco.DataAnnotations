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
        public string ResourceName { get; set; } = "RequiredError";

        public UmbracoRequiredAttribute() :
            base()
        {
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue(ResourceName);
            yield return new ModelClientValidationRequiredRule(FormatErrorMessage(metadata.GetDisplayName()));
        }
    }
}
