using System.ComponentModel;

namespace Our.Umbraco.DataAnnotations
{
    public class UmbracoDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string dictionaryKey;

        public UmbracoDisplayNameAttribute(string dictionaryKey)
            : base()
        {
            this.dictionaryKey = dictionaryKey;
        }

        public override string DisplayName
        {
            get
            {
                return UmbracoDictionary.GetDictionaryValue(dictionaryKey);
            }
        }
    }
}
