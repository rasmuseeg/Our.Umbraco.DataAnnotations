using Our.Umbraco.DataAnnotations.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    /// <summary>
    /// Specified that two properties data field value must match.
    /// </summary>
    public sealed class UmbracoCompareAttribute : System.ComponentModel.DataAnnotations.CompareAttribute, IClientValidatable, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; } = "EqualToError";
        public new string ErrorMessageString { get; set; }
        public new string OtherPropertyDisplayName { get; set; }

        public UmbracoCompareAttribute(string otherProperty)
            : base(otherProperty)
        {
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessageString = UmbracoDictionary.GetDictionaryValue(DictionaryKey);

            if (metadata.ContainerType != null)
            {
                if (OtherPropertyDisplayName == null)
                {
                    var otherPropertyMetadata = ModelMetadataProviders.Current.GetMetadataForProperty(() => metadata.Model, metadata.ContainerType, OtherProperty);
                    OtherPropertyDisplayName = otherPropertyMetadata.GetDisplayName();
                }
            }

            yield return new ModelClientValidationEqualToRule(FormatErrorMessage(metadata.GetDisplayName()), OtherProperty);
        }
    }
}
