using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECC
{
    public class FeedType
    {
        public class Response{
            [JsonIgnore]
            public string FeedTypeId { get; set; }
            [JsonIgnore]
            public string FeedTypeDesc { get; set; }
            public string Key { get { return FeedTypeId; } }
            public string Title { get { return FeedTypeDesc; } }
           
        }
    }
}