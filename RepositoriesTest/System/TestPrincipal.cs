﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.System
{
    public class TestPrincipal : ClaimsPrincipal
    {
        public TestPrincipal(params Claim[] claims) : base(new TestIdentity(claims))
        {
        }
    }
}
