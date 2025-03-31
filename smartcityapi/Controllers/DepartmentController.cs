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
	public class DepartmentController : ControllerBase
    {
		private readonly IDepartmentService _Service;
		protected ApiResponse _response;
		public DepartmentController(IDepartmentService Service)
		{
			_Service = Service;
			_response = new();
		}

		[HttpGet]
		[Route("GetDepartmentListAsync")]
		public async Task<ActionResult<ApiResponse>> GetDepartmentListAsync()
		{
			try
			{

				_response = await _Service.GetDepartmentListAsync();

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
		[Route("AddEditDepartmentAsync")]
		public async Task<ActionResult<ApiResponse>> AddEditDepartmentAsync(DepartmentRequestDTO req)
		{
			try
			{

				_response = await _Service.AddEditDepartmentAsync(req);

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
		[Route("DeleteDepartmentAsync")]
		public async Task<ActionResult<ApiResponse>> DeleteDepartmentAsync(DepertmentDeleteRequestDTO req)
		{
			try
			{

				_response = await _Service.DeleteDepartmentAsync(req);

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
