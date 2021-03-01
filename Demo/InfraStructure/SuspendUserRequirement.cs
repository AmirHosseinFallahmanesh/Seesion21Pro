using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace Demo.InfraStructure
{
    public class SuspendUserRequirement: IAuthorizationRequirement
    {
        public string[] Users { get; set; }
        public SuspendUserRequirement(params string[] users)
        {
            Users = users;
        }
    }
}
