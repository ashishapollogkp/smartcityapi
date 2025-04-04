using smartcityapi.Model.commanResponce;
using smartcityapi.Model.UserMgt;

namespace smartcityapi.Interface
{
	public interface ITransformerService
	{

		Task<ApiResponse> GetTransformerDashboard();
	}
}
