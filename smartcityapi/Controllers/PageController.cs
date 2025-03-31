using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.module;
using smartcityapi.Model.pages;

namespace smartcityapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class PageController : ControllerBase
    {
		private readonly IPageService _Service;
		protected ApiResponse _response;
		public PageController(IPageService Service)
		{
			_Service = Service;
			_response = new();
		}

		[HttpPost]
		[Route("GetPageListAsync")]
		public async Task<ActionResult<ApiResponse>> GetPageListAsync(GetPagesRequestDTO request)
		{
			try
			{

				_response = await _Service.GetPageListAsync(request);

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
		[Route("AddEditPageAsync")]
		public async Task<ActionResult<ApiResponse>> AddEditPageAsync(PagesRequestDTO req)
		{
			try
			{

				_response = await _Service.AddEditPageAsync(req);

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
		[Route("DeletePageAsync")]
		public async Task<ActionResult<ApiResponse>> DeletePageAsync(PageDeleteRequestDTO req)
		{
			try
			{

				_response = await _Service.DeletePageAsync(req);

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
