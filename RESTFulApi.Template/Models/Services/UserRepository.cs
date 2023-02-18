using RESTFulApi.Template.Models.Context;
using RESTFulApi.Template.Models.Entities;

namespace RESTFulApi.Template.Models.Services
{
	public class UserRepository
	{
		private readonly DatabaseContext _context;
		public UserRepository(DatabaseContext context)
		{
			_context = context;
		}

		public User Get()
		{
			return _context.Users.First();
		}

		public bool ValidateUser(string username)
		{
			return true;
		}
	}
}
