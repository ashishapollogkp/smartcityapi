using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.module;
using smartcityapi.Model.RoleAccess;

namespace smartcityapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class RoleAccessController : ControllerBase
    {
		private readonly IRoleAccessService _Service;
		protected ApiResponse _response;
		public RoleAccessController(IRoleAccessService Service)
		{
			_Service = Service;
			_response = new();
		}


		[HttpPost]
		[Route("GetRoleAccessListAsync")]
		public async Task<ActionResult<ApiResponse>> GetRoleAccessListAsync(GetRoleAccessRequestDTO req)
		{
			try
			{

				_response = await _Service.GetRoleAccessListAsync(req);

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
		[Route("AddEditRoleAccessAsync")]
		public async Task<ActionResult<ApiResponse>> AddEditRoleAccessAsync(RoleAccessRequestDTO req)
		{
			try
			{

				_response = await _Service.AddEditRoleAccessAsync(req);

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
		[Route("DeleteRoleAccessAsync")]
		public async Task<ActionResult<ApiResponse>> DeleteRoleAccessAsync(RoleAccessDeleteRequestDTO req)
		{
			try
			{

				_response = await _Service.DeleteRoleAccessAsync(req);

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
