using System;
using ECC.Controllers;

namespace ECC.Services
{
    public class AuthServices
    {
        public AuthServices(MasterController controller)
        {
            
        }
        public string GenerateToken()
        {
            return Cryptographer.Encrypt(new Guid().ToString());
        }
    }
}