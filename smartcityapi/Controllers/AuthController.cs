using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.login;
using smartcityapi.Model.user;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace smartcityapi.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
		private readonly IAuthService _authService;
		protected ApiResponse _response;
		
		public AuthController(IAuthService authService)
		{
			_authService = authService;
			_response = new();

			
		}

		


		[HttpPost]
		[Route("Login")]
		public async Task<ActionResult<ApiResponse>> Login([FromBody] loginModel loginRequest)
		{			
			try
			{
				_response =  await _authService.Login(loginRequest);
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}


		[HttpGet]
		[Route("GetLoginBasedModule")]
		[Authorize]
		public async Task<ActionResult<ApiResponse>> GetLoginBasedModule()
		{
			try
			{
				_response = await _authService.GetLoginBasedModule();
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}




		[HttpPost]
		[Route("GetModuleBasedPages")]
		[Authorize]
		public async Task<ActionResult<ApiResponse>> GetModuleBasedPages(GetModulePagesDTO dto)
		{
			try
			{
				_response = await _authService.GetModuleBasedPages(dto);
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}

	}
}
