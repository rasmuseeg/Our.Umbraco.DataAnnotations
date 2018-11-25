using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    public class UmbracoRangeAttribute : RangeAttribute, IClientValidatable
    {
        public string ResourceKey { get; set; } = "RangeError";

        public UmbracoRangeAttribute(int minimum, int maximum) 
            : base(minimum, maximum)
        {
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue(ResourceKey);
            yield return
                new ModelClientValidationRangeRule(FormatErrorMessage(metadata.GetDisplayName()), Minimum, Maximum);
        }
    }
}
