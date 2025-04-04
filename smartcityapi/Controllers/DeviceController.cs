using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartcityapi.Interface;
using smartcityapi.Model.AssetType;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Device;

namespace smartcityapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class DeviceController : ControllerBase
    {
		private readonly IDeviceService _Service;
		protected ApiResponse _response;
		public DeviceController(IDeviceService Service)
		{
			_Service = Service;
			_response = new();
		}

		[HttpPost]
		[Route("CreateDevice")]
		public async Task<ActionResult<ApiResponse>> CreateDevice(CreateDeviceRequestDTO req)
		{
			try
			{

				_response = await _Service.CreateDevice(req);

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
		[Route("GetDeviceList")]
		public async Task<ActionResult<ApiResponse>> GetDeviceList(GetDeviceRequestDTO request)
		{
			try
			{

				_response = await _Service.GetDeviceList(request);

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
		[Route("UpdateDevice")]
		public async Task<ActionResult<ApiResponse>> UpdateDevice(UpdateDeviceRequestDTO req)
		{
			try
			{

				_response = await _Service.UpdateDevice(req);

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
		[Route("DeleteDevice")]
		public async Task<ActionResult<ApiResponse>> DeleteDevice(DeleteDeviceRequestDTO req)
		{
			try
			{

				_response = await _Service.DeleteDevice(req);

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
