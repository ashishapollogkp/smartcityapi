using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smartcityapi.Context;
using smartcityapi.Data;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Model.AssetType;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Device;
using System.Net;

namespace smartcityapi.Services
{
	public class DeviceService : IDeviceService
	{

		private readonly SmartCityDBContext _sqlDBContext;
		protected ApiResponse _response;
		protected UserService _userService;
		public DeviceService(SmartCityDBContext sqlDBContext, UserService userService)
		{
			_sqlDBContext = sqlDBContext;
			_response = new();
			_userService = userService;
		}




		public async Task<ApiResponse> CreateDevice(CreateDeviceRequestDTO request)
		{
			// Check if the page name already exists in the module
			bool exists = await _sqlDBContext.Master_Device
							.AnyAsync(x =>
								x.Device_IMEI == request.Device_IMEI ||
								x.Device_No == request.Device_No ||
								x.SIM_No == request.SIM_No ||
								x.Fk_Department_Id == request.Department_Id ||
								x.Fk_Device_Type_Id == request.Device_Type_Id
							);
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
			var newPage = new Master_Device
			{

				Device_Expiry_Date = request.Device_Expiry_Date,
				Fk_Device_Type_Id = request.Device_Type_Id,
				Device_IMEI = request.Device_IMEI,
				SIM_No = request.SIM_No,
				Device_Manufacture_Date = request.Device_Manufacture_Date,
				Device_Model = request.Device_Model,
				Device_Name = request.Device_Name,
				Device_No = request.Device_No,
				SIM_Operator = request.SIM_Operator,
				Device_Vender = request.Device_Vender,
				is_active = 1,
				is_deleted = 0,
				created_by = 1,
				created_date = DateTime.Now,
				Fk_Department_Id = request.Department_Id,

			};

			await _sqlDBContext.Master_Device.AddAsync(newPage);
			await _sqlDBContext.SaveChangesAsync();

			return new ApiResponse
			{
				ActionResponse = "Record Saved",
				StatusCode = HttpStatusCode.OK,
				IsSuccess = true
			};
		}




		public async Task<ApiResponse> UpdateDevice(UpdateDeviceRequestDTO request)
		{

			if (request.Pk_Device_Id > 0) // Insert mode
			{

				// Check for duplicate name (excluding current page)
				bool duplicateExists = await _sqlDBContext.Master_Device
				.AnyAsync(x => x.Pk_Device_Id != request.Pk_Device_Id && x.Fk_Department_Id == request.Department_Id && 
				
				( x.Device_No == request.Device_No || x.SIM_No == request.SIM_No )
				
				&& x.Fk_Device_Type_Id == request.Device_Type_Id && x.is_deleted == 0);


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
				var existingPage = await _sqlDBContext.Master_Device.FindAsync(request.Pk_Device_Id);
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
				existingPage.Fk_Device_Type_Id = request.Device_Type_Id;
				existingPage.Device_Expiry_Date = request.Device_Expiry_Date;
				existingPage.Device_IMEI = request.Device_IMEI;
				existingPage.Device_Manufacture_Date = request.Device_Manufacture_Date;
				existingPage.Device_Model = request.Device_Model;
				existingPage.Device_Name = request.Device_Name;
				existingPage.Device_No = request.Device_No;
				existingPage.Device_Vender = request.Device_Vender;
				existingPage.SIM_No = request.SIM_No;
				existingPage.SIM_Operator = request.SIM_Operator;
					



				
				existingPage.last_updated_by = 0;
				existingPage.last_updated_date = DateTime.Now;

				_sqlDBContext.Master_Device.Update(existingPage);
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




		public async Task<ApiResponse> DeleteDevice(DeleteDeviceRequestDTO request)
		{
			try
			{
				// Find the page using `FindAsync()` for better performance
				var page = await _sqlDBContext.Master_Device.FindAsync(request.Device_Id);

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



		public async Task<ApiResponse> GetDeviceList(GetDeviceRequestDTO request)
		{

			List<GetDeviceViewDTO> result = null;
			try
			{
				result = await (from a in _sqlDBContext.Master_Device
								join b in _sqlDBContext.Asset_Type_Master
									on a.Fk_Device_Type_Id equals b.pk_asset_type_id into moduleJoin
								from b in moduleJoin.DefaultIfEmpty() // ✅ Left Join to prevent null reference issues
								where a.is_deleted == 0 && a.Fk_Department_Id == request.Department_Id

								  && (request.Device_Type_Id == 0 || a.Fk_Device_Type_Id == request.Device_Type_Id) // Conditional filter

								orderby b.asset_name
								select new GetDeviceViewDTO
								{
									
									Device_Expiry_Date=a.Device_Expiry_Date,
									Device_Type_Id=a.Fk_Device_Type_Id,
									Pk_Device_Id=a.Pk_Device_Id,
									Device_IMEI=a.Device_IMEI,
									Device_Manufacture_Date= a.Device_Manufacture_Date,
									Device_Model= a.Device_Model,
									Device_Name = a.Device_Name,
									Device_No= a.Device_No,
									Device_Vender = a.Device_Vender,
									SIM_No=a.SIM_No,
									SIM_Operator=a.SIM_Operator,
									Asset_Type_Name=b.asset_name



									




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


	}
}
