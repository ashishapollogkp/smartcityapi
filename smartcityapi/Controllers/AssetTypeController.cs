using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartcityapi.Interface;
using smartcityapi.Model.AssetType;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;

namespace smartcityapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetTypeController : ControllerBase
    {
		private readonly IAssetTypeService _Service;
		protected ApiResponse _response;
		public AssetTypeController(IAssetTypeService Service)
		{
			_Service = Service;
			_response = new();
		}

		[HttpPost]
		[Route("GetAssetTypeList")]
		public async Task<ActionResult<ApiResponse>> GetAssetTypeList(GetAssetTypeRequestDTO request)
		{
			try
			{

				_response = await _Service.GetAssetTypeList(request);

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
		[Route("CreateAssetType")]
		public async Task<ActionResult<ApiResponse>> CreateAssetType(CreateAssetTypeRequestDTO req)
		{
			try
			{

				_response = await _Service.CreateAssetType(req);

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
		[Route("UpdateAssetType")]
		public async Task<ActionResult<ApiResponse>> UpdateAssetType(UpdateAssetTypeRequestDTO req)
		{
			try
			{

				_response = await _Service.UpdateAssetType(req);

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
		[Route("DeleteAssetType")]
		public async Task<ActionResult<ApiResponse>> DeleteAssetType(DeleteAssetTypeRequestDTO req)
		{
			try
			{

				_response = await _Service.DeleteAssetType(req);

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
