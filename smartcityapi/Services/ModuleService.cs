using Microsoft.EntityFrameworkCore;
using smartcityapi.Context;
using smartcityapi.Data;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.module;
using System.Net;

namespace smartcityapi.Services
{
	public class ModuleService : IModuleService
	{


		private readonly SmartCityDBContext _sqlDBContext;
		protected ApiResponse _response;
		protected UserService _userService;
		public ModuleService(SmartCityDBContext sqlDBContext,UserService userService)
		{
			_sqlDBContext = sqlDBContext;
			_response = new();
			_userService=userService;
		}


		public async Task<ApiResponse> CreateModuleAsync(ModuleAddRequestDTO request)
		{
			// Check if module name already exists
			bool exists = await _sqlDBContext.master_module
				.AnyAsync(x => x.module_name == request.Module_Name && x.is_deleted == 0);

			if (exists)
			{
				return new ApiResponse
				{
					StatusCode = HttpStatusCode.Conflict,
					ActionResponse = "Duplicate Data",
					IsSuccess = false
				};
			}

			// Create new module
			var newModule = new master_module
			{
				module_name = request.Module_Name,
				module_icon = request.Module_Icon,
				module_url = request.Module_URL,
				module_order = request.Module_Order,
				is_active = 1,
				is_deleted = 0,
				created_by = 1,
				created_date = DateTime.Now
			};

			await _sqlDBContext.master_module.AddAsync(newModule);
			await _sqlDBContext.SaveChangesAsync();

			return new ApiResponse
			{
				ActionResponse = "Record Saved",
				StatusCode = HttpStatusCode.OK,
				IsSuccess = true
			};
		}


		public async Task<ApiResponse> DeleteModuleAsync(ModuleDeleteRequestDTO request)
		{
			try
			{



				master_module updatedata = await _sqlDBContext.master_module.SingleOrDefaultAsync(x => x.pk_module_id == request.Module_Id);
				if (updatedata == null)
				{
					_response.StatusCode = HttpStatusCode.NoContent;
					_response.ActionResponse = "No Data";
					_response.IsSuccess = false;
				}
				else
				{



					updatedata.is_deleted = 1;
					updatedata.deleted_by = 0;
					updatedata.deleted_date = DateTime.Now;

					_sqlDBContext.master_module.Update(updatedata);
					await _sqlDBContext.SaveChangesAsync();

					_response.StatusCode = HttpStatusCode.OK;
					_response.ActionResponse = "Data Deleted";
					_response.IsSuccess = true;
				}

			}
			catch
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.ActionResponse = "Data Error";
				_response.IsSuccess = false;
			}

			return _response;
		}

		public Task<ApiResponse> GetModuleAsync(ModuleGetRequestDTO request)
		{
			throw new NotImplementedException();
		}

		public async Task<ApiResponse> GetModuleListAsync()
		{
			

			List<moduleGetViewDTO> result = null;
			try
			{

				
				

					result = await (from stop in _sqlDBContext.master_module
							  where stop.is_deleted == 0 
									
							  orderby stop.module_order
							  select new moduleGetViewDTO
							  {
								  is_active=stop.is_active,
								  is_deleted=stop.is_deleted,
								  module_icon=stop.module_icon,
								  module_order=stop.module_order,
								  module_name=stop.module_name,
								 module_url=stop.module_url,
								 pk_module_id=stop.pk_module_id,


							  }).ToListAsync();
				

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

		public async Task<ApiResponse> UpdateModuleAsync(ModuleEditRequestDTO request)
		{
			try
			{


				master_module updatedata = await _sqlDBContext.master_module.Where(x => x.pk_module_id != request.Module_Id && x.module_name == request.Module_Name && x.is_deleted == 0).FirstOrDefaultAsync();
				if (updatedata != null)
				{

					_response.StatusCode = HttpStatusCode.Conflict;
					_response.ActionResponse = "Duplicate Data";
					_response.IsSuccess = false;
				}
				else
				{
					updatedata = await _sqlDBContext.master_module.SingleOrDefaultAsync(x => x.pk_module_id == request.Module_Id);
					if (updatedata == null)
					{
						_response.StatusCode = HttpStatusCode.NoContent;
						_response.ActionResponse = "No Data";
						_response.IsSuccess = false;
					}
					else
					{
						updatedata.module_name = request.Module_Name;
						updatedata.module_icon = request.Module_Icon;
						updatedata.module_order = request.Module_Order;
						updatedata.module_url = request.Module_URL;
				




						updatedata.last_updated_by = 0;
						updatedata.last_updated_date = DateTime.Now;

						_sqlDBContext.master_module.Update(updatedata);
						await _sqlDBContext.SaveChangesAsync();

						_response.StatusCode = HttpStatusCode.OK;
						_response.ActionResponse = "Data Updated";
						_response.IsSuccess = true;
					}
				}
			}
			catch
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.ActionResponse = "Data Error";
				_response.IsSuccess = false;
			}

			return _response;
		}
	}
}
