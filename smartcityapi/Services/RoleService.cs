using Microsoft.EntityFrameworkCore;
using smartcityapi.Context;
using smartcityapi.Data;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;
using smartcityapi.Model.pages;
using System.Net;

namespace smartcityapi.Services
{
	public class RoleService: IRoleService
	{
		private readonly SmartCityDBContext _sqlDBContext;
		protected ApiResponse _response;
		protected UserService _userService;
		public RoleService(SmartCityDBContext sqlDBContext, UserService userService)
		{
			_sqlDBContext = sqlDBContext;
			_response = new();
			_userService = userService;
		}

		public async Task<ApiResponse> GetRoleListAsync(GetRoleRequestDTO request)
		{


			var dept_id = _userService.GetUserDeptId();
			var role_id = _userService.GetUserRoleId();

			var levelvalue = _sqlDBContext.master_role.Where(x => x.pk_role_id == role_id && x.fk_department_id == dept_id).FirstOrDefault();


			List<RoleGetViewDTO> result = null;
			try
			{
				if (dept_id > 1)
				{
					result = await (from a in _sqlDBContext.master_role


									join b in _sqlDBContext.master_department
										on a.fk_department_id equals b.pk_department_id into moduleJoin
									from b in moduleJoin.DefaultIfEmpty() // ✅ Left Join to prevent null reference issues


									join ml in _sqlDBContext.master_level
										on a.level_value equals ml.level_order into masterRoleJoin
									from ml in masterRoleJoin.DefaultIfEmpty() // ✅ Left Join to prevent null reference issues



									where a.is_deleted == 0 && a.fk_department_id == request.Department_Id && a.level_value > levelvalue.level_value
									orderby a.role_name
									select new RoleGetViewDTO
									{
										is_active = a.is_active,
										is_deleted = a.is_deleted,
										Department_Id = request.Department_Id,
										Department_Name = b.department_name,
										Role_Id = a.pk_role_id,
										Role_Name = a.role_name,
										Level_Name = ml.level_name,
										level_value = ml.level_order


									})
						   .AsNoTracking() // ✅ Improves performance
						   .ToListAsync();
				}
				else {

					result = await (from a in _sqlDBContext.master_role


									join b in _sqlDBContext.master_department
										on a.fk_department_id equals b.pk_department_id into moduleJoin
									from b in moduleJoin.DefaultIfEmpty() // ✅ Left Join to prevent null reference issues


									join ml in _sqlDBContext.master_level
										on a.level_value equals ml.level_order into masterRoleJoin
									from ml in masterRoleJoin.DefaultIfEmpty() // ✅ Left Join to prevent null reference issues



									where a.is_deleted == 0 && a.fk_department_id == request.Department_Id 
									orderby a.role_name
									select new RoleGetViewDTO
									{
										is_active = a.is_active,
										is_deleted = a.is_deleted,
										Department_Id = request.Department_Id,
										Department_Name = b.department_name,
										Role_Id = a.pk_role_id,
										Role_Name = a.role_name,
										Level_Name = ml.level_name,
										level_value = ml.level_order


									})
							   .AsNoTracking() // ✅ Improves performance
							   .ToListAsync();
				}



				if (result.Count > 0)
				{
					_response.Result = result;
					_response.StatusCode = HttpStatusCode.OK;
					_response.IsSuccess = true;

				}
				else
				{
					_response.ActionResponse = "No  Data";
					_response.Result = null;
					_response.StatusCode = HttpStatusCode.NoContent;
					_response.IsSuccess = false;

				}
				return _response;



			}
			catch (Exception e)
			{
				_response.ActionResponse = "No  Data";
				_response.Result = null;
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = false;


			}

			return _response;
		}


		public async Task<ApiResponse> AddEditRoleAsync(RoleRequestDTO request)
		{
			if (request.Role_Id == 0) // Insert mode
			{
				// Check if the page name already exists in the module
				bool exists = await _sqlDBContext.master_role
					.AnyAsync(x => x.role_name == request.Role_Name && x.fk_department_id == request.Department_Id && x.is_deleted == 0);

				if (exists)
				{
					return new ApiResponse
					{
						StatusCode = HttpStatusCode.Conflict,
						ActionResponse = "Duplicate Data",
						IsSuccess = false
					};
				}

				


				// Create new page
				var newPage = new master_role
				{
					role_name = request.Role_Name,
					
					is_active = 1,
					is_deleted = 0,
					created_by = 1,
					created_date = DateTime.Now,
					fk_department_id = request.Department_Id,
					level_value=request.level_value
				};

				await _sqlDBContext.master_role.AddAsync(newPage);
				await _sqlDBContext.SaveChangesAsync();

				return new ApiResponse
				{
					ActionResponse = "Record Saved",
					StatusCode = HttpStatusCode.OK,
					IsSuccess = true
				};
			}
			else // Update mode
			{
				// Check for duplicate name (excluding current page)
				bool duplicateExists = await _sqlDBContext.master_role
					.AnyAsync(x => x.pk_role_id != request.Role_Id && x.role_name == request.Role_Name && x.is_deleted == 0);

				if (duplicateExists)
				{
					return new ApiResponse
					{
						StatusCode = HttpStatusCode.Conflict,
						ActionResponse = "Duplicate Data",
						IsSuccess = false
					};
				}

				// Find the existing page
				var existingPage = await _sqlDBContext.master_role.FindAsync(request.Role_Id);
				if (existingPage == null)
				{
					return new ApiResponse
					{
						StatusCode = HttpStatusCode.NoContent,
						ActionResponse = "No Data",
						IsSuccess = false
					};
				}

				// Update fields
				existingPage.role_name = request.Role_Name;				
				existingPage.last_updated_by = 0;
				existingPage.last_updated_date = DateTime.Now;
				existingPage.level_value = request.level_value;

				_sqlDBContext.master_role.Update(existingPage);
				await _sqlDBContext.SaveChangesAsync();

				return new ApiResponse
				{
					StatusCode = HttpStatusCode.OK,
					ActionResponse = "Data Updated",
					IsSuccess = true
				};
			}
		}

		public async Task<ApiResponse> DeleteRoleAsync(RoleDeleteRequestDTO request)
		{
			try
			{
				// Find the page using `FindAsync()` for better performance
				var page = await _sqlDBContext.master_role.FindAsync(request.Role_Id);

				// Check if page exists and belongs to the correct module
				if (page == null)
				{
					return new ApiResponse
					{
						StatusCode = HttpStatusCode.NoContent,
						ActionResponse = "No Data",
						IsSuccess = false
					};
				}

				// Soft delete the page
				page.is_deleted = 1;
				page.deleted_by = 0; // Update with actual user ID if available
				page.deleted_date = DateTime.Now;

				await _sqlDBContext.SaveChangesAsync();

				return new ApiResponse
				{
					StatusCode = HttpStatusCode.OK,
					ActionResponse = "Data Deleted",
					IsSuccess = true
				};
			}
			catch (Exception ex)
			{
				// Log exception (if needed)
				return new ApiResponse
				{
					StatusCode = HttpStatusCode.InternalServerError,
					ActionResponse = "An error occurred while deleting the data",
					IsSuccess = false
				};
			}
		}
	}
}
