using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;
using smartcityapi.Model.pages;

namespace smartcityapi.Interface
{
	public interface IRoleService
	{
		Task<ApiResponse> GetRoleListAsync(GetRoleRequestDTO request);
		Task<ApiResponse> AddEditRoleAsync(RoleRequestDTO request);
		Task<ApiResponse> DeleteRoleAsync(RoleDeleteRequestDTO request);
	}
}
