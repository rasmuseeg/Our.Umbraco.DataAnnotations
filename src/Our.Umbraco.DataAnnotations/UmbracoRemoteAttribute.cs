using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Mvc;
using Our.Umbraco.DataAnnotations.Interfaces;

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
            ErrorMessage = UmbracoDictionary.GetDictionaryValue(DictionaryKey);

            yield return new ModelClientValidationRemoteRule(FormatErrorMessage(metadata.GetDisplayName()), this.GetUrl(context), this.HttpMethod, this.AdditionalFields);
        }

    }
}
