using System;

namespace SmartHub.DbMigration.UI.Models
{
    [Serializable]
    internal class ConnectionRecord
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string AdoConnectionString
        {
            get { return $"Data Source={Server};Initial Catalog={Database};User ID={User};Password={Password}"; }
        }
    }
}
