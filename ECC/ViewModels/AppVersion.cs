using System;
using Microsoft.AspNetCore.Http;

namespace ECC
{
    public class AppVersion
    {
        public class Response{
            public long AppNId { get; set; }
            public long AppVersion { get; set; }
            public long PatchSequence { get; set; }
            public DateTime VersionDate { get; set; }
            public bool IsInactive { get; set; }
            public string Os { get; set; }
            public string VersionDateFormatter{ get { return VersionDate.To_ddMMMyyyymmhhss(); } }
        }
    }
}