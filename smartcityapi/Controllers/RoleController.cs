using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;
using smartcityapi.Model.pages;

namespace smartcityapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class RoleController : ControllerBase
    {
		private readonly IRoleService _Service;
		protected ApiResponse _response;
		public RoleController(IRoleService Service)
		{
			_Service = Service;
			_response = new();
		}

		[HttpPost]
		[Route("GetRoleListAsync")]
		public async Task<ActionResult<ApiResponse>> GetRoleListAsync(GetRoleRequestDTO request)
		{
			try
			{

				_response = await _Service.GetRoleListAsync(request);

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
		[Route("AddEditRoleAsync")]
		public async Task<ActionResult<ApiResponse>> AddEditRoleAsync(RoleRequestDTO req)
		{
			try
			{

				_response = await _Service.AddEditRoleAsync(req);

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
		[Route("DeleteRoleAsync")]
		public async Task<ActionResult<ApiResponse>> DeleteRoleAsync(RoleDeleteRequestDTO req)
		{
			try
			{

				_response = await _Service.DeleteRoleAsync(req);

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

