using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpLogParser.Models
{
    public class LogItem
    {
        public string IPAddress { get; set; }
        public string URL { get; set; }
        public string Method { get; set; }
        public int StatusCode { get; set; }
        public int Size { get; set; }
        public string UserAgent { get; set; }
        public DateTime DateTime { get; set; }
    }
}
