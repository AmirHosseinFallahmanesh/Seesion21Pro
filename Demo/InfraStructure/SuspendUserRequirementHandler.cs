using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.InfraStructure
{
    public class SuspendUserRequirementHandler : AuthorizationHandler<SuspendUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SuspendUserRequirement requirement)
        {
            if (context.User.Identity!=null )
            {
                if (!requirement.Users.Any(a => a.Equals(context.User.Identity.Name)))
                {
                     context.Succeed(requirement);
                } 
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;   
        }
    }
}
