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
	public class DepartmentService: IDepartmentService
	{
		private readonly SmartCityDBContext _sqlDBContext;
		protected ApiResponse _response;
		protected UserService _userService;
		public DepartmentService(SmartCityDBContext sqlDBContext, UserService userService)
		{
			_sqlDBContext = sqlDBContext;
			_response = new();
			_userService = userService;
		}

		public async Task<ApiResponse> GetDepartmentListAsync()
		{


			List<DepartmentGetViewDTO> result = null;
			try
			{
				result = await (from a in _sqlDBContext.master_department								
								where a.is_deleted == 0 
								orderby a.department_name
								select new DepartmentGetViewDTO
								{
									Department_id=a.pk_department_id,
								Department_Name=a.department_name,
								is_active=a.is_active,
								is_deleted=a.is_deleted,
									
								})
					   .AsNoTracking() // ✅ Improves performance
					   .ToListAsync();



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


		public async Task<ApiResponse> AddEditDepartmentAsync(DepartmentRequestDTO request)
		{
			if (request.Department_id == 0) // Insert mode
			{
				// Check if the page name already exists in the module
				bool exists = await _sqlDBContext.master_department
					.AnyAsync(x => x.department_name == request.Department_Name  && x.is_deleted == 0);

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
				var newPage = new master_department
				{
					department_name = request.Department_Name,
					
					is_active = 1,
					is_deleted = 0,
					created_by = 1,
					created_date = DateTime.Now,
					
				};

				await _sqlDBContext.master_department.AddAsync(newPage);
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
				bool duplicateExists = await _sqlDBContext.master_department
					.AnyAsync(x => x.pk_department_id != request.Department_id && x.department_name == request.Department_Name && x.is_deleted == 0);

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
				var existingPage = await _sqlDBContext.master_department.FindAsync(request.Department_id);
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
				existingPage.department_name = request.Department_Name;
				
				existingPage.last_updated_by = 0;
				existingPage.last_updated_date = DateTime.Now;

				_sqlDBContext.master_department.Update(existingPage);
				await _sqlDBContext.SaveChangesAsync();

				return new ApiResponse
				{
					StatusCode = HttpStatusCode.OK,
					ActionResponse = "Data Updated",
					IsSuccess = true
				};
			}
		}

		public async Task<ApiResponse> DeleteDepartmentAsync(DepertmentDeleteRequestDTO request)
		{
			try
			{
				// Find the page using `FindAsync()` for better performance
				var page = await _sqlDBContext.master_department.FindAsync(request.Department_id);

				// Check if page exists and belongs to the correct module
				if (page == null )
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
