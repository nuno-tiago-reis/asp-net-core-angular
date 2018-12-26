using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Microsoft.EntityFrameworkCore;

using Kindly.API.Models.Domain;
using Kindly.API.Utility;

namespace Kindly.API.Models.Repositories
{
	public sealed class UserRepository : IUserRepository
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public KindlyContext Context { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="UserRepository"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>
		public UserRepository(KindlyContext context)
		{
			this.Context = context;
		}
		#endregion

		#region [Methods] IEntityRepository
		/// <inheritdoc />
		public async Task<User> Create(User user)
		{
			// Keys
			if (string.IsNullOrWhiteSpace(user.UserName))
				throw new KindlyException(user.InvalidFieldMessage(u => u.UserName));
			if (await this.UserNameExists(user.UserName))
				throw new KindlyException(user.ExistingFieldMessage(u => u.UserName));

			if (string.IsNullOrWhiteSpace(user.PhoneNumber))
				throw new KindlyException(user.InvalidFieldMessage(u => u.PhoneNumber));
			if (await this.PhoneNumberExists(user.PhoneNumber))
				throw new KindlyException(user.ExistingFieldMessage(u => u.PhoneNumber));

			if (string.IsNullOrWhiteSpace(user.EmailAddress))
				throw new KindlyException(user.InvalidFieldMessage(u => u.EmailAddress));
			if (await this.EmailAddressExists(user.EmailAddress))
				throw new KindlyException(user.ExistingFieldMessage(u => u.EmailAddress));

			// Properties
			if (string.IsNullOrWhiteSpace(user.KnownAs))
				throw new KindlyException(user.InvalidFieldMessage(u => u.KnownAs));

			if (string.IsNullOrWhiteSpace(user.City))
				throw new KindlyException(user.InvalidFieldMessage(u => u.City));

			if (string.IsNullOrWhiteSpace(user.Country))
				throw new KindlyException(user.InvalidFieldMessage(u => u.Country));

			if (user.Gender == default(Gender))
				throw new KindlyException(user.InvalidFieldMessage(u => u.Gender));

			if (user.BirthDate == default(DateTime))
				throw new KindlyException(user.InvalidFieldMessage(u => u.BirthDate));

			// Create
			this.Context.Add(user);
			await this.Context.SaveChangesAsync();

			return user;
		}

		/// <inheritdoc />
		public async Task<User> Update(User user)
		{
			var databaseUser = await this.Context.Users.FindAsync(user.ID);
			if (databaseUser == null)
				throw new KindlyException(User.DoesNotExist, true);

			// Keys
			if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && databaseUser.PhoneNumber != user.PhoneNumber && await this.PhoneNumberExists(user.PhoneNumber))
				throw new KindlyException(user.ExistingFieldMessage(u => u.PhoneNumber));

			if (!string.IsNullOrWhiteSpace(user.EmailAddress) && databaseUser.EmailAddress != user.EmailAddress && await this.EmailAddressExists(user.EmailAddress))
				throw new KindlyException(user.ExistingFieldMessage(u => u.EmailAddress));

			databaseUser.PhoneNumber =
				!string.IsNullOrWhiteSpace(user.PhoneNumber) ? user.PhoneNumber : databaseUser.PhoneNumber;

			databaseUser.EmailAddress =
				!string.IsNullOrWhiteSpace(user.EmailAddress) ? user.EmailAddress : databaseUser.EmailAddress;

			// Properties
			databaseUser.KnownAs =
				!string.IsNullOrWhiteSpace(user.KnownAs) ? user.KnownAs : databaseUser.KnownAs;

			databaseUser.Introduction =
				!string.IsNullOrWhiteSpace(user.Introduction) ? user.Introduction : databaseUser.Introduction;

			databaseUser.Interests =
				!string.IsNullOrWhiteSpace(user.Interests) ? user.Interests : databaseUser.Interests;

			databaseUser.LookingFor =
				!string.IsNullOrWhiteSpace(user.LookingFor) ? user.LookingFor : databaseUser.LookingFor;

			databaseUser.City =
				!string.IsNullOrWhiteSpace(user.City) ? user.City : databaseUser.City;

			databaseUser.Country =
				!string.IsNullOrWhiteSpace(user.Country) ? user.Country : databaseUser.Country;

			databaseUser.Gender =
				user.Gender != default(Gender) ? user.Gender : databaseUser.Gender;

			databaseUser.BirthDate =
				user.BirthDate != default(DateTime) ? user.BirthDate : databaseUser.BirthDate;

			databaseUser.LastActiveAt =
				user.LastActiveAt != default(DateTime) ? user.LastActiveAt : databaseUser.LastActiveAt;

			databaseUser.Pictures =
				user.Pictures ?? databaseUser.Pictures;

			// Update
			await this.Context.SaveChangesAsync();

			return databaseUser;
		}

		/// <inheritdoc />
		public async Task Delete(Guid userID)
		{
			var databaseUser = await this.Context.Users.FindAsync(userID);
			if (databaseUser == null)
				throw new KindlyException(User.DoesNotExist, true);

			// Delete
			this.Context.Users.Remove(databaseUser);
			await this.Context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<User> Get(Guid userID)
		{
			return await this.Context.Users
				.Include(user => user.Pictures)
				.SingleOrDefaultAsync(user => user.ID == userID);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<User>> GetAll()
		{
			return await this.Context.Users
				.Include(user => user.Pictures)
				.ToListAsync();
		}
		#endregion

		#region [Methods] IUserRepository
		/// <inheritdoc />
		public async Task<User> LoginWithID(Guid userID, string password)
		{
			var databaseUser = await this.Get(userID);

			await this.Login(databaseUser, password);

			return databaseUser;
		}

		/// <inheritdoc />
		public async Task<User> LoginWithUserName(string userName, string password)
		{
			var databaseUser = await this.GetByUserName(userName);

			await this.Login(databaseUser, password);

			return databaseUser;
		}

		/// <inheritdoc />
		public async Task<User> LoginWithPhoneNumber(string phoneNumber, string password)
		{
			var databaseUser = await this.GetByPhoneNumber(phoneNumber);

			await this.Login(databaseUser, password);

			return databaseUser;
		}

		/// <inheritdoc />
		public async Task<User> LoginWithEmailAddress(string emailAddress, string password)
		{
			var databaseUser = await this.GetByEmailAddress(emailAddress);

			await this.Login(databaseUser, password);

			return databaseUser;
		}

		/// <inheritdoc />
		public async Task AddPassword(User user, string password)
		{
			var databaseUser = await this.Get(user.ID);
			if (databaseUser == null)
				throw new KindlyException(User.DoesNotExist, true);

			if(databaseUser.PasswordHash != null)
				throw new KindlyException(User.PasswordAlreadyExists);

			CreatePasswordHashAndSalt(databaseUser, password);

			await this.Context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task ChangePassword(User user, string oldPassword, string newPassword)
		{
			var databaseUser = await this.Get(user.ID);
			if (databaseUser == null)
				throw new KindlyException(User.DoesNotExist, true);

			if (CheckIfPasswordMatches(databaseUser, oldPassword) == false)
				throw new KindlyException(User.PasswordIsIncorrect);

			CreatePasswordHashAndSalt(databaseUser, newPassword);

			await this.Context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<bool> UserNameExists(string userName)
		{
			return await this.Context.Users.AnyAsync(user => user.UserName == userName);
		}

		/// <inheritdoc />
		public async Task<bool> PhoneNumberExists(string phoneNumber)
		{
			return await this.Context.Users.AnyAsync(user => user.PhoneNumber == phoneNumber);
		}

		/// <inheritdoc />
		public async Task<bool> EmailAddressExists(string emailAddress)
		{
			return await this.Context.Users.AnyAsync(user => user.EmailAddress == emailAddress);
		}

		/// <inheritdoc />
		public async Task<User> GetByUserName(string userName)
		{
			return await this.Context.Users.SingleOrDefaultAsync(user => user.UserName == userName);
		}

		/// <inheritdoc />
		public async Task<User> GetByPhoneNumber(string phoneNumber)
		{
			return await this.Context.Users.SingleOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
		}

		/// <inheritdoc />
		public async Task<User> GetByEmailAddress(string emailAddress)
		{
			return await this.Context.Users.SingleOrDefaultAsync(user => user.EmailAddress == emailAddress);
		}

		#endregion

		#region [Methods] Utility
		/// <summary>
		/// Logs in the specified user.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		///
		/// <exception cref="Exception">
		/// User does not exist.
		/// or
		/// The password is incorrect.
		/// </exception>
		private async Task<User> Login(User user, string password)
		{
			var databaseUser = user != null ? await this.Get(user.ID) : null;
			if (databaseUser == null)
				throw new KindlyException(User.UserOrPasswordAreIncorrect);

			if (CheckIfPasswordMatches(databaseUser, password) == false)
				throw new KindlyException(User.UserOrPasswordAreIncorrect);

			return databaseUser;
		}

		/// <summary>
		/// Creates the password hash and salt.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		public static void CreatePasswordHashAndSalt(User user, string password)
		{
			using (var hmac = new HMACSHA512())
			{
				hmac.Initialize();
				hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

				user.PasswordHash = hmac.Hash;
				user.PasswordSalt = hmac.Key;
			}
		}

		/// <summary>
		/// Update the password hash and salt.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		public static bool CheckIfPasswordMatches(User user, string password)
		{
			using (var hmac = new HMACSHA512(user.PasswordSalt))
			{
				hmac.Initialize();
				hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

				return hmac.Hash.SequenceEqual(user.PasswordHash);
			}
		}
		#endregion
	}
}