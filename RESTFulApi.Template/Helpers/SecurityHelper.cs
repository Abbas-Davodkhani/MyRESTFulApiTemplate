using System.Security.Cryptography;
using System.Text;

namespace RESTFulApi.Template.Helpers
{
	public class SecurityHelper
	{
		private readonly RandomNumberGenerator numberGenerator = RandomNumberGenerator.Create();	
		public string Getsha256Hash(string value)
		{
			var algoritm = new SHA256CryptoServiceProvider();
			var byteValue = Encoding.UTF8.GetBytes(value);
			var byteHash = algoritm.ComputeHash(byteValue);
			return Convert.ToBase64String(byteHash);
		}
	}
}
