using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECC
{
    public class Feed
    {
        public class Request{
            public string Type { get; set; }
        }
        public class Response{
            [JsonIgnore]
            public long RowNumber { get; set; }
            [JsonIgnore]
            public string VidUrl { get; set; }
            [JsonIgnore]
            public long FeedNId { get; set; }
            [JsonIgnore]
            public string FeedTitle { get; set; }
            [JsonIgnore]
            public string FeedUrl { get; set; }
            [JsonIgnore]
            public string FeedType { get; set; }
            [JsonIgnore]
            public string FeedDesc { get; set; }
            [JsonIgnore]
            public string AdditionalDesc { get; set; }
            [JsonIgnore]
            public long CreatedBy { get; set; }
            [JsonIgnore]
            public long ChurchEventNId { get; set; }
            [JsonIgnore]
            public DateTime CreatedDate { get; set; }
            [JsonIgnore]
            public string DisplayType { get; set; }
            [JsonIgnore]
            public string CreatedDateFormatted{ get { return CreatedDate.TimeAgo(DateTime.Now); } }

            public long Id { get { return FeedNId; } }
            public string ImageUrl { get { return FeedUrl; } }
            public string VideoUrl { get { return VidUrl; } }
            public string Title { get { return FeedTitle; } }
            public string Description { get { return FeedDesc; } }
            public string Speaker { get { return AdditionalDesc; } }
            public string Type { get { return FeedType; } }
            public string Date { get { return CreatedDateFormatted; } }
            public long ChurchEventId { get { return ChurchEventNId; } }
            public bool IsEvent { get; set; }
            public byte IsSpecialEvent { get; set; }
            public bool IsEligibleToInvite { get; set; }
        }
    }
}