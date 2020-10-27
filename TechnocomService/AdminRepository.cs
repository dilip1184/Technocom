using TechnocomShared.Entities;
using TechnocomShared.EntityLoader;
using TechnocomShared.Exceptions;
using TechnocomShared.Interfaces;
using System;
using System.Collections.Generic;

namespace TechnocomService
{
    public class AdminRepository : BaseService, IBusinessService
    {
        public AdminRepository()
            : base()
        {
        }
        public AdminRepository(ContextInfo context)
            : base(context)
        {

        }
    }
}