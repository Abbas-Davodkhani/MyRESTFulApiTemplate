using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RESTFulApi.Template.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    
        [HttpPost]
        public IActionResult Login(string userName = "test" , string password = "test")
        {
            if (true)
            {

                var userClaims = new List<Claim>()
                {
                    new Claim("Username" , userName) ,
                    new Claim("Password" , password)
                };
                string key = configuration["JwtConfig:Key"];
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var signInCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                    (
                        issuer : configuration["JwtConfig:issuer"], 
                        audience : configuration["JwtConfig:audience"],
                        expires : DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtConfig:expires"])),
                        notBefore : DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtConfig:notBefore"])),
                        claims : userClaims ,
                        signingCredentials : signInCredential
                    );


                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(jwtToken);
            }
        }
    }
}
