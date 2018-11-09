using System.Collections.Generic;
using System.Web.Mvc;

namespace Our.Umbraco.DataAnnotations
{
    /// <summary>
    /// Specified that two properties data field value must match.
    /// </summary>
    public class UmbracoCompareAttribute : System.ComponentModel.DataAnnotations.CompareAttribute, IClientValidatable
    {
        public new string ErrorMessageString { get; internal set; }
        public new string OtherPropertyDisplayName { get; internal set; }

        public UmbracoCompareAttribute(string otherProperty)
            : base(otherProperty)
        {
            ErrorMessageString = UmbracoDictionary.GetDictionaryValue("EqualToError");
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            if (metadata.ContainerType != null)
            {
                if (OtherPropertyDisplayName == null)
                {
                    var otherPropertyMetadata = ModelMetadataProviders.Current.GetMetadataForProperty(() => metadata.Model, metadata.ContainerType, OtherProperty);
                    OtherPropertyDisplayName = otherPropertyMetadata.GetDisplayName();
                }
            }

            //string otherPropertyId = BuildDependentPropertyId(metadata, context as ViewContext); // This is handled client-side

            yield return new ModelClientValidationEqualToRule(FormatErrorMessage(metadata.GetDisplayName()), OtherProperty);
        }

        //private string BuildDependentPropertyId(ModelMetadata metadata, ViewContext viewContext)
        //{
        //    string depProp = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(this.OtherProperty);
        //    // This will have the name of the current field appended to the beginning, because the TemplateInfo's context has had this fieldname appended to it.
        //    var thisField = metadata.PropertyName + "_";
        //    if (depProp.StartsWith(thisField))
        //    {
        //        depProp = depProp.Substring(thisField.Length);
        //    }
        //    return depProp;
        //}
    }
}
