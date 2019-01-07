using System.Threading.Tasks;

namespace Kindly.API.Models.Repositories.Users
{
	public interface IUserRepository : IEntityRepository<User, UserParameters>
	{
		/// <summary>
		/// Checks if a user exists with the given user name.
		/// </summary>
		/// 
		/// <param name="userName">The user name.</param>
		Task<bool> UserNameExists(string userName);

		/// <summary>
		/// Checks if a user exists with the given phone number.
		/// </summary>
		/// 
		/// <param name="phoneNumber">The phone number.</param>
		Task<bool> PhoneNumberExists(string phoneNumber);

		/// <summary>
		/// Checks if a user exists with the given email address.
		/// </summary>
		/// 
		/// <param name="emailAddress">The email address.</param>
		Task<bool> EmailAddressExists(string emailAddress);

		/// <summary>
		/// Gets a user by user name.
		/// </summary>
		/// 
		/// <param name="userName">The user name.</param>
		Task<User> GetByUserName(string userName);

		/// <summary>
		/// Gets a user by phone number.
		/// </summary>
		/// 
		/// <param name="phoneNumber">The phone number.</param>
		Task<User> GetByPhoneNumber(string phoneNumber);

		/// <summary>
		/// Gets a user by email address.
		/// </summary>
		/// 
		/// <param name="emailAddress">The email address.</param>
		Task<User> GetByEmailAddress(string emailAddress);
	}
}