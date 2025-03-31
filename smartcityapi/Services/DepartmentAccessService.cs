using Microsoft.EntityFrameworkCore;
using smartcityapi.Context;
using smartcityapi.Data;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;
using smartcityapi.Model.DepartmentAccess;
using smartcityapi.Model.module;
using smartcityapi.Model.RoleAccess;
using System.Net;

namespace smartcityapi.Services
{
	public class DepartmentAccessService : IDepartmentAccessService
	{
		private readonly SmartCityDBContext _sqlDBContext;
		protected ApiResponse _response;
		protected UserService _userService;
		public DepartmentAccessService(SmartCityDBContext sqlDBContext, UserService userService)
		{
			_sqlDBContext = sqlDBContext;
			_response = new();
			_userService = userService;
		}

		public async Task<ApiResponse> GetDepartmentAccessListAsync(GetDepartmentAccessRequestDTO request)
		{


			List<DeptAccessViewDTO> result = null;
			try
			{




				result = await (from a in _sqlDBContext.master_module
								
								where a.is_deleted == 0 

								orderby a.module_name
								select new DeptAccessViewDTO
								{
									Department_Id=0,
									is_access=0,
									module_name=a.module_name,
									pk_module_id=a.pk_module_id

								}).ToListAsync();

				foreach (var checkaccess in result)
				{

					var check = _sqlDBContext.department_permissions.Where(x => x.fk_department_id == request.Department_Id && x.fk_module_id == checkaccess.pk_module_id).FirstOrDefault();
					
					if (check != null)
					{
						checkaccess.Department_Id = check.fk_department_id;
						checkaccess.is_access = check.is_active;


					}


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



		public async Task<ApiResponse> AddEditDepartmentAccessAsync(DepartmentAccessRequestDTO request)
		{
			using var transaction = await _sqlDBContext.Database.BeginTransactionAsync();
			try
			{
				// Check if the record already exists
				var existingDepartmentAccess = await _sqlDBContext.department_permissions
					.FirstOrDefaultAsync(x => x.fk_department_id == request.Department_Id
										   && x.fk_module_id == request.Module_Id
										   ); // Fixed logical error

				if (existingDepartmentAccess != null)
				{
					//If exists, remove it
					_sqlDBContext.department_permissions.Remove(existingDepartmentAccess);
					await _sqlDBContext.SaveChangesAsync();



				}

				// Create a new role permission entry
				var newRoleAccess = new department_permissions
				{
					
					fk_department_id=request.Department_Id,
					fk_module_id=request.Module_Id,	
					is_active=request.is_access,
					is_deleted=0,

					created_by = 0,
					created_date = DateTime.UtcNow // Use UTC for consistency
				};

				await _sqlDBContext.department_permissions.AddAsync(newRoleAccess);
				await _sqlDBContext.SaveChangesAsync();
				await transaction.CommitAsync(); // Commit transaction

				return new ApiResponse
				{
					ActionResponse = "Record Saved",
					StatusCode = HttpStatusCode.OK,
					IsSuccess = true
				};
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync(); // Rollback on error
				return new ApiResponse
				{
					ActionResponse = $"Error: {ex.Message}",
					StatusCode = HttpStatusCode.InternalServerError,
					IsSuccess = false
				};
			}
		}



		public async Task<ApiResponse> DeleteDepartmentAccessAsync(DepartmentAccessDeleteRequestDTO request)
		{
			try
			{
				// Find the page using `FindAsync()` for better performance
				var existingDepartmentAccess = await _sqlDBContext.department_permissions
						.FirstOrDefaultAsync(x => x.fk_department_id == request.Department_Id
											   && x.fk_module_id == request.Module_Id
											   ); // Fixed logical error

				// Check if page exists and belongs to the correct module
				if (existingDepartmentAccess != null)
				{
					//If exists, remove it
					_sqlDBContext.department_permissions.Remove(existingDepartmentAccess);
					await _sqlDBContext.SaveChangesAsync();

					return new ApiResponse
					{
						ActionResponse = "Record Deleted",
						StatusCode = HttpStatusCode.OK,
						IsSuccess = true
					};

				}
				else
				{

					return new ApiResponse
					{
						ActionResponse = "Not Found",
						StatusCode = HttpStatusCode.NotFound,
						IsSuccess = false
					};


				}

				
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
