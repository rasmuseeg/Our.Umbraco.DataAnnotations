using Our.Umbraco.DataAnnotations.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;

namespace Our.Umbraco.DataAnnotations
{
    /// <summary>
    /// Validates the string data using default membership provider.
    /// </summary>
    /// https://referencesource.microsoft.com/#System.Web/Security/MembershipPasswordAttribute.cs,19c2a804c4a5eaf5,references
    public sealed class UmbracoPasswordAttribute : MembershipPasswordAttribute, IClientValidatable, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; } = "PasswordError";
        public string MinPasswordLengthDictionaryKey { get; set; } = "MinPasswordLengthError";
        public string MinNonAlphanumericCharactersDictionaryKey { get; set; } = "MinPasswordLengthError";
        public string PasswordStrengthDictionaryKey { get; set; } = "MinPasswordLengthError";
        public int? PasswordStrengthRegexTimeout { get; set; }
        public string ValidationName = "password";

        public UmbracoPasswordAttribute()
            : base()
        {
            ErrorMessage = UmbracoDictionary.DictionaryValue(DictionaryKey);
            MinPasswordLengthError = UmbracoDictionary.DictionaryValue(MinPasswordLengthDictionaryKey);
            MinNonAlphanumericCharactersError = UmbracoDictionary.DictionaryValue(MinNonAlphanumericCharactersDictionaryKey);
            PasswordStrengthError = UmbracoDictionary.DictionaryValue(PasswordStrengthDictionaryKey);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(ErrorMessage, metadata.GetDisplayName()),
                ValidationType = ValidationName,
            };
            if (MinRequiredPasswordLength > 0)
            {
                rule.ValidationParameters.Add("min", MinRequiredPasswordLength);
            }
            if (MinRequiredNonAlphanumericCharacters > 0)
            {
                rule.ValidationParameters.Add("nonalphamin", MinRequiredNonAlphanumericCharacters);
            }
            if (PasswordStrengthRegularExpression != null)
            {
                rule.ValidationParameters.Add("regex", PasswordStrengthRegularExpression);
            }

            yield return rule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string valueAsString = value as string;

            string name = (validationContext != null) ? validationContext.DisplayName : String.Empty;
            string[] memberNames = (validationContext != null) ? new[] { validationContext.MemberName } : null;
            string errorMessage;

            if (String.IsNullOrEmpty(valueAsString))
            {
                return ValidationResult.Success;
            }

            if (valueAsString.Length < MinRequiredPasswordLength)
            {
                errorMessage = MinPasswordLengthError;
                return new ValidationResult(FormatErrorMessage(errorMessage, name, MinRequiredPasswordLength), memberNames);
            }

            int nonAlphanumericCharacters = valueAsString.Count(c => !Char.IsLetterOrDigit(c));
            if (nonAlphanumericCharacters < MinRequiredNonAlphanumericCharacters)
            {
                errorMessage = MinNonAlphanumericCharactersError;
                return new ValidationResult(FormatErrorMessage(errorMessage, name, MinRequiredNonAlphanumericCharacters), memberNames);
            }

            string passwordStrengthRegularExpression = PasswordStrengthRegularExpression;
            if (passwordStrengthRegularExpression != null)
            {

                Regex passwordStrengthRegex;
                try
                {
                    // Adding timeout for Regex in case of malicious string causing DoS
                    passwordStrengthRegex = CreateRegex(passwordStrengthRegularExpression, RegexOptions.None, PasswordStrengthRegexTimeout);
                }
                catch (ArgumentException ex)
                {
                    throw new InvalidOperationException("", ex);
                }

                if (!passwordStrengthRegex.IsMatch(valueAsString))
                {
                    errorMessage = PasswordStrengthError;
                    return new ValidationResult(FormatErrorMessage(errorMessage, name, additionalArgument: String.Empty), memberNames);
                }
            }

            return ValidationResult.Success;
        }

        private string FormatErrorMessage(string errorMessageString, string name, object additionalArgument)
        {
            return string.Format(CultureInfo.CurrentCulture, errorMessageString, name, additionalArgument);
        }

        private string FormatErrorMessage(string errorMessageString, string name)
        {
            return string.Format(CultureInfo.CurrentCulture, errorMessageString, name, MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters, PasswordStrengthRegularExpression);
        }

        public Regex CreateRegex(string pattern, RegexOptions option, int? timeoutInMillsec)
        {
            int timeout = 100;

            if (timeout > 0 || timeoutInMillsec.HasValue)
            {
                return new Regex(pattern, option, TimeSpan.FromMilliseconds(timeout));
            }
            else
            {
                return new Regex(pattern, option);
            }
        }
    }
}
