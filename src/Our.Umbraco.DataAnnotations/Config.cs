using System;

namespace Our.Umbraco.DataAnnotations
{
    public class Config
    {
        private static string _dictionaryFallbackFormat;
        /// <summary>
        /// Fallback format (Eg. [{0}])
        /// </summary>
        public static string DictionaryFallbackFormat
        {
            get
            {
                if (_dictionaryFallbackFormat == null)
                {
                    string appSettingKey = Constants.AppSettings.Prefix + "." + Constants.AppSettings.DictionaryKeyFallback;
                    _dictionaryFallbackFormat = System.Configuration.ConfigurationManager.AppSettings[appSettingKey] as string
                        ?? "[{0}]";

                    if (!_dictionaryFallbackFormat.Contains("{0}"))
                    {
                        throw new FormatException($"'{_dictionaryFallbackFormat}' is not a valid format for '{appSettingKey}'. Format must contain '{{0}}'.");
                    }
                }
                return _dictionaryFallbackFormat;
            }
        }
    }
}
