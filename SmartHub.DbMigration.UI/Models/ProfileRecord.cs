using System;

namespace SmartHub.DbMigration.UI.Models
{
    [Serializable]
    internal class ProfileRecord
    {
        public ProfileRecord()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }

        public ConnectionRecord Connection { get; set; }

        public DateTime UpdatedOn { get; set; }

        public bool IsDefault { get; set; }
    }
}
