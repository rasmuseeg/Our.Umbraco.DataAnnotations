using Our.Umbraco.DataAnnotations.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    /// <summary>
    /// Specifies the minimum and maximum length of charactors that are allowed in a data field. 
    /// </summary>
    public sealed class UmbracoStringLengthAttribute : StringLengthAttribute, IClientValidatable, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; } = "MinMaxLengthError";

        public UmbracoStringLengthAttribute(int maximumLength)
            : base(maximumLength)
        {
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoDictionary.DictionaryValue(DictionaryKey);

            yield return
                new ModelClientValidationStringLengthRule(FormatErrorMessage(metadata.GetDisplayName()), MinimumLength, MaximumLength);
        }
    }
}
