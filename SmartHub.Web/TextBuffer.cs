using System;
using System.Text;

namespace SmartHub.Web
{
    public static class TextBuffer
    {
        private static readonly StringBuilder Buffer = new StringBuilder();

        public static void WriteLine(string value)
        {
            lock (Buffer)
            {
                Buffer.AppendLine(String.Format("{0} {1}", DateTime.UtcNow, value));
            }
        }

        public new static string ToString()
        {
            lock (Buffer)
            {
                return Buffer.ToString();
            }
        }
    }
}