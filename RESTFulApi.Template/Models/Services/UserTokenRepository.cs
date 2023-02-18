using RESTFulApi.Template.Models.Context;
using RESTFulApi.Template.Models.Entities;

namespace RESTFulApi.Template.Models.Services
{
	public class UserTokenRepository
	{
		private readonly DatabaseContext _context;
		public UserTokenRepository(DatabaseContext context)
		{
			_context = context;
		}

		public void SaveToken(UserToken userToken)
		{
			_context.UserTokens.Add(userToken);
			_context.SaveChanges();
		}
	}
}
