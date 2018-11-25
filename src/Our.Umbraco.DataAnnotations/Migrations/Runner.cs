using Semver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;

namespace Our.Umbraco.DataAnnotations.Migrations
{
    public class Runner : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            HandleMigrations(applicationContext);
        }

        private static void HandleMigrations(ApplicationContext context)
        {
            const string productName = Constants.PluginName;
            try
            {
                var currentVersion = new SemVersion(0, 0, 0);

                // get all migrations already executed
                var migrations = context.Services.MigrationEntryService.GetAll(productName);

                // get the latest migration for "UDF" executed
                var latestMigration = migrations.OrderByDescending(x => x.Version).FirstOrDefault();

                if (latestMigration != null)
                    currentVersion = latestMigration.Version;

                var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
                var targetVersion = new SemVersion(assemblyVersion);
                if (targetVersion == currentVersion)
                    return;

                var migrationsRunner = new MigrationRunner(
                  context.Services.MigrationEntryService,
                  context.ProfilingLogger.Logger,
                  currentVersion,
                  targetVersion,
                  productName);

                migrationsRunner.Execute(context.DatabaseContext.Database);
            }
            catch (Exception e)
            {
                LogHelper.Error<Runner>("Error running " + productName + " migrations", e);
            }
        }
    }
}
