using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using DotNetBankAPI_1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SQLitePCL;

namespace DotNetBankAPI_1.SecureWebAPI
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly DotNetBankAPIContext _context;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, DotNetBankAPIContext dotNetBankAPIContext) : base(options, logger, encoder, clock)
        {
            _context = dotNetBankAPIContext;
        }

        protected  override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                {
                    return AuthenticateResult.Fail("Authorization header was not found");
                }
                //YWRtaW46YWRtaW4=
                var authenticationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(authenticationHeader.Parameter);
                string[] credentials = Encoding.UTF8.GetString(bytes).Split(':');

                string userName = credentials[0];
                string password = credentials[1];

                //  var user = APISecurity.Login(userName, password);
                UserStore user = _context.UserStore.Where(user => user.Login == userName && user.Password == password).FirstOrDefault();
                if (user != null)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Login), new Claim(ClaimTypes.Role, user.Login) };


                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }

                else
                {
                    return AuthenticateResult.Fail("No user found");
                }
            }
            catch(Exception e)
            {
                return AuthenticateResult.Fail("Failed to retreive the User info");
            }


        }
    }
}
