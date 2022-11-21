using AddressBookApi.Contract;
using AddressBookApi.Entities.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AddressBookApi.Repository
{
    public class TokenHandler:IToken
    { 
      private readonly IConfiguration configure;
      public TokenHandler(IConfiguration configure)
      {
         this.configure = configure;
      }
      public string GenerateToken(LoginDto user)
      {
            List<Claim> Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configure["jwt:key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                configure["jwt:Issuer"],
                configure["jwt:Audience"],
                Claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
