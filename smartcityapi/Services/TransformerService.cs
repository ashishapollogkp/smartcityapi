using Microsoft.EntityFrameworkCore;
using smartcityapi.Context;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;
using smartcityapi.Model.transformer_dashboard;
using System.Net;

namespace smartcityapi.Services
{
	public class TransformerService: ITransformerService
	{
		private readonly SmartCityDBContext _sqlDBContext;
		protected ApiResponse _response;
		protected UserService _userService;
		public TransformerService(SmartCityDBContext sqlDBContext, UserService userService)
		{
			_sqlDBContext = sqlDBContext;
			_response = new();
			_userService = userService;
		}


		public async Task<ApiResponse> GetTransformerDashboard()
		{
			var dept_id = _userService.GetUserDeptId();
			var role_id = _userService.GetUserRoleId();

			var levelvalue = await _sqlDBContext.master_role
				.Where(x => x.pk_role_id == role_id && x.fk_department_id == dept_id)
				.FirstOrDefaultAsync(); // Added async

			TransformerDataViewDTO result = new TransformerDataViewDTO();

			try
			{
				// Fetch ShortList
				var shortList = await _sqlDBContext.Master_Device
					.Where(a => a.is_deleted == 0 && a.Fk_Department_Id == dept_id)
					.OrderBy(a => a.Device_Name)
					.Select(a => new Transformer_Shoftlist
					{
						Device_Model = a.Device_Model,
						Device_Name = a.Device_Name,
						Id = a.Pk_Device_Id
					})
					.AsNoTracking()
					.ToListAsync();

				// Fetch MapList
				var mapList = await (from a in _sqlDBContext.Master_Device
									 join b in _sqlDBContext.tbl_last_eventdata on a.Pk_Device_Id equals b.fk_device_id into moduleJoin
									 from b in moduleJoin.DefaultIfEmpty()
									 where a.is_deleted == 0 && a.Fk_Department_Id == dept_id
									 orderby a.Device_Name
									 select new Transformer_maplist
									 {
										 Device_Model = a.Device_Model,
										 Device_Name = a.Device_Name,
										 Id = a.Pk_Device_Id,
										 latitude = b.latitude,
										 longitude = b.longitude
									 })
									 .AsNoTracking()
									 .ToListAsync();

				// Fetch LongList
				var longList = await (from a in _sqlDBContext.Master_Device
									  join b in _sqlDBContext.tbl_last_eventdata on a.Pk_Device_Id equals b.fk_device_id into moduleJoin
									  from b in moduleJoin.DefaultIfEmpty()
									  where a.is_deleted == 0 && a.Fk_Department_Id == dept_id
									  orderby a.Device_Name
									  select new Transformer_Longlist
									  {
										  Device_Model = a.Device_Model,
										  Device_Name = a.Device_Name,
										  Id = a.Pk_Device_Id,
										  latitude = b.latitude,
										  longitude = b.longitude,
										  ambient_temp = b.ambient_temp,
										  bushing_temp = b.bushing_temp,
										  current_B = b.current_B,
										  current_R = b.current_R,
										  current_Y = b.current_Y,
										  energy_R = b.energy_R,
										  energy_B = b.energy_B,
										  energy_Y = b.energy_Y,
										  frequency = b.frequency,
										  oil_level = b.oil_level,
										  oil_temp = b.oil_temp,
										  power = b.power,
										  tap_changer_temp = b.tap_changer_temp,
										  voltage_BR = b.voltage_BR,
										  voltage_RY = b.voltage_RY,
										  voltage_YB = b.voltage_YB,
										  winding_temp = b.winding_temp
									  })
									  .AsNoTracking()
									  .ToListAsync();

				// Assign to result
				result.ShortList = shortList;
				result.MapList = mapList;
				result.LongList = longList;

				// Return successful response
				_response.Result = result;
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
				_response.ActionResponse = "Data fetched successfully";

				return _response;
			}
			catch (Exception ex)
			{
				// Log exception here if needed
				_response.ActionResponse = "Error fetching transformer data";
				_response.Result = null;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				_response.IsSuccess = false;

				return _response;
			}
		}




		//public async Task<ApiResponse> GetTransformerDashboard()
		//{


		//	var dept_id = _userService.GetUserDeptId();
		//	var role_id = _userService.GetUserRoleId();

		//	var levelvalue = _sqlDBContext.master_role.Where(x => x.pk_role_id == role_id && x.fk_department_id == dept_id).FirstOrDefault();


		//	TransformerDataViewDTO result = new TransformerDataViewDTO();
		//	try
		//	{

		//		List<Transformer_Shoftlist> shortList = await (from a in _sqlDBContext.Master_Device
		//													   where a.is_deleted == 0 && a.Fk_Department_Id == dept_id
		//													   orderby a.Device_Name
		//													   select new Transformer_Shoftlist
		//													   {
		//														   Device_Model = a.Device_Model,
		//														   Device_Name = a.Device_Name,
		//														   Id = a.Pk_Device_Id,
		//													   })
		//				   .AsNoTracking() // ✅ Improves performance
		//				   .ToListAsync();

		//		List< Transformer_maplist > maplists= await (from a in _sqlDBContext.Master_Device

		//														 join b in _sqlDBContext.tbl_last_eventdata
		//														 on a.Pk_Device_Id equals b.fk_device_id into moduleJoin
		//														 from b in moduleJoin.DefaultIfEmpty() // ✅ Left Join to prevent null reference issues



		//													 where a.is_deleted == 0 && a.Fk_Department_Id == dept_id
		//													 orderby a.Device_Name
		//													 select new Transformer_maplist
		//													 {
		//														 Device_Model = a.Device_Model,
		//														 Device_Name = a.Device_Name,
		//														 Id = a.Pk_Device_Id,
		//														 latitude=b.latitude,
		//														 longitude=b.longitude

		//													 })
		//				   .AsNoTracking() // ✅ Impoves performance
		//				   .ToListAsync();


		//		List<Transformer_Longlist> LongList = await (from a in _sqlDBContext.Master_Device

		//													join b in _sqlDBContext.tbl_last_eventdata
		//													on a.Pk_Device_Id equals b.fk_device_id into moduleJoin
		//													from b in moduleJoin.DefaultIfEmpty() // ✅ Left Join to prevent null reference issues



		//													where a.is_deleted == 0 && a.Fk_Department_Id == dept_id
		//													orderby a.Device_Name
		//													select new Transformer_Longlist
		//													{
		//														Device_Model = a.Device_Model,
		//														Device_Name = a.Device_Name,
		//														Id = a.Pk_Device_Id,
		//														latitude = b.latitude,
		//														longitude = b.longitude,
		//														ambient_temp=b.ambient_temp,
		//														bushing_temp=b.bushing_temp,
		//														current_B=b.current_B,
		//														current_R=b.current_R,
		//														current_Y=b.current_Y,
		//														energy_R=b.energy_R,
		//														energy_B=b.energy_B,
		//														energy_Y=b.energy_Y,
		//														frequency=b.frequency,
		//														oil_level=b.oil_level,
		//														oil_temp=b.oil_temp,
		//														power= b.power,
		//														tap_changer_temp=b.tap_changer_temp,

		//														voltage_BR=b.voltage_BR,

		//														voltage_RY=b.voltage_RY,
		//														voltage_YB=b.voltage_YB,
		//														winding_temp = b.winding_temp																											
		//													})
		//				   .AsNoTracking() // ✅ Improves performance
		//				   .ToListAsync();




		//		result.LongList = LongList;
		//		result.MapList = maplists;
		//		result.ShortList = shortList;







		//		if (result!=null)
		//		{
		//			_response.Result = result;
		//			_response.StatusCode = HttpStatusCode.OK;
		//			_response.IsSuccess = true;

		//		}
		//		else
		//		{
		//			_response.ActionResponse = "No  Data";
		//			_response.Result = null;
		//			_response.StatusCode = HttpStatusCode.NoContent;
		//			_response.IsSuccess = false;

		//		}
		//		return _response;



		//	}
		//	catch (Exception e)
		//	{
		//		_response.ActionResponse = "No  Data";
		//		_response.Result = null;
		//		_response.StatusCode = HttpStatusCode.NoContent;
		//		_response.IsSuccess = false;


		//	}

		//	return _response;
		//}


	}
}
