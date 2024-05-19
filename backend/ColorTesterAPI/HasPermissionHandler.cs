using Microsoft.AspNetCore.Authorization;

namespace ColorTesterAPI;

public class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirement>
{
    // constructor
    public HasPermissionHandler()
    {
        // log "HasPermissionHandler created" to console
        Console.WriteLine("HasPermissionHandler created");
    }
    
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        HasPermissionRequirement requirement
    )
    {
        // If user does not have the scope claim, get out of here
        if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer))
            return Task.CompletedTask;

        // there are multiple permission claims, so we need to check them all
        var permissions = context.User.FindAll(c => c.Type == "permissions" && c.Issuer == requirement.Issuer);
        
        // if any of the permissions claims contains the required permission, return success
        if (permissions.Any(p => p.Value == requirement.Permission))
            context.Succeed(requirement);
        
        return Task.CompletedTask;
    }
}