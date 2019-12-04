using System;
using Microsoft.AspNetCore.Http;

namespace ECC
{
    public class Upload
    {
        public class Request{
            public string Folder { get; set; }
            public string Extension { get; set; }
        }
    }
}