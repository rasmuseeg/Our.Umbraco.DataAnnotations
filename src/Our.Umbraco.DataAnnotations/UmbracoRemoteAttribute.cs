using Our.Umbraco.DataAnnotations.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    public sealed class UmbracoRemoteAttribute : RemoteAttribute, IClientValidatable, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; }

        public UmbracoRemoteAttribute(string routeName)
            : base(routeName)
        {
        }

        public UmbracoRemoteAttribute(string action, string controller)
            : base(action, controller)
        {

        }

        public UmbracoRemoteAttribute(string action, string controller, string areaName)
            : base(action, controller, areaName)
        {
        }


        public new IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoDictionary.DictionaryValue(DictionaryKey);

            yield return new ModelClientValidationRemoteRule(FormatErrorMessage(metadata.GetDisplayName()), this.GetUrl(context), this.HttpMethod, this.AdditionalFields);
        }

    }
}