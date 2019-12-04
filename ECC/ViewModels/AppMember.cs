using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECC
{
    public class AppMember
    {
        public class Request{
            public string Name { get; set; }
            public int ChruchEventId{get;set;}
        }
        public class Response{
            [JsonIgnore]
            public long MemberNId { get; set; }
            [JsonIgnore]
            public string GivenName { get; set; }
            [JsonIgnore]
            public string MobilePhoneNumber { get; set; }
            [JsonIgnore]
            public bool IsInvited { get; set; }
           
            [JsonIgnore]
            public DateTime InvitedDate { get; set; }
            [JsonIgnore]
            public DateTime BirthDate { get; set; }
            [JsonIgnore]
            public string BirthDateFormatted{ get { return BirthDate.To_ddMMMyyyy(); } }

            public long Id { get { return MemberNId; } }
            public string Name { get { return GivenName; } }
            public string Birth { get { return BirthDateFormatted; } }
            public string PhoneNumber { get { return MobilePhoneNumber; } }
            public bool Invited { get ; set; }
            public string InvitedDateFormatted { get { return InvitedDate.To_ddMMMyyyy(); } }
            public string InvitedBy { get; set; }
        }

        
        public class Response_MemberDetail
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Sex { get; set; }
            public string GivenName { get; set; }
            public string Nickname { get; set; }
            [JsonIgnore]
            public DateTime? BirthDate { get; set; }
            public string MaritalStatus { get; set; }
            [JsonIgnore]
            public DateTime? Anniversary { get; set; }
            public string MedicalNotes { get; set; }
            public string EducationLevel { get; set; }
            public bool? HasChild { get; set; }
            public string MemberBarcode { get; set; }
            public string NamePrefix { get; set; }
            public string NameSuffix { get; set; }
            public string eMailAddress { get; set; }
            public string HomePhoneNumber { get; set; }
            public string MobilePhoneNumber { get; set; }
            public string HomeAddress1 { get; set; }
            public string HomeAddress2 { get; set; }
            public string HomePostalCode { get; set; }
            public string Kecamatan { get; set; }
            public string Kelurahan { get; set; }
            public string HomeCity { get; set; }
            public string HomeProvince { get; set; }
            public byte SJStatusNId { get; set; }
            public IList<ResultHome> Home { get; set; }
            public class ResultHome
            {
                public long HomeNId { get; set; }
                public string HomeId { get; set; }
                public string HomeDesc { get; set; }
            }
        }
    }
}