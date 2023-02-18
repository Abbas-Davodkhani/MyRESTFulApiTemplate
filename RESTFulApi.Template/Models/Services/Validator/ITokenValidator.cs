using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RESTFulApi.Template.Models.Services.Validator
{
	public interface ITokenValidator
	{
		Task Execute(TokenValidatedContext context);
	}
}
