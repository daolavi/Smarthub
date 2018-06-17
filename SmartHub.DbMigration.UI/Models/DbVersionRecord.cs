using System;

namespace SmartHub.DbMigration.UI.Models
{
    internal class DbVersionRecord
    {
        public long VersionNumber { get; set; }

        public string Description { get; set; }

        public DateTime? AppliedOn { get; set; }
    }
}
