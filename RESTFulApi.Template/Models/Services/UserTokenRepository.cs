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

		public UserToken GetRefreshToken(string refreshToken) 
		{
			var userToken = _context.UserTokens.SingleOrDefault(x => x.RefreshToken == refreshToken);
			if(userToken != null) 
				return userToken;

			return null;
		}

		public void DeleteRefreshToken(string refreshToken)
		{
			var userToken = GetRefreshToken(refreshToken);
			if(userToken != null)
			{
				_context.UserTokens.Remove(userToken);
				_context.SaveChanges();
			}
		}
	}
}
