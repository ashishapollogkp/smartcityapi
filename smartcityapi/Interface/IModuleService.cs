using smartcityapi.Model.commanResponce;
using smartcityapi.Model.module;

namespace smartcityapi.Interface
{
	public interface IModuleService
	{

		Task<ApiResponse> GetModuleAsync(ModuleGetRequestDTO request);
		Task<ApiResponse> GetModuleListAsync();
		Task<ApiResponse> CreateModuleAsync(ModuleAddRequestDTO request);
		Task<ApiResponse> UpdateModuleAsync(ModuleEditRequestDTO request);
		Task<ApiResponse> DeleteModuleAsync(ModuleDeleteRequestDTO request);

		
	}
}
