using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Snickler.EFCore;

namespace ECC.Models
{
    public partial class ECCContext : DbContext
    {
        public ECCContext()
        {
        }

        public ECCContext(DbContextOptions<ECCContext> options)
            : base(options)
        {
        }

        public async Task<AppUser.Response_Login> AppLogin(AppUser.Request_Login param , string appId)
        {
            DbParameter cUserToken = null;
            DbParameter cUserType = null;
            DbParameter bLoginSucceed = null;
            DbParameter cServerMessage = null;
            DbParameter nMemberNId = null;
            DbParameter nUserNId = null;

            AppUser.Response_Login result = null;
            var func = this.LoadStoredProc("AppLogin")
                .WithSqlParam("cUserId", param.Username)
                .WithSqlParam("cUserPassword", param.Password)
                .WithSqlParam("cAppId", "ECCWeb")
                .WithSqlParam("cUserToken"," ", (dbParam) =>
            {
                dbParam.Size = 40;
                dbParam.ParameterName = "cUserToken";

                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.String;
                cUserToken = dbParam;
            }).WithSqlParam("cUserType"," ", (dbParam) =>
            {
                dbParam.Size = 40;
                dbParam.ParameterName = "cUserType";
                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.String;
                cUserType = dbParam;
            }).WithSqlParam("bLoginSucceed"," ", (dbParam) =>
            {
                dbParam.ParameterName = "bLoginSucceed";
                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.Int32;
                bLoginSucceed = dbParam;
            }).WithSqlParam("cServerMessage"," ", (dbParam) =>
            {
                dbParam.Size = 100;
                dbParam.ParameterName = "cServerMessage";
                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.String;
                cServerMessage = dbParam;
            }).WithSqlParam("nMemberNId"," ", (dbParam) =>
            {
                dbParam.ParameterName = "nMemberNId";
                dbParam.IsNullable = true;
                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.Int16;
                nMemberNId = dbParam;
            }).WithSqlParam("nUserNId"," ", (dbParam) =>
            {
                dbParam.ParameterName = "nUserNId";
                dbParam.IsNullable = true;
                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.Int16;
                nUserNId = dbParam;
            }).ExecuteStoredProcAsync(handler => {
                var resultMenu = handler.ReadToList<AppUser.Response_Login.ResultMenu>().SingleOrDefault();
                handler.NextResult();
                var resultHome = handler.ReadToList<AppUser.Response_Login.ResultHome>();
                handler.NextResult();
                
                if (cUserToken?.Value != null)
                {

                    result = result == null ? new AppUser.Response_Login() : result;
                    try
                    {
                        result.cUserToken = (string)cUserToken?.Value;
                        result.cUserType = (string)cUserType?.Value;
                        result.bLoginSucceed = (int)bLoginSucceed?.Value;
                        result.cServerMessage = (string)cServerMessage?.Value;
                        result.nMemberNId = (short)nMemberNId?.Value;
                        result.nUserNId = (short)nUserNId?.Value;
                        result.Home = resultHome;
                    }
                    catch (Exception e)
                    {
                        result = null;
                    }
                }
            });
            await func;
            return result;
        }
        public async Task<AppUser.Response_Register> AppRegister(AppUser.Request_Register param , string appId)
        {
            AppUser.Response_Register result = null;
            var func = this.LoadStoredProc("InsertUserRegistration");
            func.WithSqlParam("cAppGroup", appId);
            func.WithSqlParam("cUserId", param.UserId);
            func.WithSqlParam("cUserPassword", param.Password);
            func.WithSqlParam("cFullName", param.FullName);
            func.WithSqlParam("cPhoneNumber", param.PhoneNumber);
            func.WithSqlParam("ceMailAddress", param.MailAddress);
            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<AppUser.Response_Register>().SingleOrDefault();
            });
            return result;
        }
        public async Task<AppUser.Response_GetUserByToken> AppGetUser(string token)
        {
            AppUser.Response_GetUserByToken result = null;
            var func =  this.LoadStoredProc("AppGetUser_FromTokenId");
            func.WithSqlParam("cSessionGUID",token);
           
            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<AppUser.Response_GetUserByToken>().SingleOrDefault();
            });

            return result;
        }
        public async Task<AppMember.Response> AppGetMember(string token)
        {
            AppMember.Response result = null;
            var func = this.LoadStoredProc("AppGetMember_FromTokenId");
            func.WithSqlParam("cSessionGUID", token);

            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<AppMember.Response>().SingleOrDefault();
            });

            return result;
        }
        public async Task<AppVersion.Response> AppVersion()
        {
            AppVersion.Response result = null;
            var func =  this.LoadStoredProc("GetAppVersion");
            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<AppVersion.Response>().SingleOrDefault();
            });
            return result;
        }
        public async Task<bool> AppLogout()
        {
            /*AppVersion.Response result = null;
            var func = this.LoadStoredProc("GetAppVersion");
            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<AppVersion.Response>().SingleOrDefault();
            });
            return result;*/
            return true;
        }
        public async Task<bool> SendRecoveryMail(string email)
        {
            /*AppVersion.Response result = null;
            var func = this.LoadStoredProc("GetAppVersion");
            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<AppVersion.Response>().SingleOrDefault();
            });
            return result;*/
            return true;
        }
        public async Task<IEnumerable<AppMember.Response>> GetMemberByName(AppMember.Request param)
        {
            IEnumerable<AppMember.Response> result = new List<AppMember.Response>();
            var func = this.LoadStoredProc("QueryMemberByName_InviteEvent");
            func.WithSqlParam("cMemberName", param.Name);
            func.WithSqlParam("nChurchEventNId", param.ChruchEventId);
            await func.ExecuteStoredProcAsync(handler => {
                var tmp = handler.ReadToList<AppMember.Response>();
                Console.WriteLine("RESULT FROM DB : " + Newtonsoft.Json.JsonConvert.SerializeObject(tmp, Newtonsoft.Json.Formatting.Indented));
                result = tmp;
            });
            return result;
        }
        public async Task<IEnumerable<Event.Response_MyEvent>> GetMyEvent(long userId)
        {
            IEnumerable<Event.Response_MyEvent> result = new List<Event.Response_MyEvent>();
            var func = this.LoadStoredProc("GetMyEvents");
            func.WithSqlParam("nUserNId", userId);
            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<Event.Response_MyEvent>();
            });
            return result;
        }
        public async Task<IEnumerable<Feed.Response>> GetFeeds(Feed.Request param,long userId)
        {
            IEnumerable<Feed.Response> result = new List<Feed.Response>();
            var func = this.LoadStoredProc("GetFeeds");
            func.WithSqlParam("nRowCount", 100);
            func.WithSqlParam("cFeedType", param.Type);
            func.WithSqlParam("nUserNId",userId);
            
            await func.ExecuteStoredProcAsync(handler => {
                
                result = handler.ReadToList<Feed.Response>();
            });
            return result;
        }
        public async Task<AppMember.Response_MemberDetail> GetMemberDetail(long memberId)
        {
            AppMember.Response_MemberDetail result = new AppMember.Response_MemberDetail();
            var func = this.LoadStoredProc("GetMemberProfile");
            func.WithSqlParam("nMemberNId", memberId);

            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<AppMember.Response_MemberDetail>().SingleOrDefault();
                if (result != null)
                {
                    handler.NextResult();
                    result.Home = handler.ReadToList<AppMember.Response_MemberDetail.ResultHome>();
                }
            });
            return result;
        }
        public async Task<Feed.Response> GetFeedsDetail(long feedId)
        {
            Feed.Response result = new Feed.Response();
            var func = this.LoadStoredProc("GetFeedDetail");
            func.WithSqlParam("nFeedNId", feedId);

            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<Feed.Response>().SingleOrDefault();
            });
            return result;
        }
        public async Task<Event.Response_EventDetail> GetEventDetail(Event.Request_EventDetail param , long memberId )
        {
            Event.Response_EventDetail result = new Event.Response_EventDetail();
            var func = this.LoadStoredProc("GetChurchEventDetail");
            func.WithSqlParam("nChurchEventNId", param.ChurchEventId);
            func.WithSqlParam("nMemberNId", memberId); 

            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<Event.Response_EventDetail>().SingleOrDefault();
            });
            return result;
        }
        public async Task<IEnumerable<FeedType.Response>> GetFeedsType()
        {
            IEnumerable<FeedType.Response> result = new List<FeedType.Response>();
            var func = this.LoadStoredProc("GetFeedsType");

            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<FeedType.Response>();
            });
            return result;
        }
        public async Task<string> RegisterMemberSpecialEvents(Event.Request_RegisterEventForLeaders param , long userId)
        {
            DbParameter cServerMessage = null;
            string result = string.Empty;
            var parameter = new List<object>();
            parameter.Add(new
            {
                MemberNId = param.MemberNId,
                Remark = "",
                PaymentAmount = "",
                PaymentDate = "",
                IsComing = "1",
                PostToDatabase = "I"
            }); ;
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
            var func = this.LoadStoredProc("PostChurchEventRegistration2")
                .WithSqlParam("nUserNId", userId)
                .WithSqlParam("cRegistrationDetail", Newtonsoft.Json.JsonConvert.SerializeObject(parameter))
                .WithSqlParam("nChurchEventNId", param.ChurchEventNId)
                .WithSqlParam("cServerMessage"," ", (dbParam) =>
            {
                dbParam.Size = 100;
                dbParam.ParameterName = "cServerMessage";
                dbParam.IsNullable = false;
                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.String;
                cServerMessage = dbParam;
            })
                .ExecuteStoredProcAsync(handler => {
                    handler.NextResult();
                do
                {
                        if (cServerMessage?.Value != null)
                            try
                            {
                                result = (string)cServerMessage?.Value;
                            }
                            catch (Exception e) { }
                    Console.WriteLine("VALUE : " + cServerMessage?.Value);
                } while (handler.NextResult());
            });
            await func;
            return result;
        }
        public async Task<string> CancelRegisterMemberSpecialEvents(Event.Request_RegisterEventForLeaders param , long userId )
        {
            DbParameter cServerMessage = null;
            string result = string.Empty;
            var func = this.LoadStoredProc("PostChurchEventRegistration2")
               .WithSqlParam("nUserNId", userId)
               .WithSqlParam("cRegistrationDetail", Newtonsoft.Json.JsonConvert.SerializeObject(new List<object>(){
                new{
                    MemberNId = param.MemberNId,
                    Remark = "",
                    PaymentAmount = "",
                    PaymentDate ="",
                    IsComing = "0",
                    PostToDatabase = "D"
                }
            }))
               .WithSqlParam("nChurchEventNId", param.ChurchEventNId)
               .WithSqlParam("cServerMessage", " ", (dbParam) =>
               {
                   dbParam.Size = 100;
                   dbParam.ParameterName = "cServerMessage";
                   dbParam.IsNullable = false;
                   dbParam.Direction = System.Data.ParameterDirection.Output;
                   dbParam.DbType = System.Data.DbType.String;
                   cServerMessage = dbParam;
               })
               .ExecuteStoredProcAsync(handler => {
                   do
                   {
                       if (cServerMessage?.Value != null)
                           try
                           {
                               result = (string)cServerMessage?.Value;
                           }
                           catch (Exception e) { }
                       Console.WriteLine("VALUE : " + cServerMessage?.Value);
                   } while (handler.NextResult());
               });
            await func;
            return result;
        }
        public async Task<string> RegisterNonMemberSpecialEvents(Event.Request_RegisterEventForNewComers param , long userId)
        {
            DbParameter cServerMessage = null;
            string result = string.Empty;
            var func = this.LoadStoredProc("PostChurchEventRegistration_NewComer");
            func.WithSqlParam("nUserNId", userId);
            func.WithSqlParam("nChurchEventNId", param.ChurchEventNId);
            func.WithSqlParam("cRegistrationDetail", Newtonsoft.Json.JsonConvert.SerializeObject(new List<object>(){
                new{
                    FullName = param.FullName,
                    BirthDate = param.BirthDate,
                    Sex = param.Sex,
                    MobilePhoneNumber = param.PhoneNumber,
                    eMailAddress = param.Email,
                    OfficeAddress = param.OfficeAddress,
                    Occupation = param.Occupation,
                    NewComerStatus = param.Status,
                    Deleted =0,
                    DatabaseOperation = "I",
                    RecordNId = 0,
                    MemberNId = 0
                }
            }));
            func.WithSqlParam("cServerMessage"," ", (dbParam) =>
            {
                dbParam.Size = 100;
                dbParam.ParameterName = "cServerMessage";
                dbParam.IsNullable = true;
                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.String;
                cServerMessage = dbParam;
            });
            await func.ExecuteStoredProcAsync(handler => {
                do
                {
                    if (cServerMessage?.Value != null)
                        try
                        {
                            result = (string)cServerMessage?.Value;
                        }
                        catch (Exception e) { }
                    Console.WriteLine("VALUE : " + cServerMessage?.Value);
                } while (handler.NextResult());
            });
            return result;
        }
        public async Task<string> CancelRegisterNonMemberSpecialEvents(Event.Request_CancelRegisterNonMember param,long userId)
        {
            DbParameter cServerMessage = null;
            string result = string.Empty;
            var func = this.LoadStoredProc("PostChurchEventRegistration_NewComer");
            func.WithSqlParam("nUserNId", userId);
            func.WithSqlParam("nChurchEventNId", param.ChurchEventNId);
            func.WithSqlParam("cRegistrationDetail", Newtonsoft.Json.JsonConvert.SerializeObject(new List<object>(){
                new{
                    FullName = "",
                    Deleted =0,
                    DatabaseOperation = "D",
                    RecordNId = param.RecordNId
                }
            }));
            func.WithSqlParam("cServerMessage"," ", (dbParam) =>
            {
                dbParam.Size = 100;
                dbParam.ParameterName = "cServerMessage";
                dbParam.IsNullable = true;
                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.String;
                cServerMessage = dbParam;
            });
            await func.ExecuteStoredProcAsync(handler => {
                do
                {
                    if(cServerMessage?.Value != null)
                        try
                        {
                            result = (string)cServerMessage?.Value;
                        }
                        catch (Exception e) { }
                    Console.WriteLine("VALUE : " + cServerMessage?.Value);
                } while (handler.NextResult());
                
            });
            return result;
        }
        public async Task<string> GetInvitationImageUrl(long churchEventId)
        {
            DbParameter cServerMessage = null;
            string result = string.Empty;
            var func = this.LoadStoredProc("GetInvitationUrl");
            func.WithSqlParam("nChurchEventNId", churchEventId);
            func.WithSqlParam("cInvitationUrl", " ", (dbParam) =>
            {
                dbParam.Size = 100;
                dbParam.ParameterName = "cInvitationUrl";
                dbParam.IsNullable = true;
                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.String;
                cServerMessage = dbParam;
            });
            await func.ExecuteStoredProcAsync(handler => {
                var url = handler.ReadToList<Event.Response_InvitationUrl>().SingleOrDefault();
                result = url == null ? string.Empty : url.InvitationUrl;
            });
            return result;
        }
        public async Task<IEnumerable<Event.Response_ChurchEventParticipants>> GetChurchEventParticipants(Event.Request_ChurchEventParticipants param , long userNId )
        {
            IEnumerable<Event.Response_ChurchEventParticipants> result = new List<Event.Response_ChurchEventParticipants>();
            var func = this.LoadStoredProc("GetListChurchEventParticipants");
            func.WithSqlParam("nUserNId", userNId);
            func.WithSqlParam("nChurchEventNId", param.ChurchEventId);
            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<Event.Response_ChurchEventParticipants>();
            });
            return result;
        }
        public async Task<IEnumerable<Event.Response_NewComersParticipant>> GetChurchEventParticipantsNewComers(long churchEventId, long userNId)
        {
            IEnumerable<Event.Response_NewComersParticipant> result = new List<Event.Response_NewComersParticipant>();
            var func = this.LoadStoredProc("GetListChurchEventParticipants_NewComer");
            func.WithSqlParam("nUserNId", userNId);
            func.WithSqlParam("nChurchEventNId", churchEventId);
            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<Event.Response_NewComersParticipant>();
            });
            return result;
        }
        public async Task<string> PostChurchEventAbsence(Event.Request_ChurchEventAbsence param )
        {
            DbParameter cServerMessage = null;
            string result = string.Empty;
            var func = this.LoadStoredProc("PostChurchEventAbsence");
            func.WithSqlParam("cQRCode", param.QrCode);
            func.WithSqlParam("nUserNId", param.UserId);
            func.WithSqlParam("nChurchEventNId", param.ChurchEventId);
            func.WithSqlParam("cServerMsg"," ", (dbParam) =>
            {
                dbParam.Size = 200;
                dbParam.ParameterName = "cServerMsg";
                dbParam.IsNullable = true;
                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.String;
                cServerMessage = dbParam;
            });
            try{
                await func.ExecuteStoredProcAsync(handler => {
                    do
                    {
                        if (cServerMessage?.Value != null)
                            try
                            {
                                result = (string)cServerMessage?.Value;
                            }
                            catch (Exception e) {
                                
                            }
                        Console.WriteLine("VALUE : " + cServerMessage?.Value);
                    } while (handler.NextResult());
                });
            }catch(Exception e)
            {
                if(e is SqlException)
                {
                    result = e.Message;
                }
            }
            return result;
        }
        public async Task<IEnumerable<Event.Response_TicketAvailability>> GetTicketAvailablity(Event.Request_TicketAvailability param)
        {
            IEnumerable<Event.Response_TicketAvailability> result = new List<Event.Response_TicketAvailability>();
            var func = this.LoadStoredProc("GetTicketAvailability");
            func.WithSqlParam("nUserNId", param.UserId.IsEmpty() ? null : param.UserId);
            func.WithSqlParam("nChurchEventNId", param.ChurchEventId.IsEmpty() ? null : param.ChurchEventId);
            await func.ExecuteStoredProcAsync(handler => {
                result = handler.ReadToList<Event.Response_TicketAvailability>();
            });
            return result;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                optionsBuilder.UseSqlServer("Data Source=165.22.103.161,1433;Initial Catalog=ECCDB;User ID=sa;Password=ECCStrongPassword(!);");
            }
        }

    }
}
