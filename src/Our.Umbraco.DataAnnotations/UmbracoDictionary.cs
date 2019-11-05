using Umbraco.Web;
using Umbraco.Web.Composing;

namespace Our.Umbraco.DataAnnotations
{
    public sealed class UmbracoDictionary
    {
        public static string DictionaryValue(string dictionaryKey)
        {
            string key = Current.UmbracoHelper.GetDictionaryValue(dictionaryKey);
            if (!string.IsNullOrEmpty(key))
            {
                return key;
            }

            return string.Format(Config.DictionaryFallbackFormat, dictionaryKey);
        }
    }
}
