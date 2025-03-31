using Microsoft.EntityFrameworkCore;
using smartcityapi.Context;
using smartcityapi.Data;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.pages;
using smartcityapi.Model.RoleAccess;
using System.Net;

namespace smartcityapi.Services
{
	public class RoleAccessService: IRoleAccessService
	{
		private readonly SmartCityDBContext _sqlDBContext;
		protected ApiResponse _response;
		protected UserService _userService;
		public RoleAccessService(SmartCityDBContext sqlDBContext, UserService userService)
		{
			_sqlDBContext = sqlDBContext;
			_response = new();
			_userService = userService;
		}

		public async Task<ApiResponse> GetRoleAccessListAsync(GetRoleAccessRequestDTO request)
		{
			try
			{
				var result = await (from a in _sqlDBContext.master_module_pages
									join b in _sqlDBContext.master_module
										on a.fk_module_id equals b.pk_module_id into moduleJoin
									from b in moduleJoin.DefaultIfEmpty() // Left Join
									where a.is_deleted == 0 && a.fk_module_id == request.Module_Id
									orderby a.page_order
									select new RoleAccessViewDTO
									{
										fk_module_id = b.pk_module_id,
										fk_page_id = a.pk_page_id,
										Module_Name = b.module_name,
										Page_Name = a.page_name
									})
								   .AsNoTracking()
								   .ToListAsync();

				if (result.Any()) // More efficient than Count > 0
				{
					// Get role permissions in a single query to avoid looping DB calls
					var rolePermissions = await _sqlDBContext.role_permissions
						.Where(rp => rp.fk_role_id == request.Role_Id)
						.ToListAsync();

					var roleNames = await _sqlDBContext.master_role
						.ToDictionaryAsync(r => r.pk_role_id, r => r.role_name);

					// Update result list with role permissions
					foreach (var accessData in result)
					{
						var roleAccess = rolePermissions.FirstOrDefault(x =>
							x.fk_module_id == accessData.fk_module_id &&
							x.fk_page_id == accessData.fk_page_id);

						if (roleAccess != null)
						{
							accessData.fk_role_id = roleAccess.fk_role_id;
							accessData.is_view = roleAccess.is_view;
							accessData.is_add = roleAccess.is_add;
							accessData.is_delete = roleAccess.is_delete;
							accessData.is_export = roleAccess.is_export;
							accessData.is_update = roleAccess.is_update;
							accessData.Role_Name = roleNames.GetValueOrDefault(roleAccess.fk_role_id, accessData.Page_Name);
						}
					}

					return new ApiResponse
					{
						Result = result,
						StatusCode = HttpStatusCode.OK,
						IsSuccess = true
					};
				}

				return new ApiResponse
				{
					ActionResponse = "No Data",
					Result = null,
					StatusCode = HttpStatusCode.NoContent,
					IsSuccess = false
				};
			}
			catch (Exception ex)
			{
				return new ApiResponse
				{
					ActionResponse = $"Error: {ex.Message}",
					Result = null,
					StatusCode = HttpStatusCode.InternalServerError,
					IsSuccess = false
				};
			}
		}


		public async Task<ApiResponse> AddEditRoleAccessAsync(RoleAccessRequestDTO request)
		{
			using var transaction = await _sqlDBContext.Database.BeginTransactionAsync();
			try
			{
				// Check if the record already exists
				var existingRoleAccess = await _sqlDBContext.role_permissions
					.FirstOrDefaultAsync(x => x.fk_role_id == request.Role_Id
										   && x.fk_module_id == request.Module_Id
										   && x.fk_page_id == request.Page_Id); // Fixed logical error

				if (existingRoleAccess != null)
				{
					// If exists, remove it
					_sqlDBContext.role_permissions.Remove(existingRoleAccess);
					await _sqlDBContext.SaveChangesAsync();
				}

				// Create a new role permission entry
				var newRoleAccess = new role_permissions
				{
					fk_role_id = request.Role_Id,
					fk_page_id = request.Page_Id,
					fk_module_id = request.Module_Id,
					is_add = request.Is_Add,
					is_delete = request.Is_Delete,
					is_export = request.Is_Export,
					is_update = request.Is_Update,
					is_view = request.Is_View,
					created_by = 0,
					created_date = DateTime.UtcNow // Use UTC for consistency
				};

				await _sqlDBContext.role_permissions.AddAsync(newRoleAccess);
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

		public async Task<ApiResponse> DeleteRoleAccessAsync(RoleAccessDeleteRequestDTO request)
		{
			//using var transaction = await _sqlDBContext.Database.BeginTransactionAsync();
			try
			{
				// Check if the record already exists
				var existingRoleAccess = await _sqlDBContext.role_permissions
					.FirstOrDefaultAsync(x => x.fk_role_id == request.Role_Id
										   && x.fk_module_id == request.Module_Id
										   && x.fk_page_id == request.Page_Id); // Fixed logical error

				if (existingRoleAccess != null)
				{
					// If exists, remove it
					_sqlDBContext.role_permissions.Remove(existingRoleAccess);
					await _sqlDBContext.SaveChangesAsync();


					return new ApiResponse
					{
						ActionResponse = "Record Deleted",
						StatusCode = HttpStatusCode.OK,
						IsSuccess = true
					};
				}
				else {

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
				//await transaction.RollbackAsync(); // Rollback on error
				return new ApiResponse
				{
					ActionResponse = $"Error: {ex.Message}",
					StatusCode = HttpStatusCode.InternalServerError,
					IsSuccess = false
				};
			}
		}

		
	}
}
