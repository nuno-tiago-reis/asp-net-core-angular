using Kindly.API.Models.Repositories.Users;
using Kindly.API.Utility;
using Kindly.API.Utility.Collections;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;
using System.Linq;

namespace Kindly.API.Models.Repositories.Pictures
{
	public sealed class PictureRepository : IPictureRepository
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public KindlyContext Context { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="PictureRepository"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>
		public PictureRepository(KindlyContext context)
		{
			this.Context = context;
		}
		#endregion

		#region [Methods] IEntityRepository
		/// <inheritdoc />
		public async Task<Picture> Create(Picture picture)
		{
			// Properties
			if (string.IsNullOrWhiteSpace(picture.Url))
				throw new KindlyException(picture.InvalidFieldMessage(p => p.Url));

			if (string.IsNullOrWhiteSpace(picture.PublicID))
				throw new KindlyException(picture.InvalidFieldMessage(p => p.PublicID));

			// Foreign Keys
			var user = await this.Context.Users.FindAsync(picture.UserID);
			if (user == null)
				throw new KindlyException(User.DoesNotExist, true);

			// Profile picture
			var profilePicture = await this.GetProfilePicture(picture.UserID);

			if (picture.IsProfilePicture.HasValue && picture.IsProfilePicture.Value)
			{
				// Don't allow the indicator to be 'removed'
				// The only way to 'remove' a profile picture is to add a new one
				if (profilePicture != null)
					profilePicture.IsProfilePicture = false;

				picture.IsProfilePicture = true;
			}
			else
			{
				if (profilePicture == null)
					picture.IsProfilePicture = true;
			}

			// Create
			this.Context.Add(picture);

			await this.Context.SaveChangesAsync();

			return picture;
		}

		/// <inheritdoc />
		public async Task<Picture> Update(Picture picture)
		{
			var databasePicture = await this.Context.Pictures.FindAsync(picture.ID);
			if (databasePicture == null)
				throw new KindlyException(Picture.DoesNotExist, true);

			// Properties
			databasePicture.Description =
				!string.IsNullOrWhiteSpace(picture.Description) ? picture.Description : databasePicture.Description;

			// Profile picture
			if (picture.IsProfilePicture.HasValue && picture.IsProfilePicture.Value)
			{
				// Don't allow the indicator to be 'removed'
				// The only way to 'remove' a profile picture is to add a new one
				var profilePicture = await this.GetProfilePicture(picture.UserID);
				if (profilePicture != null)
					profilePicture.IsProfilePicture = false;

				databasePicture.IsProfilePicture = true;
			}

			databasePicture.IsApproved =
				picture.IsApproved ?? databasePicture.IsApproved;

			// Update
			await this.Context.SaveChangesAsync();

			return databasePicture;
		}

		/// <inheritdoc />
		public async Task Delete(Guid pictureID)
		{
			var picture = await this.Context.Pictures.FindAsync(pictureID);
			if (picture == null)
				throw new KindlyException(Picture.DoesNotExist, true); 
			if (picture.IsProfilePicture.HasValue && picture.IsProfilePicture.Value)
				throw new KindlyException(Picture.CannotDeleteTheProfilePicture);

			// Delete
			this.Context.Pictures.Remove(picture);

			await this.Context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<Picture> Get(Guid pictureID)
		{
			return await this.GetQueryable().SingleOrDefaultAsync(p => p.ID == pictureID);
		}

		/// <inheritdoc />
		public async Task<PagedList<Picture>> GetAll(PictureParameters parameters = null)
		{
			var pictures = this.GetQueryable();

			if (parameters != null)
			{
				switch (parameters.Container)
				{
					case PictureContainer.Approved:
						pictures = pictures
							.Where(p => p.IsApproved.Value);
						break;

					case PictureContainer.Unapproved:
						pictures = pictures
							.Where(p => p.IsApproved.Value == false);
						break;

					case PictureContainer.Everything:
						break;

					default:
						throw new ArgumentOutOfRangeException(nameof(parameters.Container), parameters.Container, null);
				}

				if (string.IsNullOrWhiteSpace(parameters.OrderBy) == false)
				{
					if (parameters.OrderBy == nameof(Picture.CreatedAt).ToLowerCamelCase())
					{
						pictures = pictures.OrderByDescending(p => p.CreatedAt);
					}
					else
					{
						throw new ArgumentOutOfRangeException(nameof(parameters.OrderBy), parameters.OrderBy, null);
					}
				}
			}
			else
			{
				parameters = new PictureParameters();
			}

			return await PagedList<Picture>.CreateAsync(pictures, parameters.PageNumber, parameters.PageSize);
		}
		#endregion

		#region [Methods] IPictureRepository
		/// <inheritdoc />
		public async Task<bool> PictureBelongsToUser(Guid userID, Guid pictureID)
		{
			bool exists = await this.Context.Pictures
				.AnyAsync(p => p.ID == pictureID && p.UserID == userID);

			if (exists == false)
				throw new KindlyException(Picture.DoesNotExist, true);

			return true;
		}

		/// <inheritdoc />
		public async Task<PagedList<Picture>> GetByUser(Guid userID, PictureParameters parameters)
		{
			var pictures = this.GetQueryableByUser(userID);

			return await PagedList<Picture>.CreateAsync(pictures, parameters.PageNumber, parameters.PageSize);
		}
		#endregion

		#region [Methods] Utility
		/// <summary>
		/// Gets the queryable.
		/// </summary>
		private IQueryable<Picture> GetQueryable()
		{
			return this.Context.Pictures
				.Include(p => p.User)
				.OrderByDescending(p => p.CreatedAt);
		}

		/// <summary>
		/// Gets the queryable by user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		private IQueryable<Picture> GetQueryableByUser(Guid userID)
		{
			return this.GetQueryable()
				.Where(p => p.UserID == userID);
		}

		/// <summary>
		/// Gets the profile picture.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		private async Task<Picture> GetProfilePicture(Guid userID)
		{
			var profilePicture = await this.GetQueryable()
				.SingleOrDefaultAsync(p => p.UserID == userID && p.IsProfilePicture.Value);

			return profilePicture;
		}
		#endregion
	}
}