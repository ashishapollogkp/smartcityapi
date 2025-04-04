using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;
using smartcityapi.Model.UserMgt;

namespace smartcityapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class UserController : ControllerBase
    {
        private readonly IUserMgtService _Service;
		protected ApiResponse _response;
		public UserController(IUserMgtService Service)
		{
			_Service = Service;
			_response = new();
		}


		[HttpPost]
		[Route("CreateUserAsync")]
		public async Task<ActionResult<ApiResponse>> CreateUserAsync(CreateUserRequestDTO req)
		{
			try
			{

				_response = await _Service.CreateUserAsync(req);

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
		[Route("UpdateUserAsync")]
		public async Task<ActionResult<ApiResponse>> UpdateUserAsync(UpdateUserRequestDTO req)
		{
			try
			{

				_response = await _Service.UpdateUserAsync(req);

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
		[Route("DeleteUserAsync")]
		public async Task<ActionResult<ApiResponse>> DeleteUserAsync(DeleteUserRequestDTO req)
		{
			try
			{

				_response = await _Service.DeleteUserAsync(req);

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
		[Route("StatusUserAsync")]
		public async Task<ActionResult<ApiResponse>> StatusUserAsync(StatusUserRequestDTO req)
		{
			try
			{

				_response = await _Service.StatusUserAsync(req);

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
		[Route("GetUserListAsync")]
		public async Task<ActionResult<ApiResponse>> GetUserListAsync(GetUserListRequestDTO req)
		{
			try
			{

				_response = await _Service.GetUserListAsync(req);

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
