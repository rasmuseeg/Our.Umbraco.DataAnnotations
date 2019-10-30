using Our.Umbraco.DataAnnotations;

namespace UmbracoCms.Models
{
    public sealed class ContactFormModel
    {
        [UmbracoDisplayName(nameof(FirstName))]
        [UmbracoRequired]
        public string FirstName { get; set; }

        [UmbracoDisplayName(nameof(LastName))]
        [UmbracoRequired]
        public string LastName { get; set; }

        [UmbracoDisplayName(nameof(FullName))]
        public string FullName { get { return string.Concat(FirstName, " ", LastName); } }

        [UmbracoDisplayName(nameof(EmailAddress))]
        [UmbracoEmailAddress]
        [UmbracoRequired(DictionaryKey = "")]
        public string EmailAddress { get; set; }

        [UmbracoDisplayName(nameof(Subject))]
        [UmbracoStringLength(maximumLength: 120, MinimumLength = 10, DictionaryKey = "")]
        [UmbracoRequired]
        public string Subject { get; set; }

        [UmbracoDisplayName(nameof(Message))]
        [UmbracoStringLength(maximumLength: 260, MinimumLength = 30, DictionaryKey = "")]
        [UmbracoRegularExpression("[a-zA-Z0-9]", DictionaryKey = "")]
        [UmbracoRequired]
        public string Message { get; set; }
    }
}
