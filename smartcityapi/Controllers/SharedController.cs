using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.shared;

namespace smartcityapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class SharedController : ControllerBase
    {
		private readonly ISharedService _Service;
		protected ApiResponse _response;
		public SharedController(ISharedService Service)
		{
			_Service = Service;
			_response = new();
		}


		[HttpGet]
		[Route("GetModuleList")]
		public async Task<ActionResult<ApiResponse>> GetModuleList()
		{
			try
			{

				_response = await _Service.GetModuleList();

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
		[Route("GetDepartmentList")]
		public async Task<ActionResult<ApiResponse>> GetDepartmentList()
		{
			try
			{

				_response = await _Service.GetDepartmentList();

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
		[Route("GetRoleList")]
		public async Task<ActionResult<ApiResponse>> GetRoleList()
		{
			try
			{

				_response = await _Service.GetRoleList();

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
		[Route("GetLevelList")]
		public async Task<ActionResult<ApiResponse>> GetLevelList()
		{
			try
			{

				_response = await _Service.GetLevelList();

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
		[Route("GetAssetTypeList")]
		public async Task<ActionResult<ApiResponse>> GetAssetTypeList()
		{
			try
			{

				_response = await _Service.GetAssetTypeList();

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
		[Route("GetRoleListByDept")]
		public async Task<ActionResult<ApiResponse>> GetRoleListByDept(GetDepartmentWiseRequestDTO request)
		{
			try
			{

				_response = await _Service.GetRoleListByDept(request);

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
		[Route("GetModuleListByDept")]
		public async Task<ActionResult<ApiResponse>> GetModuleListByDept(GetDepartmentWiseRequestDTO request)
		{
			try
			{

				_response = await _Service.GetModuleListByDept(request);

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
