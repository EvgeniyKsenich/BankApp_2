using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BA.Business.Modeles;
using BA.Business.Repositories;
using BA.Database.UnitOfWork;
using BA.Web.Auth;
using BA.Web.Modeles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BA.Web.Controllers
{
    [Route("Identity")]
    public class IdentityController : Controller
    {
        private IUnitOfWork _Unit;
        private IMapper _Mapper;
        private IOptions<Identity> Identity_;
        private IPasswordEngine PasswordEngine_;

        public IdentityController(IPasswordEngine PasswordEngine, IUnitOfWork Unit, IMapper mapper, IOptions<Identity> Identity)
        {
            _Mapper = mapper;
            _Unit = Unit;
            Identity_ = Identity;
            PasswordEngine_ = PasswordEngine;
        }

        [Route("Token")]
        public async Task Token(UserAuth User_)
        {
            var username = User_.Username;
            var password = User_.Password;

            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;
            // create JWT-token
            var jwt = new JwtSecurityToken(
                    issuer: Identity_.Value.Publisher,
                    audience: Identity_.Value.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(Identity_.Value.LifeTime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(Identity_.Value.Key), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var person = _Unit.Users.Get(username);
            if (person != null)
            {
                if (person.Password == PasswordEngine_.GetHash( string.Concat(password, Identity_.Value.Salt) ))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserName),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Password)
                    };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
            }

            return null;
        }
    }
}