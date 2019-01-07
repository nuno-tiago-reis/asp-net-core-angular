using Kindly.API.Utility;
using Kindly.API.Utility.Collections;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Kindly.API.Models.Repositories.Users
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

			if (string.IsNullOrWhiteSpace(user.Email))
				throw new KindlyException(user.InvalidFieldMessage(u => u.Email));
			if (await this.EmailAddressExists(user.Email))
				throw new KindlyException(user.ExistingFieldMessage(u => u.Email));

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

			if (!string.IsNullOrWhiteSpace(user.Email) && databaseUser.Email != user.Email && await this.EmailAddressExists(user.Email))
				throw new KindlyException(user.ExistingFieldMessage(u => u.Email));

			databaseUser.PhoneNumber =
				!string.IsNullOrWhiteSpace(user.PhoneNumber) ? user.PhoneNumber : databaseUser.PhoneNumber;

			databaseUser.Email =
				!string.IsNullOrWhiteSpace(user.Email) ? user.Email : databaseUser.Email;

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

			// Update
			await this.Context.SaveChangesAsync();

			return databaseUser;
		}

		/// <inheritdoc />
		public async Task Delete(Guid userID)
		{
			var user = await this.Context.Users.FindAsync(userID);
			if (user == null)
				throw new KindlyException(User.DoesNotExist, true);

			// Delete
			this.Context.Users.Remove(user);

			await this.Context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<User> Get(Guid userID)
		{
			return await this.GetQueryable().SingleOrDefaultAsync(user => user.ID == userID);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<User>> GetAll()
		{
			return await this.GetQueryable().ToListAsync();
		}

		/// <inheritdoc />
		public async Task<PagedList<User>> GetAll(UserParameters parameters)
		{
			var users = this.GetQueryable();

			if (parameters.Gender.HasValue == false)
			{
				var user = await this.Context.Users.FindAsync(parameters.UserID);

				switch (user.Gender)
				{
					case Gender.Female:
						parameters.Gender = Gender.Male;
						break;

					case Gender.Male:
						parameters.Gender = Gender.Female;
						break;

					default:
						throw new ArgumentOutOfRangeException(nameof(user.Gender));
				}
			}

			if (parameters.MinimumAge.HasValue)
			{
				var maximumBirthDate = DateTime.Today.AddYears(-parameters.MinimumAge.Value);

				users = users.Where(u => u.BirthDate <= maximumBirthDate);
			}

			if (parameters.MaximumAge.HasValue)
			{
				var minimumBirthDate = DateTime.Today.AddYears(-parameters.MaximumAge.Value-1);

				users = users.Where(u => u.BirthDate >= minimumBirthDate);
			}

			if (string.IsNullOrWhiteSpace(parameters.OrderBy) == false)
			{
				if (parameters.OrderBy == nameof(User.CreatedAt).ToLowerCamelCase())
				{
					users = users.OrderByDescending(u => u.CreatedAt);
				}
				else if (parameters.OrderBy == nameof(User.LastActiveAt).ToLowerCamelCase())
				{
					users = users.OrderByDescending(u => u.LastActiveAt);
				}
				else
				{
					throw new ArgumentOutOfRangeException(nameof(parameters.OrderBy), parameters.OrderBy, null);
				}
			}

			users = users.Where(u => u.ID != parameters.UserID);
			users = users.Where(u => u.Gender == parameters.Gender);

			return await PagedList<User>.CreateAsync(users, parameters.PageNumber, parameters.PageSize);
		}
		#endregion

		#region [Methods] IUserRepository
		/// <inheritdoc />
		public async Task<bool> UserNameExists(string userName)
		{
			return await this.Context.Users.AnyAsync(u => u.UserName == userName);
		}

		/// <inheritdoc />
		public async Task<bool> PhoneNumberExists(string phoneNumber)
		{
			return await this.Context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
		}

		/// <inheritdoc />
		public async Task<bool> EmailAddressExists(string emailAddress)
		{
			return await this.Context.Users.AnyAsync(u => u.Email == emailAddress);
		}

		/// <inheritdoc />
		public async Task<User> GetByUserName(string userName)
		{
			return await this.Context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
		}

		/// <inheritdoc />
		public async Task<User> GetByPhoneNumber(string phoneNumber)
		{
			return await this.Context.Users.SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
		}

		/// <inheritdoc />
		public async Task<User> GetByEmailAddress(string emailAddress)
		{
			return await this.Context.Users.SingleOrDefaultAsync(u => u.Email == emailAddress);
		}
		#endregion

		#region [Methods] Utility
		/// <summary>
		/// Gets the users queryable.
		/// </summary>
		private IQueryable<User> GetQueryable()
		{
			return this.Context.Users
				.Include(u => u.Pictures)
				.Include(u => u.LikeTargets)
				.Include(u => u.LikeSources)
				.OrderByDescending(u => u.LastActiveAt);
		}
		#endregion
	}
}