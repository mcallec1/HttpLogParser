using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpLogParser.Models
{
    public class LogAnalysisResult
    {
        public int UniqueIPCount { get; set; }
        public List<(string Url, int Count)> TopUrls { get; set; }
        public List<(string IP, int Count)> TopIPs { get; set; }
    }
}
