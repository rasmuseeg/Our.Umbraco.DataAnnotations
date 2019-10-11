﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    /// <summary>
    /// Specifies the minimum length of array or string data allowed in a property.
    /// </summary>
    public class UmbracoMinLengthAttribute : MinLengthAttribute, IClientValidatable
    {
        public UmbracoMinLengthAttribute(int length)
            : base(length)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue("MinLengthError");
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return
                new ModelClientValidationMinLengthRule(FormatErrorMessage(metadata.GetDisplayName()), Length);
        }

        public UmbracoMinLengthAttribute(int length, string dictionaryKey)
            : base(length)
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue(dictionaryKey);
        }
    }
}
