using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using smartcityapi.Context;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.login;
using smartcityapi.Model.user;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace smartcityapi.Services
{
	public class AuthService: IAuthService
	{
		private readonly SmartCityDBContext _jwtContext;
		private readonly IConfiguration _configuration;
		protected ApiResponse _response;

		private readonly UserService _userService;

		public AuthService(SmartCityDBContext jwtContext, IConfiguration configuration, UserService userService)
		{
			_jwtContext = jwtContext;
			_configuration = configuration;

			_response = new();
			_userService = userService;
		}

		public async Task<ApiResponse> Login(loginModel loginRequest)
		{
			ApiResponse res = new ApiResponse();


			try
			{
				if (string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
				{
					//throw new ArgumentException("Username and password cannot be empty.");

					res.ActionResponse = "Invalid username or password.";
					res.StatusCode = HttpStatusCode.NotFound;
					res.IsSuccess = false;
					return res;

				}

				var emp = await _jwtContext.master_user
					.SingleOrDefaultAsync(s => s.login_id == loginRequest.Username && s.is_deleted == 0);//&& s.is_active == 1

				if (emp == null)
				{
					//throw new UnauthorizedAccessException("Invalid username or password.");

					res.ActionResponse = "Invalid username or password.";
					res.StatusCode = HttpStatusCode.NotFound;
					res.IsSuccess = false;
					return res;

				}
				if (emp.is_active == 0)
				{
					res.ActionResponse = "user Deactivated !";
					res.StatusCode = HttpStatusCode.NotFound;
					res.IsSuccess = false;
					return res;

				}


				// Encrypt input password for comparison
				string encryptedPassword = CryptorEngine.Encrypt(loginRequest.Password, true);

				if (emp.password != loginRequest.Password)
				{
					//throw new UnauthorizedAccessException("Invalid username or password.");



					res.ActionResponse = "Invalid username or password.";
					res.StatusCode = HttpStatusCode.NotFound;
					res.IsSuccess = false;



					return res;

				}

				var claims = new List<Claim>
						{
							new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
							new Claim("Id", emp.pk_user_id.ToString()),
							new Claim("UserName", emp.user_name),
							new Claim("Email", emp.email),
							new Claim("RoleId", emp.fk_role_id.ToString()),
								new Claim("DeptId", emp.fk_department_id.ToString())
						};

				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
				var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

				var tokenDescriptor = new JwtSecurityToken(
					_configuration["Jwt:Issuer"],
					_configuration["Jwt:Audience"],
					claims,
					expires: DateTime.UtcNow.AddDays(15),
					signingCredentials: credentials
				);

				//return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

				string generate_token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

				res.Result = generate_token;
				res.StatusCode = HttpStatusCode.OK;
				res.IsSuccess = true;
				return res;


			}
			catch
			{



				//return "Invalid username or password.";

				res.ActionResponse = "Invalid username or password.";
				res.StatusCode = HttpStatusCode.NotFound;
				res.IsSuccess = false;



				return res;
			}
		}


		public async Task<ApiResponse> GetLoginBasedModule()
		{
			ApiResponse res = new ApiResponse();


			var user_Id = _userService.GetUserId();
			var user_role_Id = _userService.GetUserRoleId();




			try
			{
				var modules = await (from module in _jwtContext.master_module
									 where _jwtContext.role_permissions
										   .Where(rp => rp.fk_role_id == user_role_Id )
										   .Select(rp => rp.fk_module_id)
										   .Contains(module.pk_module_id)
									 select new
									 {
										 module.pk_module_id,
										 module.module_name,
										 module.module_url,
										 module.module_icon,
										 module.module_order,
										 module.is_active,
										 module.is_deleted
									 }).ToListAsync();

				if (modules.Any())
				{

					res.Result = modules;
					res.StatusCode = HttpStatusCode.OK;
					res.IsSuccess = true;
					return res;
				}
				else
				{
					res.ActionResponse = "No Module found!";
					res.StatusCode = HttpStatusCode.NotFound;
					res.IsSuccess = false;
					return res;
				}


			}
			catch (Exception ex)
			{
				res.ActionResponse = "No Module found!";
				res.StatusCode = HttpStatusCode.NotFound;
				res.IsSuccess = false;
				return res;

			}
		}

		public async Task<ApiResponse> GetModuleBasedPages(GetModulePagesDTO dto)
		{
			ApiResponse res = new ApiResponse();

			var user_Id = _userService.GetUserId();
			var user_role_Id = _userService.GetUserRoleId();

			try
			{
				var permissions = (from rp in _jwtContext.role_permissions
								   join page in _jwtContext.master_module_pages
								   on rp.fk_page_id equals page.pk_page_id into pageJoin
								   from p in pageJoin.DefaultIfEmpty() // Left Join
								   where rp.fk_role_id == user_role_Id && rp.fk_module_id==dto.module_id
								   select new
								   {
									   rp.fk_role_id,
									   rp.fk_module_id,
									   rp.fk_page_id,
									   rp.is_view,
									   rp.is_add,
									   rp.is_update,
									   rp.is_delete,
									   rp.is_export,
									   PageName = p.page_name,
									   PageIcon = p.page_icon,
									   PageUrl = p.page_url,
									   PageOrder = p.page_order
								   }).ToList();


				if (permissions.Any())
				{

					res.Result = permissions;
					res.StatusCode = HttpStatusCode.OK;
					res.IsSuccess = true;
					return res;
				}
				else
				{
					res.ActionResponse = "No Module found!";
					res.StatusCode = HttpStatusCode.NotFound;
					res.IsSuccess = false;
					return res;
				}


			}
			catch (Exception ex)
			{
				res.ActionResponse = "No Module found!";
				res.StatusCode = HttpStatusCode.NotFound;
				res.IsSuccess = false;
				return res;

			}
		}


	}
}

