using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;
using smartcityapi.Model.pages;

namespace smartcityapi.Interface
{
	public interface IDepartmentService
	{
		Task<ApiResponse> GetDepartmentListAsync();
		Task<ApiResponse> AddEditDepartmentAsync(DepartmentRequestDTO request);
		Task<ApiResponse> DeleteDepartmentAsync(DepertmentDeleteRequestDTO request);
	}
}
