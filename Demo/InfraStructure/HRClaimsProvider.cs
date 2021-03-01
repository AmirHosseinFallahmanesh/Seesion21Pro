using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Demo.InfraStructure
{
    public class HRClaimsProvider : IClaimsTransformation
    {

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
            if (identity.IsAuthenticated && identity != null)
            {
                if (identity.Name == "c@c")
                {
                    identity.AddClaims(new Claim[] {
                        new Claim(ClaimTypes.StateOrProvince, "Tehran", ClaimValueTypes.String, "HR"),

                    });
                }
                else
                {
                    identity.AddClaims(new Claim[] {
                        new Claim(ClaimTypes.StateOrProvince, "Other City", ClaimValueTypes.String, "HR"),

                    });

                }
            }
            return Task.FromResult(principal);
        }
     
    }

    public class HR0ClaimsProvider : IClaimsTransformation
    {

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
            if (identity.IsAuthenticated && identity != null)
            {
                if (identity.Name == "c@c")
                {
                    identity.AddClaims(new Claim[] {
                        new Claim(ClaimTypes.StateOrProvince, "Tehran", ClaimValueTypes.String, "HR"),

                    });
                }
            }
            return Task.FromResult(principal);
        }

    }
}
