using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECC
{
    public class Event
    {
        public class Response_InvitationUrl
        {
            public string InvitationUrl { get; set; }
        }
        public class Response_TicketAvailability
        {
            public string ChurchEventTItle { get; set; }
            public int MaxParticipant { get; set; }
            public int TakenTicketByNewComer { get; set; }
            public int TakenTicketByMember { get; set; }
            public int AvailableTicket { get; set; }
            public int TicketScanned { get; set; }
        }
        public class Response_ChurchEventAbsence{
            public int ChurchEventNId { get; set; }
            public string ChurchEventTitle { get; set; }
            public string EventDesc { get; set; }
            public string LocDesc { get; set; }
            public double? LocLatitude { get; set; }
            public double? LocLongitude { get; set; }
            public DateTime? EventDate { get; set; }
            public TimeSpan? EventTime { get; set; }
            public DateTime? EventDate2 { get; set; }
            public TimeSpan? EventTime2 { get; set; }
            public bool Deleted { get; set; }
            public short? CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Url1 { get; set; }
            public string Url2 { get; set; }
            public bool MustRegistered { get; set; }
            public bool PaidEvent { get; set; }
            public decimal EarlyBirdPrice { get; set; }
            public DateTime EarlyBirdDate { get; set; }
            public decimal NormalPrice { get; set; }
            public DateTime RegistrationDateOpen { get; set; }
            public DateTime RegistrationDateClose { get; set; }
            public byte RegistrationType { get; set; }
            public short MemberNId { get; set; }
            public DateTime? RegDate { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Sex { get; set; }
            public string GivenName { get; set; }
            public string Nickname { get; set; }
            public DateTime? BirthDate { get; set; }
            public string MaritalStatus { get; set; }
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
            public string PID { get; set; }
            public string BirthPlace { get; set; }
            public byte? MemberStatus { get; set; }
        }
        public class Response_ChurchEventParticipants{
            public string GivenName { get; set; }
            [JsonIgnore]
            public string InvitationToken { get; set; }
            public string Token { get{ return InvitationToken;} }
            public long MemberNId { get; set; }
            public string Sex { get; set; }
            public DateTime BirthDate { get; set; }
            public string HomeAddress { get; set; }
            public string MobilePhoneNumber { get; set; }
            public bool IsRegistered { get; set; }
            public bool IsComing { get; set; }
            public DateTime LastUpdatedDate { get; set; }
            public long UpdatedBy { get; set; }
            public bool IsMember { get; set; }
            public int RecordNId{get;set;}
            public string CreatedDateFormatted{ get { return BirthDate.TimeAgo(DateTime.Now); } }
            public string LastUpdatedDateFormatted{ get { return LastUpdatedDate.TimeAgo(DateTime.Now); } }
        }    
        public class Request_RegisterEventForLeaders{
            public long ChurchEventNId { get; set; }
            public long MemberNId { get; set; }
        }

        public class Request_CancelRegisterNonMember{
            public long ChurchEventNId { get; set; }
            public long RecordNId { get; set; }
        }
        public class Request_RegisterEventForNewComers{
            public int ChurchEventNId { get; set; }
            public string FullName { get; set; }
            public string BirthDate { get; set; }
            public string Sex { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Occupation { get; set; }
            public string Status { get; set; }
            public string OfficeAddress { get; set; }
        }
        public class Request_ChurchEventParticipants{
            public int ChurchEventId { get; set; }
        }
        public class Request_ChurchEventAbsence{
            public int UserId { get; set; }
            public string ChurchEventId { get; set; }
            public string QrCode { get; set; }
        }

        public class Request_TicketAvailability
        {
            public string UserId { get; set; }
            public string ChurchEventId { get; set; }
        }
        public class Request_EventDetail {
            public int ChurchEventId { get; set; }
        }
        public class Response_MyEvent {
            public int ChurchEventNId { get; set; }
            public string ChurchEventTitle { get; set; }
            public string EventDesc { get; set; }
            public DateTime EventDate { get; set; }
            public TimeSpan EventTime { get; set; }
            public string Token { get; set; }
            public bool IsComing { get; set; }
            public int IsPresent { get; set; }
            [JsonIgnore]
            public string Url1 { get; set; }
            public string ImageUrl { get { return Url1; } }
            public string EventDateFormatted { get { return EventDate.To_ddMMMyyyy(); } }
        }
        public class Response_EventDetail
        {
            public int ChurchEventNId { get; set; }
            [JsonIgnore]
            public string ChurchEventTitle { get; set; }
            [JsonIgnore]
            public string EventDesc { get; set; }
            public string LocDesc { get; set; }
            [JsonIgnore]
            public string Url1 { get; set; }
            [JsonIgnore]
            public DateTime EventDate { get; set; }
            [JsonIgnore]
            public TimeSpan EventTime { get; set; }
            public bool IsSpecialEvent { get; set; }
            public bool IsPresent { get; set; }
            public bool IsEligibleToInvite { get; set; }
            public bool IsRegistered { get; set; }
            public int Id { get { return ChurchEventNId; } }
            public string Date { get { return EventDate.ToString("dd MMM yyyy"); } }
            public string ImageUrl { get{ return Url1; } }
            public string Time { get{ return EventTime.ToString("hh\\:mm");} }
            public string Title { get { return ChurchEventTitle; } }
            public string Description { get { return EventDesc; } }
        }

        public class Response_NewComersParticipant
        {
            public int RecordNId { get; set; }
            public short? ChurchEventNId { get; set; }
            public string FullName { get; set; }
            public DateTime? BirthDate { get; set; }
            public string Sex { get; set; }
            public string MobilePhoneNumber { get; set; }
            public string eMailAddress { get; set; }
            public string HomeAddress { get; set; }
            public string Occupation { get; set; }
            public string OfficeAddress { get; set; }
            public string NewComerStatus { get; set; }
            public short? InputUserNId { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string InputBatch { get; set; }
            public bool Deleted { get; set; }
        }
    }
}