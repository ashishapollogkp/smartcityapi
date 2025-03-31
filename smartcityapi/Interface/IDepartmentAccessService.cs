using smartcityapi.Model.commanResponce;
using smartcityapi.Model.DepartmentAccess;
using smartcityapi.Model.RoleAccess;

namespace smartcityapi.Interface
{
	public interface IDepartmentAccessService
	{
		Task<ApiResponse> GetDepartmentAccessListAsync(GetDepartmentAccessRequestDTO request);
		Task<ApiResponse> AddEditDepartmentAccessAsync(DepartmentAccessRequestDTO request);
		Task<ApiResponse> DeleteDepartmentAccessAsync(DepartmentAccessDeleteRequestDTO request);
	}
}
