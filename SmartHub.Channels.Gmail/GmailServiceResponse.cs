using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Channels.Gmail
{
    public class GmailServiceResponse<T>
    {
        public T Result { get; set; }

        public string Error { get; set; }

        public bool HasError => !string.IsNullOrEmpty(Error);
    }
}
