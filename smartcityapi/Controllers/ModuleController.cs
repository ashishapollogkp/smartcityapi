using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.module;

namespace smartcityapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
		

		private readonly IModuleService _Service;
		protected ApiResponse _response;
		public ModuleController(IModuleService Service)
		{
			_Service = Service;
			_response = new();
		}


		[HttpPost]
		[Route("CreateModuleAsync")]
		public async Task<ActionResult<ApiResponse>> CreateModuleAsync(ModuleAddRequestDTO req)
		{
			try
			{

				_response = await _Service.CreateModuleAsync(req);

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
		[Route("UpdateModuleAsync")]
		public async Task<ActionResult<ApiResponse>> UpdateModuleAsync(ModuleEditRequestDTO req)
		{
			try
			{

				_response = await _Service.UpdateModuleAsync(req);

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
		[Route("DeleteModuleAsync")]
		public async Task<ActionResult<ApiResponse>> DeleteModuleAsync(ModuleDeleteRequestDTO req)
		{
			try
			{

				_response = await _Service.DeleteModuleAsync(req);

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
		[Route("GetModuleListAsync")]
		public async Task<ActionResult<ApiResponse>> GetModuleListAsync()
		{
			try
			{

				_response = await _Service.GetModuleListAsync();

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
