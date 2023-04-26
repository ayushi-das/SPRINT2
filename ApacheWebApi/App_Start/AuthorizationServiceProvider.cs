using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using ApacheDAL;
using Microsoft.Owin.Security.OAuth;

namespace ApacheWebApi.App_Start
{
    public class AuthorizationServiceProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AccountDataAccess dal = new AccountDataAccess();
            var result = dal.GetAllUsers().Where(o => o.Id == context.UserName).SingleOrDefault();

            if (result != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
        }
    }
}