using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Kindly.API.Contracts;
using Kindly.API.Contracts.Pictures;
using Kindly.API.Models.Domain;
using Kindly.API.Utility;

namespace Kindly.API.Models.Repositories
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
			var databaseUser = await this.Context.Users.FindAsync(picture.UserID);
			if (databaseUser == null)
				throw new KindlyException(User.DoesNotExist, true);

			if (picture.IsProfilePicture)
			{
				// Don't allow the indicator to be 'removed'
				// The only way to 'remove' a profile picture is to add a new one
				var profilePicture = await this.Context.Pictures.SingleOrDefaultAsync(p => p.UserID == picture.UserID && p.IsProfilePicture);
				if (profilePicture != null)
					profilePicture.IsProfilePicture = false;

				picture.IsProfilePicture = true;
			}
			else
			{
				if (await this.Context.Pictures.AnyAsync(p => p.UserID == picture.UserID) == false)
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
			databasePicture.Url =
				!string.IsNullOrWhiteSpace(picture.Url) ? picture.Url : databasePicture.Url;

			databasePicture.PublicID =
				!string.IsNullOrWhiteSpace(picture.PublicID) ? picture.PublicID : databasePicture.PublicID;

			databasePicture.Description =
				!string.IsNullOrWhiteSpace(picture.Description) ? picture.Description : databasePicture.Description;

			if (picture.IsProfilePicture)
			{
				// Don't allow the indicator to be 'removed'
				// The only way to 'remove' a profile picture is to add a new one
				var profilePicture = await this.Context.Pictures.SingleOrDefaultAsync(p => p.UserID == picture.UserID && p.IsProfilePicture);
				if (profilePicture != null)
					profilePicture.IsProfilePicture = false;

				databasePicture.IsProfilePicture = true;
			}

			// Update
			await this.Context.SaveChangesAsync();

			return databasePicture;
		}

		/// <inheritdoc />
		public async Task Delete(Guid pictureID)
		{
			var databasePicture = await this.Context.Pictures.FindAsync(pictureID);
			if (databasePicture == null)
				throw new KindlyException(Picture.DoesNotExist, true); 
			if (databasePicture.IsProfilePicture)
				throw new KindlyException(Picture.CannotDeleteTheProfilePicture);

			// Delete
			this.Context.Pictures.Remove(databasePicture);
			await this.Context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<Picture> Get(Guid pictureID)
		{
			return await this.Context.Pictures.FindAsync(pictureID);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Picture>> GetAll()
		{
			return await this.Context.Pictures.ToListAsync();
		}

		/// <inheritdoc />
		public async Task<PagedList<Picture>> GetAll(PictureParameters parameters)
		{
			var pictures = this.Context.Pictures;

			return await PagedList<Picture>.CreateAsync(pictures.OrderBy(p => p.AddedAt), parameters.PageNumber, parameters.PageSize);
		}
		#endregion

		#region [Methods] IPictureRepository
		/// <inheritdoc />
		public async Task<bool> PictureBelongsToUser(Guid userID, Guid pictureID)
		{
			var databaseUser = await this.Context.Users.FindAsync(userID);
			if (databaseUser == null)
				throw new KindlyException(User.DoesNotExist, true);

			var databasePicture = await this.Context.Pictures.SingleOrDefaultAsync(picture => picture.ID == pictureID && picture.UserID == userID);
			if (databasePicture == null)
				throw new KindlyException(Picture.DoesNotExist, true);

			return true;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Picture>> GetByUser(Guid userID)
		{
			return await this.Context.Pictures.Where(picture => picture.UserID == userID).ToListAsync();
		}
		#endregion
	}
}