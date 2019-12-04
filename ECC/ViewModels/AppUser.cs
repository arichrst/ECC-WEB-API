using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECC
{
    public class AppUser
    {
        public class Response_GetUserByToken
        {
            public string cServerMessage { get; set; }
            public long UserNId { get; set; }
            public string UserSessionGUID { get; set; }
            public bool IsLoggedOut { get; set; }
            public DateTime LoginDate { get; set; }
            public DateTime LogoutDate { get; set; }
            public string LoginDateFormatted { get { return LoginDate.TimeAgo(DateTime.Now); } }
            public string LogoutDateFormatted { get { return LogoutDate.TimeAgo(DateTime.Now); } }
        }

        public class Response_Login{
            [JsonIgnore]
            public string cUserToken { get; set; }
            [JsonIgnore]
            public string cUserType { get; set; }
            [JsonIgnore]
            public int bLoginSucceed { get; set; }
            [JsonIgnore]
            public string cServerMessage { get; set; }
            [JsonIgnore]
            public short nMemberNId { get; set; }
            [JsonIgnore]
            public short nUserNId { get; set; }
           
            public string Token { get { return cUserToken; } }
            public string Type { get { return cUserType; } }
            public bool Success { get { return bLoginSucceed == 1; } }
            public string Message { get { return cServerMessage; } }
            public short MemberId { get { return nMemberNId; } }
            public short UserId { get { return nUserNId; } }

            public IList<ResultHome> Home { get; set; }

            public class ResultMenu {
                public long MenuNId { get; set; }
                public string MenuId { get; set; }
                public long ParentMenuNId { get; set; }
                public string MenuKey { get; set; }
                public string MenuDesc { get; set; }
                public string MenuSlug { get; set; }
            }

            public class ResultHome {
                public long HomeNId { get; set; }
                public string HomeId { get; set; }
                public string HomeDesc { get; set; }
            }
        }

        public class Response_Register
        {
            public string cUserToken { get; set; }
            public string cServerMessage { get; set; }
        }

        public class Request_Login{
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Request_Register
        {
            public string UserId { get; set; }
            public string Password { get; set; }
            public string FullName { get; set; }
            public string PhoneNumber { get; set; }
            public string MailAddress { get; set; }
        }

        public class Request_Profile
        {
            public string Token { get; set; }
        }
    }
}