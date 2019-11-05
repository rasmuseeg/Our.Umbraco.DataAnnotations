using Our.Umbraco.DataAnnotations.Interfaces;
using System.ComponentModel;

namespace Our.Umbraco.DataAnnotations
{
    public sealed class UmbracoDisplayNameAttribute : DisplayNameAttribute, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; }

        public UmbracoDisplayNameAttribute(string dictionaryKey)
            : base()
        {
            DictionaryKey = dictionaryKey;
        }

        public override string DisplayName
        {
            get
            {
                return UmbracoDictionary.DictionaryValue(DictionaryKey);
            }
        }
    }
}
