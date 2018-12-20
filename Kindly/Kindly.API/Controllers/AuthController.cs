using AutoMapper;

using Kindly.API.Contracts.Auth;
using Kindly.API.Contracts.Users;
using Kindly.API.Models.Domain;
using Kindly.API.Models.Repositories;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Kindly.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public sealed class AuthController : KindlyController
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the mapper.
		/// </summary>
		private IMapper Mapper { get; set; }

		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		private IUserRepository Repository { get; set; }

		/// <summary>
		/// Gets or sets the security key.
		/// </summary>
		private SymmetricSecurityKey SecurityKey { get; set; }

		/// <summary>
		/// Gets or sets the signing credentials.
		/// </summary>
		private SigningCredentials SigningCredentials { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="UsersController"/> class.
		/// </summary>
		/// 
		/// <param name="mapper">The mapper.</param>
		/// <param name="repository">The repository.</param>
		/// <param name="configuration">The configuration.</param>
		public AuthController(IMapper mapper, IUserRepository repository, IConfiguration configuration)
		{
			string secret = configuration.GetSection(KindlyConstants.AppSettingsEncryptionKey).Value;

			this.Mapper = mapper;
			this.Repository = repository;
			this.SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
			this.SigningCredentials = new SigningCredentials(this.SecurityKey, SecurityAlgorithms.HmacSha512Signature);
		}
		#endregion

		#region [Interface Methods]
		/// <summary>
		/// Registers the specified user.
		/// </summary>
		/// 
		/// <param name="registerInfo">The register information.</param>
		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto registerInfo)
		{
			var user = await this.Repository.Create(Mapper.Map<User>(registerInfo));
			await this.Repository.AddPassword(user, registerInfo.Password);

			return this.Created(new Uri($"{Request.GetDisplayUrl()}/{user.ID}"), Mapper.Map<UserDto>(user));
		}

		/// <summary>
		/// Logins the user using its user ID.
		/// </summary>
		/// 
		/// <param name="loginInfo">The login information.</param>
		[HttpPost("login/id")]
		public async Task<IActionResult> LoginWithID(LoginWithUserIdDto loginInfo)
		{
			try
			{
				var user = await this.Repository.LoginWithID(loginInfo.ID, loginInfo.Password);

				return this.Ok(new { token = this.GenerateLoginToken(user) });
			}
			catch (KindlyException exception)
			{
				exception.StatusCode = StatusCodes.Status401Unauthorized;

				throw;
			}
		}

		/// <summary>
		/// Logins the user using its user name.
		/// </summary>
		/// 
		/// <param name="loginInfo">The login information.</param>
		[HttpPost("login/user-name")]
		public async Task<IActionResult> LoginWithUserName(LoginWithUserNameDto loginInfo)
		{
			try
			{
				var user = await this.Repository.LoginWithUserName(loginInfo.UserName, loginInfo.Password);

				return this.Ok(new { token = this.GenerateLoginToken(user) });
			}
			catch (KindlyException exception)
			{
				exception.StatusCode = StatusCodes.Status401Unauthorized;

				throw;
			}
		}

		/// <summary>
		/// Logins the user using its phone number.
		/// </summary>
		/// 
		/// <param name="loginInfo">The login information.</param>
		[HttpPost("login/phone-number")]
		public async Task<IActionResult> LoginWithPhoneNumber(LoginWithPhoneNumberDto loginInfo)
		{
			try
			{
				var user = await this.Repository.LoginWithPhoneNumber(loginInfo.PhoneNumber, loginInfo.Password);

				return this.Ok(new { token = this.GenerateLoginToken(user) });
			}
			catch (KindlyException exception)
			{
				exception.StatusCode = StatusCodes.Status401Unauthorized;

				throw;
			}
		}

		/// <summary>
		/// Logins the user using its email address.
		/// </summary>
		/// 
		/// <param name="loginInfo">The login information.</param>
		[HttpPost("login/email-address")]
		public async Task<IActionResult> LoginWithEmailAddress(LoginWithEmailAddressDto loginInfo)
		{
			try
			{
				var user = await this.Repository.LoginWithEmailAddress(loginInfo.EmailAddress, loginInfo.Password);

				return this.Ok(new { token = this.GenerateLoginToken(user)});
			}
			catch (KindlyException exception)
			{
				exception.StatusCode = StatusCodes.Status401Unauthorized;

				throw;
			}
		}

		/// <summary>
		/// Adds a password to the user.
		/// </summary>
		/// 
		/// <param name="passwordInfo">The password information.</param>
		[Authorize]
		[HttpPost("password")]
		public async Task<IActionResult> AddPassword(AddPasswordDto passwordInfo)
		{
			if (passwordInfo.ID != this.GetInvocationUserID())
				return this.Unauthorized();

			var user = new User
			{
				ID = passwordInfo.ID
			};

			await this.Repository.AddPassword(user, passwordInfo.Password);

			return this.Ok();
		}

		/// <summary>
		/// Changes the users password.
		/// </summary>
		/// 
		/// <param name="passwordInfo">The password information.</param>
		[Authorize]
		[HttpPut("password")]
		public async Task<IActionResult> ChangePassword(ChangePasswordDto passwordInfo)
		{
			if (passwordInfo.ID != this.GetInvocationUserID())
				return this.Unauthorized();

			var user = new User
			{
				ID = passwordInfo.ID
			};

			await this.Repository.ChangePassword(user, passwordInfo.OldPassword, passwordInfo.NewPassword);

			return this.Ok();
		}
		#endregion

		#region [Utility Methods]
		/// <summary>
		/// Generates a login token for the user.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		private string GenerateLoginToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity
				(
					new[]
					{
						new Claim(KindlyClaimTypes.ID.ToString().ToLowerCamelCase(), user.ID.ToString()),
						new Claim(KindlyClaimTypes.ProfileName.ToString().ToLowerCamelCase(), user.KnownAs)
					}
				),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = this.SigningCredentials
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
		#endregion
	}
}