using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetCoreJwtBasicExample.Helpers
{
    public class TokenGenerator
    {
        public string SignatureKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public DateTime Expires { get; set; }

        private SymmetricSecurityKey key;
        private SigningCredentials credentials;
        private List<Claim> claims;
        private JwtSecurityToken token;
        private JwtSecurityTokenHandler handler;

        public TokenGenerator(string signatureKey, string issuer = "http://localhost", string audience = "http://localhost", string expires = "3600")
        {
            SignatureKey = signatureKey;
            Issuer = issuer;
            Audience = audience;
            Expires = DateTime.Now.AddSeconds(Convert.ToDouble(expires));
            var bytes = Encoding.UTF8.GetBytes(SignatureKey);
            key = new SymmetricSecurityKey(bytes);
            credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }
        public string CreateToken()
        {
            token = new JwtSecurityToken(issuer: Issuer, audience: Audience, notBefore: DateTime.Now, expires: Expires, signingCredentials: credentials);
            handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }

        public string CreateTokenWithAdminRole()
        {
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,"Admin")
            };
            token = new JwtSecurityToken(issuer: Issuer, audience: Audience, notBefore: DateTime.Now, expires: Expires, signingCredentials: credentials, claims: claims);
            handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}
