using Microsoft.AspNetCore.Identity.Data;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.login;
using smartcityapi.Model.user;

namespace smartcityapi.Interface
{
	public interface IAuthService
	{
		Task<ApiResponse> Login(loginModel loginRequest);
		

		Task<ApiResponse> GetLoginBasedModule();
		Task<ApiResponse> GetModuleBasedPages(GetModulePagesDTO dto);
	}
}
