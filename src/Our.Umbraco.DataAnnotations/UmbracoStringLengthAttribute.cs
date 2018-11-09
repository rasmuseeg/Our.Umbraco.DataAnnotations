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
    /// Specifies the minimum and maximum length of charactors that are allowed in a data field. 
    /// </summary>
    public class UmbracoStringLengthAttribute : StringLengthAttribute, IClientValidatable
    {
        public UmbracoStringLengthAttribute(int maximumLength)
            : base(maximumLength)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue("MinMaxLengthError");
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {

            yield return
                new ModelClientValidationStringLengthRule(FormatErrorMessage(metadata.GetDisplayName()), MinimumLength, MaximumLength);
        }

        public UmbracoStringLengthAttribute(int maximumLength, string dictionaryKey)
            : base(maximumLength)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue(dictionaryKey);
        }
    }
}
