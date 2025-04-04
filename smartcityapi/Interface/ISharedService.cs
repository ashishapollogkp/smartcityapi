using Microsoft.AspNetCore.Mvc.ApplicationModels;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.shared;

namespace smartcityapi.Interface
{
	public interface ISharedService
	{
		Task<ApiResponse> GetModuleList();
		Task<ApiResponse> GetDepartmentList();	
		Task<ApiResponse> GetRoleList();
		Task<ApiResponse> GetLevelList();
		Task<ApiResponse> GetAssetTypeList();


		Task<ApiResponse> GetRoleListByDept(GetDepartmentWiseRequestDTO request);
		Task<ApiResponse> GetModuleListByDept(GetDepartmentWiseRequestDTO request);
	}
}
