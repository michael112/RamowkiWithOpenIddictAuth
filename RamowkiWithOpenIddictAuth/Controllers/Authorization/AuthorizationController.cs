﻿using System;
using System.Linq;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Mvc;
using Rework;

using RamowkiWithOpenIddictAuth.Models.Authentication;
using RamowkiWithOpenIddictAuth.Services.User;

namespace RamowkiWithOpenIddictAuth.Controllers.Authorization
{
    public class AuthorizationController : Controller
    {
        private IUserService userService;

        public AuthorizationController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("~/connect/token"), Produces("application/json")]
        public IActionResult Exchange(OpenIdConnectRequest request)
        {
            if (!(request.IsPasswordGrantType()))
            {
                throw new InvalidOperationException("Grant type not supported.");
            }
            else
            {
                try
                {
                    User user = this.userService.FindUserByUserName(request.Username);
                    string encryptedPassword = request.Password.ToSHA(Crypto.SHA_Type.SHA256).ToLower();
                    if (!(user.PasswordHash.ToLower().Equals(encryptedPassword)))
                    {
                        return new UnauthorizedResult();
                    }
                    var identity = new ClaimsIdentity(OpenIdConnectServerDefaults.AuthenticationScheme, OpenIdConnectConstants.Claims.Name, OpenIdConnectConstants.Claims.Role);
                    identity.AddClaim(OpenIdConnectConstants.Claims.Subject, user.Id, OpenIdConnectConstants.Destinations.AccessToken);
                    identity.AddClaim(OpenIdConnectConstants.Claims.Name, user.UserName, OpenIdConnectConstants.Destinations.AccessToken);
                    identity.AddClaim(OpenIdConnectConstants.Claims.Email, user.Email, OpenIdConnectConstants.Destinations.AccessToken);
                    foreach (var role in user.RoleJoins.Select(j => j.Role))
                    {
                        identity.AddClaim(OpenIdConnectConstants.Claims.Role, role.Name, OpenIdConnectConstants.Destinations.AccessToken);
                    }
                    return SignIn(new ClaimsPrincipal(identity), OpenIdConnectServerDefaults.AuthenticationScheme);
                }
                catch(InvalidOperationException)
                {
                    return new UnauthorizedResult();
                }
            }
        }
    }
}