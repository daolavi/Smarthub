using FluentMigrator;

namespace SmartHub.DbMigration
{
    internal sealed class SmartHubMigrationAttribute : MigrationAttribute
    {
        public SmartHubMigrationAttribute(int phase, int sprint, int migration, string dev, string desc)
            : base(SmartHubDbVersionHelper.GenerateVersionNumber(phase, sprint, migration), $"[{dev}] {desc}")
        { }
    }
}
