using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MinimumAgeRequirement requirement)
    {
        var ageClaim = context.User.FindFirst("age");

        if (ageClaim != null && int.Parse(ageClaim.Value) >= requirement.Age)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}