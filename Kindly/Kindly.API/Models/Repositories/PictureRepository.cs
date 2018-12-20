using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Kindly.API.Models.Domain;
using Kindly.API.Utility;

using Microsoft.EntityFrameworkCore;

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

			if (string.IsNullOrWhiteSpace(picture.Description))
				throw new KindlyException(picture.InvalidFieldMessage(p => p.Description));

			// Foreign Keys
			var databaseUser = await this.Context.Users.FindAsync(picture.UserID);
			if (databaseUser == null)
				throw new KindlyException(User.DoesNotExist, true);

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

			databasePicture.Description =
				!string.IsNullOrWhiteSpace(picture.Description) ? picture.Description : databasePicture.Description;

			databasePicture.IsProfilePicture =
				picture.IsProfilePicture ?? databasePicture.IsProfilePicture;

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
		#endregion
	}
}