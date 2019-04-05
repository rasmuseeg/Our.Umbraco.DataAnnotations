using System.Collections.Generic;
using Umbraco.Core;
using System.Linq;
using Umbraco.Core.Services;
using Umbraco.Core.Models;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Our.Umbraco.DataAnnotations
{
    [Migration("1.2.0", 1, Constants.PluginName)]
    public class CreateNotesTable : MigrationBase
    {
        private readonly UmbracoDatabase _database = ApplicationContext.Current.DatabaseContext.Database;
        private readonly DatabaseSchemaHelper _schemaHelper;

        public CreateNotesTable(ISqlSyntaxProvider sqlSyntax, ILogger logger)
            : base(sqlSyntax, logger)
        {
            _schemaHelper = new DatabaseSchemaHelper(_database, logger, sqlSyntax);
        }

        private readonly Dictionary<string, string> defaultDictionaryItems = new Dictionary<string, string>() {
            { "RequiredError", "{0} is required." },
            { "EqualToError", "The '{0}' and {1} fields must match." },
            { "EmailError", "Doesn't look like a valid e-mail." },
            { "MaxLengthError", "Must not exceed {1} characters." },
            { "MinLengthError", "Must not be less than {1} characters." },
            { "MinMaxLengthError", "Must not be less than {1} and greather than {1} characters." },
            { "MustBeTrueError", "{0} is required." },
            { "RangeError", "Please enter a number between {1} and {2}." },
            { "PasswordError", "Must be at least {1} characters long and contain {2} symbol (!, @, #, etc)." },
            { "MinPasswordLengthError", "Must be at least {1} characters." },
            { "MinNonAlphanumericCharactersError", "Must contain at leat {2} symbol (!, @, #, etc)." },
            { "PasswordStrengthError", "Must be at least {1} characters long and contain {2} symbol (!, @, #, etc)." },
        };

        public override void Up()
        {
            ILocalizationService localizationService = ApplicationContext.Current.Services.LocalizationService;
            IEnumerable<ILanguage> languages = localizationService.GetAllLanguages();
            ILanguage language = languages.FirstOrDefault(x=> x.IsoCode == "en-GB" || x.IsoCode == "en-US")
                ?? languages.FirstOrDefault();

            if (language == null)
            {
                return;
            }

            IDictionaryItem dataAnnotations = localizationService.GetDictionaryItemByKey("DataAnnotions");
            if (dataAnnotations != null)
            {
                return;
            }

            dataAnnotations = localizationService.CreateDictionaryItemWithIdentity("DataAnnotations", null);

            foreach (var item in defaultDictionaryItems)
            {
                if (localizationService.DictionaryItemExists(item.Key))
                    continue;

                localizationService.CreateDictionaryItemWithIdentity(item.Key, dataAnnotations.Key, item.Value);
            }
        }

        public override void Down()
        {
        }
    }
}