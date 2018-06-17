namespace SmartHub.DbMigration
{
    public static class SmartHubDbVersionHelper
    {
        public static long GenerateVersionNumber(int phase, int sprint, int migration)
        {
            return (phase * 1000000) + (sprint * 1000) + migration;
        }
    }
}
