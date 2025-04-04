using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.pages;

namespace smartcityapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransformerController : ControllerBase
    {
		private readonly ITransformerService _Service;
		protected ApiResponse _response;
		public TransformerController(ITransformerService Service)
		{
			_Service = Service;
			_response = new();
		}

		[HttpGet]
		[Route("GetTransformerDashboard")]
		public async Task<ActionResult<ApiResponse>> GetTransformerDashboard()
		{
			try
			{

				_response = await _Service.GetTransformerDashboard();

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
