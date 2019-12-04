using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECC.Models;
using ECC.Services;

namespace ECC.Controllers
{
    public class MasterController : ControllerBase
    {
        public ECCContext Db ;
        public FileServices ECCFile;
        public NotificationServices ECCNotification;
        public MailServices ECCMail;

        public AuthServices ECCAuth;

        public MasterController()
        {
            Db = new ECCContext();
            ECCFile = new FileServices(this);
            ECCNotification = new NotificationServices(this);
            ECCMail = new MailServices(this);
            ECCAuth = new AuthServices(this);
        }
    }
}
