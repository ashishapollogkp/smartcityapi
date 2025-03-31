using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;
using smartcityapi.Model.pages;
using smartcityapi.Model.RoleAccess;

namespace smartcityapi.Interface
{
	public interface IRoleAccessService
	{
		Task<ApiResponse> GetRoleAccessListAsync(GetRoleAccessRequestDTO request);

		Task<ApiResponse> AddEditRoleAccessAsync(RoleAccessRequestDTO request);
		Task<ApiResponse> DeleteRoleAccessAsync(RoleAccessDeleteRequestDTO request);
	}
}
