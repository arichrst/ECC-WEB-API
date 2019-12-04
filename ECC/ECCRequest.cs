using System;
using System.Linq;
using ECC.Models;
using ECC.Controllers;
using ECC.Services;
using System.Threading.Tasks;

namespace ECC
{
    public class ECCRequest<T>
    {
        public DateTime RequestTime { get; set; }
        public MasterController Controller { get; set; }

        bool TokenChecking;
        public ECCRequest(MasterController controller, bool isTokenChecking = true)
        {
            RequestTime = DateTime.UtcNow;
            Controller = controller;
            TokenChecking = isTokenChecking;
            
        }

        public async Task<AppMember.Response> GetMember()
        {
            if (Controller.Request.Headers.ContainsKey("token"))
            {
                try
                {
                    var token = Controller.Request.Headers["token"].ToString();
                    Console.WriteLine("TOKEN : " + Newtonsoft.Json.JsonConvert.SerializeObject(token));
                    var result = await Controller.Db.AppGetMember(token);
                    Console.WriteLine("MEMBERDATA : " + Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    return result;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<AppUser.Response_GetUserByToken> GetUser()
        {
            if (Controller.Request.Headers.ContainsKey("token"))
            {
                try
                {
                    var token = Controller.Request.Headers["token"].ToString();
                    Console.WriteLine("TOKEN : " + Newtonsoft.Json.JsonConvert.SerializeObject(token));
                    var result = await Controller.Db.AppGetUser(token);
                    Console.WriteLine("USERDATA : " + Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    return result;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public string GetAppId()
        {
            if (Controller.Request.Headers.ContainsKey("appid"))
            {
                try
                {
                    var appid = Controller.Request.Headers["appid"].ToString();
                    Console.WriteLine("APPID : " + Newtonsoft.Json.JsonConvert.SerializeObject(appid));
                    return appid;
                }
                catch
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        public string GetToken()
        {
            if (Controller.Request.Headers.ContainsKey("token"))
            {
                try
                {
                    var token = Controller.Request.Headers["token"].ToString();
                    Console.WriteLine("Token : " + Newtonsoft.Json.JsonConvert.SerializeObject(token));
                    return token;
                }
                catch
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }
        async Task<bool> IsTokenAlive()
        {
            if(TokenChecking)
            {
                return await GetUser() != null;
            }
            return true;
        }

        
#region //FUNDAMENTAL FUNCTIONS//
        

        public async Task<BaseApiResponse<T>> ExecuteAsync(string url, Func<Task<T>> action, object param = null , string message = "")
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine("CALL API : " + url + " ===================================");
            Console.WriteLine("PARAM : " + Newtonsoft.Json.JsonConvert.SerializeObject(param, Newtonsoft.Json.Formatting.Indented));

            
            if(await IsTokenAlive())
            {
                var result = action();
                try{
                    return await SuccessAsync(result,result == null ? "No Data" : message);
                }catch(Exception e)
                {
                    return await FailureAsync(result,e.Message);
                }
            }
            else{
                var result = action();
                return await UnauthorizedAsync(result);
            }
        }

        void RecordResponse(BaseApiResponse<T> content, string message)
        {
            Console.WriteLine("\nRESPONSE : " + Newtonsoft.Json.JsonConvert.SerializeObject(content, Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine("\nMESSAGE : " + message);
            Console.WriteLine("\nEND OF CALL API ========================================");
            Console.WriteLine("\n\n\n");
        }


        public async Task<BaseApiResponse<T>> SuccessAsync(Task<T> content , string message = "")
        {
            var result = new BaseApiResponse<T>(await content,true,RequestTime,message.IsEmpty() ? "Successfully retreiving data" : message);
            RecordResponse(result,message.IsEmpty() ? "Successfully retreiving data" : message);
            return result;
        }

        public async Task<BaseApiResponse<T>> FailureAsync(Task<T> content , string message = "")
        {
            var result = new BaseApiResponse<T>(await content,false,RequestTime,message.IsEmpty() ? "Unknown error detected" : message);
            RecordResponse(result,message.IsEmpty() ? "Unknown error detected" : message);
            return result;
        }

        public async Task<BaseApiResponse<T>> UnauthorizedAsync(Task<T> content)
        {
            var result = new BaseApiResponse<T>(await content,false,RequestTime,"Unauthorized");
            result.IsLoginRequired = true;
            RecordResponse(result,"Unauthorized");
            return result;
        }
#endregion
    }
}