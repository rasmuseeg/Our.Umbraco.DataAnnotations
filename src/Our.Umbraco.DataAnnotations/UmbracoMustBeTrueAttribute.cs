using Our.Umbraco.DataAnnotations.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class UmbracoMustBeTrueAttribute : ValidationAttribute, IClientValidatable, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; } = "MustBeTrueError";

        public UmbracoMustBeTrueAttribute()
        {
            ErrorMessage = UmbracoDictionary.GetDictionaryValue(DictionaryKey);
        }

        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool)value;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ValidationType = "mustbetrue",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
            };

            yield return rule;
        }
    }
}
