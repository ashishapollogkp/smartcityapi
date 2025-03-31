using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.DepartmentAccess;
using smartcityapi.Model.RoleAccess;

namespace smartcityapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentAccessController : ControllerBase
    {
		private readonly IDepartmentAccessService _Service;
		protected ApiResponse _response;
		public DepartmentAccessController(IDepartmentAccessService Service)
		{
			_Service = Service;
			_response = new();
		}


		[HttpPost]
		[Route("GetDepartmentAccessListAsync")]
		public async Task<ActionResult<ApiResponse>> GetDepartmentAccessListAsync(GetDepartmentAccessRequestDTO req)
		{
			try
			{

				_response = await _Service.GetDepartmentAccessListAsync(req);

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
		[Route("AddEditDepartmentAccessAsync")]
		public async Task<ActionResult<ApiResponse>> AddEditDepartmentAccessAsync(DepartmentAccessRequestDTO req)
		{
			try
			{

				_response = await _Service.AddEditDepartmentAccessAsync(req);

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
		[Route("DeleteDepartmentAccessAsync")]
		public async Task<ActionResult<ApiResponse>> DeleteDepartmentAccessAsync(DepartmentAccessDeleteRequestDTO req)
		{
			try
			{

				_response = await _Service.DeleteDepartmentAccessAsync(req);

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
