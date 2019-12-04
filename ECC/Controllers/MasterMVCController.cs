using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECC.Models;
using ECC.Services;

namespace ECC.Controllers
{
    public class MasterMVCController : Controller
    {
        public ECCContext Db ;

        public MasterMVCController()
        {
            Db = new ECCContext();
        }
    }
}
