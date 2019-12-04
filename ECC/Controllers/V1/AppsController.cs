using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECC.Models;
using Microsoft.AspNetCore.Http;
using ECC.Constants;
using System.IO;

namespace ECC.Controllers
{
    [Route("api/v1/[controller]")]
    public class AppsController : MasterController
    {
        [HttpPost("UploadImage")]
        public string UploadImage(IFormFile file)
        {
            string filename = Guid.NewGuid().ToString();
            var dir = Directory.GetCurrentDirectory();
            if (file.Length > 0)
            {
                try
                {
                    string extension = Path.GetExtension(file.FileName);
                    if (!Directory.Exists(dir + "/"+ "uploads" +"/"))
                    {
                        Directory.CreateDirectory(dir + "/" + "uploads" + "/");
                    }
                    using (FileStream filestream = System.IO.File.Create(dir + "/" + "uploads" + "/" + filename + extension))
                    {
                        file.CopyTo(filestream);
                        filestream.Flush();
                        var baseUrl = HttpContext.Request.Host;

                        return baseUrl + "/" + "uploads" + "/" + filename + extension;
                    }
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }

        }

        [HttpGet("Testing")]
        public async Task<BaseApiResponse<object>> Testing()
        {
            ECCRequest<object> request = new ECCRequest<object>(this, false);
            return await request.ExecuteAsync("Apps/CheckVersion", async () => {
                //TOKEN SAMPLE : 78D51E92-0DA5-4C91-94CF-9FE47DB21C69
                var result = await Db.AppGetUser("14778FBB-4780-4042-B66D-658F2773F42F");
                return await Db.AppVersion();
            });
        }

        [HttpPost("CheckVersion")]
        public async Task<BaseApiResponse<AppVersion.Response>> CheckVersion()
        {
           return await new ECCRequest<AppVersion.Response>(this,false).ExecuteAsync("Apps/CheckVersion", async () => {
                return await Db.AppVersion();
            });
        }

        [HttpPost("AppLogin")]
        public async Task<BaseApiResponse<AppUser.Response_Login>> AppLogin(AppUser.Request_Login param)
        {
            ECCRequest<AppUser.Response_Login> request = new ECCRequest<AppUser.Response_Login>(this, false);
            return await request.ExecuteAsync("Apps/AppLogin", async () => {
                return await Db.AppLogin(param, request.GetAppId());
            },param);
        }

        [HttpPost("GetMemberByName")]
        public async Task<BaseApiResponse<IEnumerable<AppMember.Response>>> GetFeeds(AppMember.Request param)
        {
            return await new ECCRequest<IEnumerable<AppMember.Response>>(this, true).ExecuteAsync("Apps/GetMemberByName", async () => {
                return await Db.GetMemberByName(param);
            }, param);
        }

        [HttpPost("GetMemberProfile")]
        public async Task<BaseApiResponse<AppMember.Response_MemberDetail>> GetMemberProfile(long memberId)
        {
            return await new ECCRequest<AppMember.Response_MemberDetail>(this, false).ExecuteAsync("Apps/GetMemberProfile", async () => {
                return await Db.GetMemberDetail(memberId);
            }, memberId);
        }

        [HttpPost("GetMyMemberProfile")]
        public async Task<BaseApiResponse<AppMember.Response_MemberDetail>> GetMyMemberProfile()
        {
            ECCRequest<AppMember.Response_MemberDetail> request = new ECCRequest<AppMember.Response_MemberDetail>(this, false);
            return await request.ExecuteAsync("Apps/GetMyMemberProfile", async () => {
                var member = await request.GetMember();
                return await Db.GetMemberDetail(member == null? 0 : member.MemberNId);
            });
        }

        //[HttpPost("MyProfile")]
        //public async Task<BaseApiResponse<AppMember.Response_MemberDetail>> MyProfile()
        //{
        //    ECCRequest<AppMember.Response_MemberDetail> request = new ECCRequest<AppMember.Response_MemberDetail>(this, false);
        //    return await request.ExecuteAsync("Apps/GetMemberProfile", async () => {
        //        var member = await Db.AppGetMember(request.GetToken());
        //        return await Db.GetMemberDetail(member == null? 0 : 1);
        //    });
        //}

        [HttpPost("GetFeeds")]
        public async Task<BaseApiResponse<IEnumerable<Feed.Response>>> GetFeeds(Feed.Request param)
        {
            ECCRequest<IEnumerable<Feed.Response>> request = new ECCRequest<IEnumerable<Feed.Response>>(this, false);
            return await request.ExecuteAsync("Apps/GetFeeds", async () => {
                var user = await request.GetUser();
                return await Db.GetFeeds(param,user == null? 0 : user.UserNId);
            }, param);
        }

        [HttpPost("GetFeedsType")]
        public async Task<BaseApiResponse<IEnumerable<FeedType.Response>>> GetFeedsType()
        {
            return await new ECCRequest<IEnumerable<FeedType.Response>>(this, false).ExecuteAsync("Apps/GetFeedsType", async () => {
                return await Db.GetFeedsType();
            });
        }

        [HttpPost("GetFeedsDetail")]
        public async Task<BaseApiResponse<Feed.Response>> GetFeedsDetail(long feedId)
        {
            return await new ECCRequest<Feed.Response>(this, false).ExecuteAsync("Apps/GetFeedsDetail", async () => {
                return await Db.GetFeedsDetail(feedId);
            },feedId);
        }

        [HttpPost("GetEventDetail")]
        public async Task<BaseApiResponse<Event.Response_EventDetail>> GetEventDetail(Event.Request_EventDetail param)
        {
            ECCRequest<Event.Response_EventDetail> request = new ECCRequest<Event.Response_EventDetail>(this, false);
            return await request.ExecuteAsync("Apps/GetEventDetail", async () => {
                var member = await request.GetMember();
                return await Db.GetEventDetail(param, member == null ? 0 : member.Id);
            }, param);
        }

        [HttpPost("GetMyEvents")]
        public async Task<BaseApiResponse<IEnumerable<Event.Response_MyEvent>>> GetMyEvents()
        {
            ECCRequest<IEnumerable<Event.Response_MyEvent>> request = new ECCRequest<IEnumerable<Event.Response_MyEvent>>(this, false);
            return await request.ExecuteAsync("Apps/GetMyEvents", async () => {
                var user = await request.GetUser();
                return await Db.GetMyEvent( user == null ? 0 : user.UserNId);
            });
        }

        [HttpPost("RegisterMemberSpecialEvents")]
        public async Task<BaseApiResponse<string>> RegisterMemberSpecialEvents(Event.Request_RegisterEventForLeaders param)
        {
            ECCRequest<string> request = new ECCRequest<string>(this, false);
            return await request.ExecuteAsync("Apps/RegisterMemberSpecialEvents", async () => {
                var user = await request.GetUser();
                return await Db.RegisterMemberSpecialEvents(param,user == null? 0 : user.UserNId);
            }, param);
        }

        [HttpPost("CancelRegisterMemberSpecialEvents")]
        public async Task<BaseApiResponse<string>> CancelRegisterMemberSpecialEvents(Event.Request_RegisterEventForLeaders param)
        {
            ECCRequest<string> request = new ECCRequest<string>(this, true);
            return await request.ExecuteAsync("Apps/CancelRegisterMemberSpecialEvents", async () => {
                var user = await request.GetUser();
                return await Db.CancelRegisterMemberSpecialEvents(param,user == null? 0 : user.UserNId );
            }, param);
        }

        [HttpPost("RegisterEventsForNewComers")]
        public async Task<BaseApiResponse<string>> RegisterEventsForNewComers(Event.Request_RegisterEventForNewComers param)
        {
            ECCRequest<string> request = new ECCRequest<string>(this, true);
            return await request.ExecuteAsync("Apps/RegisterEventsForNewComers", async () => {
                var user = await request.GetUser();
                return await Db.RegisterNonMemberSpecialEvents(param,user == null ? 0 :user.UserNId);
            }, param);
        }

        [HttpPost("GetInvitationImageUrl")]
        public async Task<BaseApiResponse<string>> GetInvitationImageUrl(long churchEventId)
        {
            return await new ECCRequest<string>(this, true).ExecuteAsync("Apps/GetInvitationImageUrl", async () => {
                return await Db.GetInvitationImageUrl(churchEventId);
            }, churchEventId);
        }

        [HttpPost("CancelRegisterNonMemberSpecialEvents")]
        public async Task<BaseApiResponse<string>> CancelRegisterNonMemberSpecialEvents(Event.Request_CancelRegisterNonMember param)
        {
            ECCRequest<string> request = new ECCRequest<string>(this, true);
            return await request.ExecuteAsync("Apps/CancelRegisterNonMemberSpecialEvents", async () => {
                var user = await request.GetUser();
                return await Db.CancelRegisterNonMemberSpecialEvents(param,user == null ? 0 :user.UserNId);
            }, param);
        }

        [HttpPost("ChurchEventParticipants")]
        public async Task<BaseApiResponse<IEnumerable<Event.Response_ChurchEventParticipants>>> ChurchEventParticipants(Event.Request_ChurchEventParticipants param)
        {
            ECCRequest<IEnumerable<Event.Response_ChurchEventParticipants>> request = new ECCRequest<IEnumerable<Event.Response_ChurchEventParticipants>>(this, true);
            return await request.ExecuteAsync("Apps/ChurchEventParticipants", async () => {
                var user = await request.GetUser();
                return await Db.GetChurchEventParticipants(param, user == null? 0 : user.UserNId);
            }, param);
        }

        [HttpPost("ChurchEventParticipantsNewComers")]
        public async Task<BaseApiResponse<IEnumerable<Event.Response_NewComersParticipant>>> ChurchEventParticipantsNewComers(long churchEventId)
        {
            ECCRequest<IEnumerable<Event.Response_NewComersParticipant>> request = new ECCRequest<IEnumerable<Event.Response_NewComersParticipant>>(this, true);
            return await request.ExecuteAsync("Apps/ChurchEventParticipantsNewComers", async () => {
                var user = await request.GetUser();
                return await Db.GetChurchEventParticipantsNewComers(churchEventId, user == null ? 0 : user.UserNId);
            }, churchEventId);
        }

        [HttpPost("ChurchEventAbsences")]
        public async Task<BaseApiResponse<string>> ChurchEventAbsences(Event.Request_ChurchEventAbsence param)
        {
            return await new ECCRequest<string>(this, true).ExecuteAsync("Apps/ChurchEventAbsences", async () => {
                return await Db.PostChurchEventAbsence(param);
            }, param);
        }

        [HttpPost("GetTicketAvailability")]
        public async Task<BaseApiResponse<IEnumerable<Event.Response_TicketAvailability>>> GetTicketAvailability(Event.Request_TicketAvailability param)
        {
            return await new ECCRequest<IEnumerable<Event.Response_TicketAvailability>>(this, true).ExecuteAsync("Apps/GetTicketAvailability", async () => {
                return await Db.GetTicketAvailablity(param);
            }, param);
        }
    }
}
