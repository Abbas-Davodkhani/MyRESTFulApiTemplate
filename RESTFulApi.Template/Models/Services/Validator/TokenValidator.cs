using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Any;
using System.Security.Claims;

namespace RESTFulApi.Template.Models.Services.Validator
{
	public class TokenValidator : ITokenValidator
	{
		private readonly UserRepository userRepository;
		public TokenValidator(UserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task Execute(TokenValidatedContext context)
		{
			var claims = context.Principal.Identity as ClaimsIdentity;

			if(claims.Claims == null || !claims.Claims.Any()) 
			{
				context.Fail("User claims is not valid ... ");
				return;	
			}

			var user = userRepository.Get();
			if(user == null || !user.IsActive) 
			{
				context.Fail("User is not active ... ");
				return;
			}

			var userId = context.Principal.FindFirst("UserId").Value;
			if(!Guid.TryParse(userId ,out Guid userGuid)) 
			{
				context.Fail("User id is not valid ...");
				return;
			}
		}
	}
}
