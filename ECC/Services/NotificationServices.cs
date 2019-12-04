using System.Collections.Generic;
using ECC.Controllers;
using ECC.Models;

namespace ECC.Services
{
    public class NotificationServices
    {
        public NotificationServices(MasterController controller)
        {
            
        }

        public void SendToMe(string message)
        {}

        public void SendToOthers(AppUser target , string message)
        {}
 
        public void SendToOthers(IEnumerable<AppUser> target , string message)
        {
            if (target is null)
            {
                throw new System.ArgumentNullException(nameof(target));
            }
        }
    }
}