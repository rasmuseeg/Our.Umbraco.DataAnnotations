using Umbraco.Web;

namespace Our.Umbraco.DataAnnotations
{
    public sealed class UmbracoDictionary
    {
        private static UmbracoHelper _helper;

        private static UmbracoHelper Helper
        {
            get
            {
                if (_helper == null)
                {
                    _helper = new UmbracoHelper();
                }
                return _helper;
            }
        }

        public static string GetDictionaryValue(string dictionaryKey)
        {
            string key = Helper.GetDictionaryValue(dictionaryKey);
            if (!string.IsNullOrEmpty(key))
            {
                return key;
            }

            return string.Format(Config.DictionaryFallbackFormat, dictionaryKey); // Fallback with the key name
        }
    }
}
