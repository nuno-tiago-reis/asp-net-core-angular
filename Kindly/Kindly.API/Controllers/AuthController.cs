using AutoMapper;

using Kindly.API.Contracts.Auth;
using Kindly.API.Contracts.Users;
using Kindly.API.Models;
using Kindly.API.Models.Repositories;

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
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
	public sealed class AuthController : ControllerBase
	{
		#region [Properties]
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
		/// <param name="repository">The repository.</param>
		/// <param name="configuration">The configuration.</param>
		public AuthController(IUserRepository repository, IConfiguration configuration)
		{
			string secret = configuration.GetSection("AppSettings:Secret").Value;

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
			try
			{
				var user = new User
				{
					UserName = registerInfo.UserName,
					PhoneNumber = registerInfo.PhoneNumber,
					EmailAddress = registerInfo.EmailAddress
				};

				await this.Repository.CreateUser(user);
				await this.Repository.AddPassword(user, registerInfo.Password);

				return this.Created(new Uri($"{Request.GetDisplayUrl()}/{user.ID}"), Mapper.Map<UserDto>(user));
			}
			catch (Exception exception)
			{
				return this.BadRequest(exception.Message);
			}
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
			catch (Exception)
			{
				return this.Unauthorized();
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
			catch (Exception)
			{
				return this.Unauthorized();
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
			catch (Exception)
			{
				return this.Unauthorized();
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
			catch (Exception)
			{
				return this.Unauthorized();
			}
		}

		/// <summary>
		/// Adds a password to the user.
		/// </summary>
		/// 
		/// <param name="passwordInfo">The password information.</param>
		[HttpPost("password")]
		public async Task<IActionResult> AddPassword(AddPasswordDto passwordInfo)
		{
			try
			{
				var user = new User
				{
					ID = passwordInfo.ID
				};

				await this.Repository.AddPassword(user, passwordInfo.Password);

				return this.Ok();
			}
			catch (Exception exception)
			{
				return this.BadRequest(exception.Message);
			}
		}

		/// <summary>
		/// Changes the users password.
		/// </summary>
		/// 
		/// <param name="passwordInfo">The password information.</param>
		[HttpPut("password")]
		public async Task<IActionResult> ChangePassword(ChangePasswordDto passwordInfo)
		{
			try
			{
				var user = new User
				{
					ID = passwordInfo.ID
				};

				await this.Repository.ChangePassword(user, passwordInfo.OldPassword, passwordInfo.NewPassword);

				return this.Ok();
			}
			catch (Exception exception)
			{
				return this.BadRequest(exception.Message);
			}
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
						new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
						new Claim(ClaimTypes.Name, user.UserName),
						new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
						new Claim(ClaimTypes.Email, user.EmailAddress)
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