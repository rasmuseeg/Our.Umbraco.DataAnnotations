using Our.Umbraco.DataAnnotations.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    /// <summary>
    /// Specifies that a data field value is required.
    /// </summary>
    public sealed class UmbracoRequiredAttribute : RequiredAttribute, IClientValidatable, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; } = "RequiredError";

        public UmbracoRequiredAttribute() 
            : base()
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue(DictionaryKey);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRequiredRule(FormatErrorMessage(metadata.GetDisplayName()));
        }
    }
}
