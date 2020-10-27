using TechnocomShared.Entities;
using TechnocomService;
using TechnocomShared.Enums;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TechnocomWeb.APIService
{
    public class ERPController : ApiController
    {
        [HttpGet]
        public string test()
        {
            return "OK";
        }
    }
}
