using System;
using System.Linq;

namespace ECC
{
    public class BaseApiResponse<T>
    {
        private DateTime RequestTime { get; set; }
        private DateTime ResponseTime { get; set; }
        public string Request { get {return RequestTime.To_ddMMMyyyymmhhss();} }
        public string Response {get {return ResponseTime.To_ddMMMyyyymmhhss();} }
        public string Message { get; set; }
        public T Content { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsLoginRequired { get; set; }
        public string Latency { 
            get{
                try{
                    return (ResponseTime - RequestTime).TotalMilliseconds.ToString("0.000") + " ms";
                }catch(Exception)
                {
                    return "Maaf terjadi kesalahan pada server kami, mohon untuk mengulanginya beberapa saat lagi";
                }
            }
        }

        public BaseApiResponse(T content, bool isSuccess, DateTime requestTime, string message = "")
        {
            ResponseTime = DateTime.UtcNow;
            RequestTime = requestTime;
            Message = message;
            IsSuccess = isSuccess;
            Content = content;
        }
    }
}