using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    public class UmbracoRangeAttribute : RangeAttribute, IClientValidatable
    {
        public UmbracoRangeAttribute(int minimum, int maximum) 
            : base(minimum, maximum)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue("RangeError");
        }

        public UmbracoRangeAttribute(int minimum, int maximum, string dictionaryKey)
            : base(minimum, maximum)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue(dictionaryKey);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return
                new ModelClientValidationRangeRule(FormatErrorMessage(metadata.GetDisplayName()), Minimum, Maximum);
        }
    }
}
