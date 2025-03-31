using Microsoft.EntityFrameworkCore;
using smartcityapi.Context;
using smartcityapi.Data;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.module;
using smartcityapi.Model.pages;
using System.Net;

namespace smartcityapi.Services
{
	public class PageService:IPageService
	{
		private readonly SmartCityDBContext _sqlDBContext;
		protected ApiResponse _response;
		protected UserService _userService;
		public PageService(SmartCityDBContext sqlDBContext, UserService userService)
		{
			_sqlDBContext = sqlDBContext;
			_response = new();
			_userService = userService;
		}

		public async Task<ApiResponse> GetPageListAsync(GetPagesRequestDTO request)
		{


			List<PageGetViewDTO> result = null;
			try
			{
				 result = await (from a in _sqlDBContext.master_module_pages
									join b in _sqlDBContext.master_module
										on a.fk_module_id equals b.pk_module_id into moduleJoin
									from b in moduleJoin.DefaultIfEmpty() // ✅ Left Join to prevent null reference issues
									where a.is_deleted == 0 && a.fk_module_id == request.Module_Id
									orderby a.page_order
									select new PageGetViewDTO
									{
										is_active = a.is_active,
										is_deleted = a.is_deleted,
										page_icon = a.page_icon ?? "", // ✅ Null-safe
										page_name = a.page_name ?? "Unknown",
										page_url = a.page_url ?? "#",
										page_order = a.page_order,
										pk_page_id = a.pk_page_id,
										fk_module_id = a.fk_module_id,
										module_name = b != null ? b.module_name ?? "Unknown" : "Unknown" // ✅ Handles missing module
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


		public async Task<ApiResponse> AddEditPageAsync(PagesRequestDTO request)
		{
			if (request.Page_Id == 0) // Insert mode
			{
				// Check if the page name already exists in the module
				bool exists = await _sqlDBContext.master_module_pages
					.AnyAsync(x => x.page_name == request.Page_Name && x.fk_module_id == request.Module_Id && x.is_deleted == 0);

				if (exists)
				{
					return new ApiResponse
					{
						StatusCode = HttpStatusCode.Conflict,
						ActionResponse = "Duplicate Data",
						IsSuccess = false
					};
				}

				// Get the max page order (efficiently handles empty tables)
				//int maxOrder = (await _sqlDBContext.master_module_pages
				//	.Where(x => x.is_deleted == 0 && x.fk_module_id == request.Module_Id)
				//	.MaxAsync(x => (int?)x.page_order)) ?? 0;

				int maxOrder = await _sqlDBContext.master_module_pages
	.Where(x => x.is_deleted == 0 && x.fk_module_id == request.Module_Id)
	.MaxAsync(x => (int?)x.page_order) ?? 0;


				// Create new page
				var newPage = new master_module_pages
				{
					page_name = request.Page_Name,
					page_icon = request.Page_Icon,
					page_url = request.Page_Url,
					page_order = maxOrder + 1,
					is_active = 1,
					is_deleted = 0,
					created_by = 1,
					created_date = DateTime.Now,
					fk_module_id=request.Module_Id
				};

				await _sqlDBContext.master_module_pages.AddAsync(newPage);
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
				bool duplicateExists = await _sqlDBContext.master_module_pages
					.AnyAsync(x => x.pk_page_id != request.Page_Id && x.page_name == request.Page_Name && x.is_deleted == 0);

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
				var existingPage = await _sqlDBContext.master_module_pages.FindAsync(request.Page_Id);
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
				existingPage.page_name = request.Page_Name;
				existingPage.page_url = request.Page_Url;
				existingPage.page_icon = request.Page_Icon;
				existingPage.last_updated_by = 0;
				existingPage.last_updated_date = DateTime.Now;

				_sqlDBContext.master_module_pages.Update(existingPage);
				await _sqlDBContext.SaveChangesAsync();

				return new ApiResponse
				{
					StatusCode = HttpStatusCode.OK,
					ActionResponse = "Data Updated",
					IsSuccess = true
				};
			}
		}

		public async Task<ApiResponse> DeletePageAsync(PageDeleteRequestDTO request)
		{
			try
			{
				// Find the page using `FindAsync()` for better performance
				var page = await _sqlDBContext.master_module_pages.FindAsync(request.Page_Id);

				// Check if page exists and belongs to the correct module
				if (page == null || page.fk_module_id != request.Module_Id)
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
