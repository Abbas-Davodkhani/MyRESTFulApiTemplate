using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RESTFulApi.Template.Helpers;
using RESTFulApi.Template.Models.Services;
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
        private readonly UserRepository _userRepository;
        private readonly UserTokenRepository _userTokenRepository;
        public AccountController(IConfiguration configuration 
            , UserTokenRepository userTokenRepository 
            , UserRepository userRepository)
        {
            this.configuration = configuration;
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
        }

        [HttpPost]
        public IActionResult Login(string userName = "test" , string password = "test")
        {
            if (_userRepository.ValidateUser(userName))
            {
                var user = _userRepository.Get();
                var userClaims = new List<Claim>()
                {
                    new Claim("UserId" , user.Id.ToString()),
                    new Claim("Username" , user.Name)
                };
                string key = configuration["JwtConfig:Key"];
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var signInCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var expire = DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtConfig:expires"]));

				var token = new JwtSecurityToken
                    (
                        issuer : configuration["JwtConfig:issuer"], 
                        audience : configuration["JwtConfig:audience"],
                        expires : expire,
                        notBefore : DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtConfig:notBefore"])),
                        claims : userClaims ,
                        signingCredentials : signInCredential
                    );


                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                SecurityHelper security = new SecurityHelper();

                _userTokenRepository.SaveToken(new Models.Entities.UserToken
                { 
                    ExpireDate = expire , 
                    HashToken = security.Getsha256Hash(jwtToken) , 
                    User = user
                });
                return Ok(jwtToken);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
