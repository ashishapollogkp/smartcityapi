using Azure.Core;
using Microsoft.EntityFrameworkCore;
using smartcityapi.Context;
using smartcityapi.Data;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Model.AssetType;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;
using System.Net;

namespace smartcityapi.Services
{
	public class AssetTypeService: IAssetTypeService
	{
		private readonly SmartCityDBContext _sqlDBContext;
		protected ApiResponse _response;
		protected UserService _userService;
		public AssetTypeService(SmartCityDBContext sqlDBContext, UserService userService)
		{
			_sqlDBContext = sqlDBContext;
			_response = new();
			_userService = userService;
		}

		public async Task<ApiResponse> GetAssetTypeList(GetAssetTypeRequestDTO request)
		{

			List<GetAssetTypeViewDTO> result = null;
			try
			{
				result = await (from a in _sqlDBContext.Asset_Type_Master
								join b in _sqlDBContext.master_department
									on a.fk_department_id equals b.pk_department_id into moduleJoin
								from b in moduleJoin.DefaultIfEmpty() // ✅ Left Join to prevent null reference issues
								where a.is_deleted == 0 && a.fk_department_id == request.Department_Id
								orderby a.asset_name
								select new GetAssetTypeViewDTO
								{
									is_active = a.is_active,
									is_deleted = a.is_deleted,
									Department_Id = request.Department_Id,
									
									Asset_Name = a.asset_name,
									Asset_Description = a.asset_description,
									Asset_Type_Id=a.pk_asset_type_id


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

		public async Task<ApiResponse> CreateAssetType(CreateAssetTypeRequestDTO request)
		{
			
				// Check if the page name already exists in the module
				bool exists = await _sqlDBContext.Asset_Type_Master
					.AnyAsync(x => x.asset_name == request.Asset_Name && x.fk_department_id == request.Department_Id && x.is_deleted == 0);

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
				var newPage = new Asset_Type_Master
				{
					asset_name = request.Asset_Name,
					asset_description = request.Asset_Description,

					is_active = 1,
					is_deleted = 0,
					created_by = 1,
					created_date = DateTime.Now,
					fk_department_id = request.Department_Id,
					
				};

				await _sqlDBContext.Asset_Type_Master.AddAsync(newPage);
				await _sqlDBContext.SaveChangesAsync();

				return new ApiResponse
				{
					ActionResponse = "Record Saved",
					StatusCode = HttpStatusCode.OK,
					IsSuccess = true
				};


			
		}



		public async Task<ApiResponse> UpdateAssetType(UpdateAssetTypeRequestDTO request)
		{

			if (request.Asset_Type_Id > 0) // Insert mode
			{

				// Check for duplicate name (excluding current page)
				bool duplicateExists = await _sqlDBContext.Asset_Type_Master
				.AnyAsync(x => x.pk_asset_type_id != request.Asset_Type_Id && x.asset_name == request.Asset_Name && x.is_deleted == 0);

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
				var existingPage = await _sqlDBContext.Asset_Type_Master.FindAsync(request.Asset_Type_Id);
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
				existingPage.asset_name = request.Asset_Name;
				existingPage.asset_description = request.Asset_Description;
				existingPage.last_updated_by = 0;
				existingPage.last_updated_date = DateTime.Now;

				_sqlDBContext.Asset_Type_Master.Update(existingPage);
				await _sqlDBContext.SaveChangesAsync();

				return new ApiResponse
				{
					StatusCode = HttpStatusCode.OK,
					ActionResponse = "Data Updated",
					IsSuccess = true
				};
			}
			else
			{

				return new ApiResponse
				{
					StatusCode = HttpStatusCode.NoContent,
					ActionResponse = "No Data",
					IsSuccess = false
				};

			}
		}



		public async Task<ApiResponse> DeleteAssetType(DeleteAssetTypeRequestDTO request)
		{
			try
			{
				// Find the page using `FindAsync()` for better performance
				var page = await _sqlDBContext.Asset_Type_Master.FindAsync(request.Asset_Type_Id);

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
