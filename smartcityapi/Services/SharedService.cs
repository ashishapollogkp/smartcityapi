using Microsoft.EntityFrameworkCore;
using smartcityapi.Context;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Common;
using smartcityapi.Model.module;
using smartcityapi.Model.shared;
using System.Net;

namespace smartcityapi.Services
{
	public class SharedService: ISharedService
	{
		private readonly SmartCityDBContext _sqlDBContext;
		protected ApiResponse _response;
		protected UserService _userService;
		public SharedService(SmartCityDBContext sqlDBContext, UserService userService)
		{
			_sqlDBContext = sqlDBContext;
			_response = new();
			_userService = userService;
		}

		public async Task<ApiResponse> GetModuleList()
		{
			try
			{
				var result = await _sqlDBContext.master_module
					.Where(stop => stop.is_deleted == 0)
					.OrderBy(stop => stop.module_order)
					.Select(stop => new ddlViewDTO
					{
						Value = stop.pk_module_id,
						Text = stop.module_name
					})
					.ToListAsync();

				if (result.Any()) // More efficient than result.Count > 0
				{
					return new ApiResponse
					{
						Result = result,
						StatusCode = HttpStatusCode.OK,
						IsSuccess = true
					};
				}
				else
				{
					return new ApiResponse
					{
						ActionResponse = "No Data",
						Result = null,
						StatusCode = HttpStatusCode.NoContent,
						IsSuccess = false
					};
				}
			}
			catch (Exception e)
			{
				return new ApiResponse
				{
					ActionResponse = $"Error: {e.Message}",
					Result = null,
					StatusCode = HttpStatusCode.InternalServerError,
					IsSuccess = false
				};
			}
		}

		public async Task<ApiResponse> GetDepartmentList()
		{
			var dept_id = _userService.GetUserDeptId();
		


			try
			{
				List<ddlViewDTO> result = new List<ddlViewDTO>();
				if (dept_id != 1)
				{
					result = await _sqlDBContext.master_department
					   .Where(stop => stop.is_deleted == 0 && stop.pk_department_id== dept_id)
					   .OrderBy(stop => stop.department_name)
					   .Select(stop => new ddlViewDTO
					   {
						   Value = stop.pk_department_id,
						   Text = stop.department_name
					   })
					   .ToListAsync();
				}
				else {

					result = await _sqlDBContext.master_department
						   .Where(stop => stop.is_deleted == 0 )
						   .OrderBy(stop => stop.department_name)
						   .Select(stop => new ddlViewDTO
						   {
							   Value = stop.pk_department_id,
							   Text = stop.department_name
						   })
						   .ToListAsync();

				}

				if (result.Any()) // More efficient than result.Count > 0
				{
					return new ApiResponse
					{
						Result = result,
						StatusCode = HttpStatusCode.OK,
						IsSuccess = true
					};
				}
				else
				{
					return new ApiResponse
					{
						ActionResponse = "No Data",
						Result = null,
						StatusCode = HttpStatusCode.NoContent,
						IsSuccess = false
					};
				}
			}
			catch (Exception e)
			{
				return new ApiResponse
				{
					ActionResponse = $"Error: {e.Message}",
					Result = null,
					StatusCode = HttpStatusCode.InternalServerError,
					IsSuccess = false
				};
			}
		}

		

		public async Task<ApiResponse> GetRoleList()
		{
			var dept_id = _userService.GetUserDeptId();
			var role_id = _userService.GetUserRoleId();

			var levelvalue = _sqlDBContext.master_role.Where(x => x.pk_role_id == role_id && x.fk_department_id == dept_id).FirstOrDefault();




			try
			{

				if (dept_id == 1)
				{
					var result = await _sqlDBContext.master_role
						.Where(stop => stop.is_deleted == 0)//&& stop.pk_role_id != role_id && stop.level_value > levelvalue.level_value
						.OrderBy(stop => stop.role_name)
						.Select(stop => new ddlViewDTO
						{
							Value = stop.pk_role_id,
							Text = stop.role_name
						})
						.ToListAsync();


					if (result.Any()) // More efficient than result.Count > 0
					{
						return new ApiResponse
						{
							Result = result,
							StatusCode = HttpStatusCode.OK,
							IsSuccess = true
						};
					}
					else
					{
						return new ApiResponse
						{
							ActionResponse = "No Data",
							Result = null,
							StatusCode = HttpStatusCode.NoContent,
							IsSuccess = false
						};
					}
				}
				else
				{
					var result = await _sqlDBContext.master_role
							.Where(stop => stop.is_deleted == 0 && stop.pk_role_id != role_id && stop.level_value > levelvalue.level_value)
							.OrderBy(stop => stop.role_name)
							.Select(stop => new ddlViewDTO
							{
								Value = stop.pk_role_id,
								Text = stop.role_name
							})
							.ToListAsync();


					if (result.Any()) // More efficient than result.Count > 0
					{
						return new ApiResponse
						{
							Result = result,
							StatusCode = HttpStatusCode.OK,
							IsSuccess = true
						};
					}
					else
					{
						return new ApiResponse
						{
							ActionResponse = "No Data",
							Result = null,
							StatusCode = HttpStatusCode.NoContent,
							IsSuccess = false
						};
					}

				}



			}
			catch (Exception e)
			{
				return new ApiResponse
				{
					ActionResponse = $"Error: {e.Message}",
					Result = null,
					StatusCode = HttpStatusCode.InternalServerError,
					IsSuccess = false
				};
			}
		}


		public async Task<ApiResponse> GetLevelList()
		{
			var dept_id = _userService.GetUserDeptId();
			var role_id = _userService.GetUserRoleId();

			var levelvalue = _sqlDBContext.master_role.Where(x => x.pk_role_id == role_id && x.fk_department_id == dept_id).FirstOrDefault();




			try
			{
				var result = await _sqlDBContext.master_level
					.Where(stop => stop.is_deleted == 0 && stop.level_order > levelvalue.level_value)
					.OrderBy(stop => stop.level_order)
					.Select(stop => new ddlViewDTO
					{
						Value = stop.level_order,
						Text = stop.level_name
					})
					.ToListAsync();

				if (result.Any()) // More efficient than result.Count > 0
				{
					return new ApiResponse
					{
						Result = result,
						StatusCode = HttpStatusCode.OK,
						IsSuccess = true
					};
				}
				else
				{
					return new ApiResponse
					{
						ActionResponse = "No Data",
						Result = null,
						StatusCode = HttpStatusCode.NoContent,
						IsSuccess = false
					};
				}
			}
			catch (Exception e)
			{
				return new ApiResponse
				{
					ActionResponse = $"Error: {e.Message}",
					Result = null,
					StatusCode = HttpStatusCode.InternalServerError,
					IsSuccess = false
				};
			}
		}

		public async Task<ApiResponse> GetAssetTypeList()
		{
			var dept_id = _userService.GetUserDeptId();
			var role_id = _userService.GetUserRoleId();

		




			try
			{
				var result = await _sqlDBContext.Asset_Type_Master
					.Where(stop => stop.is_deleted == 0 && stop.fk_department_id==dept_id)
					.OrderBy(stop => stop.asset_name)
					.Select(stop => new ddlViewDTO
					{
						Value = stop.pk_asset_type_id,
						Text = stop.asset_name
					})
					.ToListAsync();

				if (result.Any()) // More efficient than result.Count > 0
				{
					return new ApiResponse
					{
						Result = result,
						StatusCode = HttpStatusCode.OK,
						IsSuccess = true
					};
				}
				else
				{
					return new ApiResponse
					{
						ActionResponse = "No Data",
						Result = null,
						StatusCode = HttpStatusCode.NoContent,
						IsSuccess = false
					};
				}
			}
			catch (Exception e)
			{
				return new ApiResponse
				{
					ActionResponse = $"Error: {e.Message}",
					Result = null,
					StatusCode = HttpStatusCode.InternalServerError,
					IsSuccess = false
				};
			}
		}



		public async Task<ApiResponse> GetRoleListByDept(GetDepartmentWiseRequestDTO request)
		{

			var dept_id = _userService.GetUserDeptId();
			var role_id = _userService.GetUserRoleId();

			var levelvalue = _sqlDBContext.master_role.Where(x => x.pk_role_id == role_id && x.fk_department_id == dept_id).FirstOrDefault();


			try
			{
				List<ddlViewDTO> result = new List<ddlViewDTO>();

				if (dept_id == 1)
				{

					result = await _sqlDBContext.master_role
					   .Where(stop => stop.is_deleted == 0 && stop.fk_department_id == request.Department_Id)
					   .OrderBy(stop => stop.role_name)
					   .Select(stop => new ddlViewDTO
					   {
						   Value = stop.pk_role_id,
						   Text = stop.role_name
					   })
					   .ToListAsync();
				}
				else {


					result = await _sqlDBContext.master_role
				   .Where(stop => stop.is_deleted == 0 && stop.fk_department_id == request.Department_Id && stop.level_value > levelvalue.level_value)
				   .OrderBy(stop => stop.role_name)
				   .Select(stop => new ddlViewDTO
				   {
					   Value = stop.pk_role_id,
					   Text = stop.role_name
				   })
				   .ToListAsync();

				}

				if (result.Any()) // More efficient than result.Count > 0
				{
					return new ApiResponse
					{
						Result = result,
						StatusCode = HttpStatusCode.OK,
						IsSuccess = true
					};
				}
				else
				{
					return new ApiResponse
					{
						ActionResponse = "No Data",
						Result = null,
						StatusCode = HttpStatusCode.NoContent,
						IsSuccess = false
					};
				}
			}
			catch (Exception e)
			{
				return new ApiResponse
				{
					ActionResponse = $"Error: {e.Message}",
					Result = null,
					StatusCode = HttpStatusCode.InternalServerError,
					IsSuccess = false
				};
			}
		}

		public async Task<ApiResponse> GetModuleListByDept(GetDepartmentWiseRequestDTO request)
		{
			try
			{

				var result = await (from dp in _sqlDBContext.department_permissions
									join m in _sqlDBContext.master_module
									on dp.fk_module_id equals m.pk_module_id into moduleGroup
									from module in moduleGroup.DefaultIfEmpty() // Left Join
									where dp.fk_department_id == request.Department_Id && dp.is_active==1
									orderby module.module_name
									select new ddlViewDTO
									{
										Value = module.pk_module_id,
										Text = module.module_name
									}).ToListAsync();


				if (result.Any()) // More efficient than result.Count > 0
				{
					return new ApiResponse
					{
						Result = result,
						StatusCode = HttpStatusCode.OK,
						IsSuccess = true
					};
				}
				else
				{
					return new ApiResponse
					{
						ActionResponse = "No Data",
						Result = null,
						StatusCode = HttpStatusCode.NoContent,
						IsSuccess = false
					};
				}
			}
			catch (Exception e)
			{
				return new ApiResponse
				{
					ActionResponse = $"Error: {e.Message}",
					Result = null,
					StatusCode = HttpStatusCode.InternalServerError,
					IsSuccess = false
				};
			}
		}

	}
}
