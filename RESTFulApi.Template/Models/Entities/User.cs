namespace RESTFulApi.Template.Models.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; } = true;
		public ICollection<UserToken> UserTokens { get; set; }
	}

	public class UserToken
	{
		public int Id { get; set; }
		public string HashToken { get; set; }
		public DateTime ExpireDate { get; set; }
		public string RefreshToken { get; set; }
		public DateTime RefreshTokenExp { get; set; }
		public User User { get; set; }
		public Guid UserId { get; set; }
	}
}
