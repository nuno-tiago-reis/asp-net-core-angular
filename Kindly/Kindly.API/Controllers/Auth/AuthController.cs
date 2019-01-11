using AutoMapper;

using Kindly.API.Contracts.Auth;
using Kindly.API.Contracts.Users;
using Kindly.API.Models.Repositories.Users;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Kindly.API.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[ServiceFilter(typeof(KindlyActivityFilter))]
	[Route("api/[controller]")]
	public sealed class AuthController : KindlyController
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the user manager.
		/// </summary>
		private UserManager<User> UserManager { get; set; }

		/// <summary>
		/// Gets or sets the sign in manager.
		/// </summary>
		private SignInManager<User> SignInManager { get; set; }

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
		/// Initializes a new instance of the <see cref="AuthController"/> class.
		/// </summary>
		/// 
		/// <param name="mapper">The mapper.</param>
		/// <param name="userManager">The user manager.</param>
		/// <param name="signInManager">The sign in manager.</param>
		/// <param name="authorizationService">The authorization service.</param>
		/// <param name="configuration">The configuration.</param>
		public AuthController
		(
			IMapper mapper,
			UserManager<User> userManager,
			SignInManager<User> signInManager,
			IAuthorizationService authorizationService,
			IConfiguration configuration
		)
		: base (mapper, authorizationService)
		{
			string secret = configuration.GetSection(KindlyConstants.AppSettingsEncryptionKey).Value;
			
			this.UserManager = userManager;
			this.SignInManager = signInManager;
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
			var user = Mapper.Map<User>(registerInfo);

			if (await this.UserManager.FindByEmailAsync(user.Email) != null)
				return this.BadRequest(user.ExistingFieldMessage(u => u.Email));

			if (await this.UserManager.FindByNameAsync(user.UserName) != null)
				return this.BadRequest(user.ExistingFieldMessage(u => u.UserName));

			var userResult = await this.UserManager.CreateAsync(user, registerInfo.Password);
			if (userResult.Succeeded)
			{
				await this.UserManager.AddToRoleAsync(user, nameof(KindlyRoles.Member));

				return this.Created(new Uri($"{Request.GetDisplayUrl()}/{user.ID}"), Mapper.Map<UserDto>(user));
			}
			else
			{
				return this.BadRequest(userResult.Errors);
			}
		}

		/// <summary>
		/// Logins the user using its user ID.
		/// </summary>
		/// 
		/// <param name="loginInfo">The login information.</param>
		[HttpPost("login/id")]
		public async Task<IActionResult> LoginWithID(LoginWithIdDto loginInfo)
		{
			var user = await this.UserManager.FindByIdAsync(loginInfo.ID.ToString());
			if (user == null)
				return this.NotFound();

			var result = await this.SignInManager.CheckPasswordSignInAsync(user, loginInfo.Password, false);
			if (result.Succeeded)
			{
				return this.Ok(new LoginResponseDto(await this.GenerateUserDto(user), await this.GenerateLoginToken(user)));
			}
			else
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
			var user = await this.UserManager.FindByNameAsync(loginInfo.UserName);
			if (user == null)
				return this.NotFound();

			var result = await this.SignInManager.CheckPasswordSignInAsync(user, loginInfo.Password, false);
			if (result.Succeeded)
			{
				return this.Ok(new LoginResponseDto(await this.GenerateUserDto(user), await this.GenerateLoginToken(user)));
			}
			else
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
		public async Task<IActionResult> LoginWithEmail(LoginWithEmailDto loginInfo)
		{
			var user = await this.UserManager.FindByEmailAsync(loginInfo.Email);
			if (user == null)
				return this.NotFound();

			var result = await this.SignInManager.CheckPasswordSignInAsync(user, loginInfo.Password, false);
			if (result.Succeeded)
			{
				return this.Ok(new LoginResponseDto(await this.GenerateUserDto(user), await this.GenerateLoginToken(user)));
			}
			else
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
			var user = await this.UserManager.FindByIdAsync(passwordInfo.ID.ToString());

			#region [Authorization]
			var result = await this.AuthorizationService.AuthorizeAsync
			(
				this.User, user, nameof(KindlyPolicies.AllowIfOwner)
			);

			if (result.Succeeded == false)
			{
				return this.Unauthorized();
			}
			#endregion

			var identityResult = await this.UserManager.AddPasswordAsync(user, passwordInfo.Password);
			if (identityResult.Succeeded)
			{
				return this.Ok(new LoginResponseDto(await this.GenerateUserDto(user), await this.GenerateLoginToken(user)));
			}
			else
			{
				return this.Unauthorized();
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
			var user = await this.UserManager.FindByIdAsync(passwordInfo.ID.ToString());

			#region [Authorization]
			var result = await this.AuthorizationService.AuthorizeAsync
			(
				this.User, user, nameof(KindlyPolicies.AllowIfOwner)
			);

			if (result.Succeeded == false)
			{
				return this.Unauthorized();
			}
			#endregion

			var identityResult = await this.UserManager.ChangePasswordAsync(user, passwordInfo.OldPassword, passwordInfo.NewPassword);
			if (identityResult.Succeeded)
			{
				return this.Ok(new LoginResponseDto(await this.GenerateUserDto(user), await this.GenerateLoginToken(user)));
			}
			else
			{
				return this.Unauthorized();
			}
		}
		#endregion

		#region [Utility Methods]
		/// <summary>
		/// Generates a user dto for the user.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		private async Task<UserDetailedDto> GenerateUserDto(User user)
		{
			var databaseUser = await this.UserManager.Users
				.Include(u => u.Pictures)
				.Include(u => u.LikeSenders)
				.Include(u => u.LikeRecipients)
				.Include(u => u.UserRoles)
				.FirstOrDefaultAsync(u => u.UserName == user.UserName);
			var databaseUserDto = Mapper.Map<UserDetailedDto>(databaseUser);

			return databaseUserDto;
		}

		/// <summary>
		/// Generates a login token for the user.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		private async Task<string> GenerateLoginToken(User user)
		{
			var roles = await this.UserManager.GetRolesAsync(user);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
			};

			foreach (string role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = this.SigningCredentials
			};

			return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
		}
		#endregion
	}
}