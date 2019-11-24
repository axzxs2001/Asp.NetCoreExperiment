using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GRPCDemo01Service
{
    public class JwtToken
    {
        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static dynamic BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: permissionRequirement.Issuer,
                audience: permissionRequirement.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(permissionRequirement.Expiration),
                signingCredentials: permissionRequirement.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                Status = true,
                access_token = encodedJwt,
                expires_in = permissionRequirement.Expiration.TotalMilliseconds,
                token_type = "Bearer"
            };
            return response;
        }
    }
}
