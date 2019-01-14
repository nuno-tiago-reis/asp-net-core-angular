using Kindly.API.Utility.Collections;

using System;
using System.Threading.Tasks;

namespace Kindly.API.Models.Repositories.Users
{
	public interface IUserRepository : IEntityRepository<User, UserParameters>
	{
		/// <summary>
		/// Checks if a user exists with the given email.
		/// </summary>
		/// 
		/// <param name="email">The email.</param>
		Task<bool> EmailExists(string email);

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
		/// Adds the role to the user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="roleID">The role identifier.</param>
		Task AddRoleToUser(Guid userID, Guid roleID);

		/// <summary>
		/// Removes the role from the user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="roleID">The role identifier.</param>
		Task RemoveRoleFromUser(Guid userID, Guid roleID);

		/// <summary>
		/// Gets the user with its roles.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		Task<User> GetUserWithRoles(Guid userID);

		/// <summary>
		/// Gets all the users with their respective roles.
		/// </summary>
		/// 
		/// <param name="parameters">The parameters.</param>
		Task<PagedList<User>> GetUsersWithRoles(UserParameters parameters);

		/// <summary>
		/// Gets the user with its pictures.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="includeUnapprovedPictures">Whether to include unapproved pictures.</param>
		Task<User> GetUserWithPictures(Guid userID, bool includeUnapprovedPictures);
	}
}