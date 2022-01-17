
using Business.Utilities.Security.Abstract;
using Business.Utilities.Security.Helpers;
using Entities;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private IConfiguration _configuration;
        private TokenOptions tokenOptions;
        private DateTime accessTokenExpiration;
        private List<Claim> claims;
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
            accessTokenExpiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);
        }
        public AccessToken CreateToken(User user)
        {
            var securityKey = SecurityKeyHelper.CreateSymmetricSecurityKey(tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = SetJwt(tokenOptions,signingCredentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new AccessToken() { Token = token, Expiration = accessTokenExpiration };
        }

        
        public JwtSecurityToken SetJwt(TokenOptions tokenOptions,SigningCredentials signingCredentials)
        {
            return new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );
        }

    }
}
