using Microsoft.EntityFrameworkCore;
using smartcityapi.Context;
using smartcityapi.Data;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;
using smartcityapi.Model.UserMgt;
using System.Net;

namespace smartcityapi.Services
{
	public class UserMgtService : IUserMgtService
	{
		private readonly SmartCityDBContext _sqlDBContext;
		protected ApiResponse _response;
		protected UserService _userService;
		public UserMgtService(SmartCityDBContext sqlDBContext, UserService userService)
		{
			_sqlDBContext = sqlDBContext;
			_response = new();
			_userService = userService;
		}

		public async Task<ApiResponse> CreateUserAsync(CreateUserRequestDTO request)
		{
			
				// Check if the page name already exists in the module
				bool exists = await _sqlDBContext.master_user
					.AnyAsync(x => x.login_id == request.login_id && x.fk_department_id == request.fk_department_id && x.is_deleted == 0);

				if (exists)
				{
					return new ApiResponse
					{
						StatusCode = HttpStatusCode.Conflict,
						ActionResponse = "Duplicate Login Id !",
						IsSuccess = false
					};
				}




				// Create new page
				var newPage = new master_user
				{
					
					email=request.email,
					fk_department_id=request.fk_department_id,
					fk_role_id=request.fk_role_id,
					login_id=request.login_id,
					mobile_no=request.mobile_no,
					parent_id=0,
					password=request.password,					
					user_name=request.user_name,
					password_enc=request.password,

					is_active = 1,
					is_deleted = 0,
					created_by = 1,
					created_date = DateTime.Now,
					

				};

				await _sqlDBContext.master_user.AddAsync(newPage);
				await _sqlDBContext.SaveChangesAsync();

				return new ApiResponse
				{
					ActionResponse = "Record Saved",
					StatusCode = HttpStatusCode.OK,
					IsSuccess = true
				};

		
		}



		public async Task<ApiResponse> UpdateUserAsync(UpdateUserRequestDTO request)
		{
			if (request.pk_user_id > 0) // Insert mode
			{


				// Find the existing page
				var existingPage = await _sqlDBContext.master_user.FindAsync(request.pk_user_id);
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
				existingPage.email = request.email;
				existingPage.password = request.password;
				existingPage.password_enc = request.password;
				existingPage.fk_department_id = request.fk_department_id;
				existingPage.fk_role_id = request.fk_role_id;
				existingPage.mobile_no = request.mobile_no;
				existingPage.user_name = request.user_name;

				existingPage.last_updated_by = 0;
				existingPage.last_updated_date = DateTime.Now;

				_sqlDBContext.master_user.Update(existingPage);
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

		public async Task<ApiResponse> DeleteUserAsync(DeleteUserRequestDTO request)
		{
			if (request.pk_user_id > 0) // Insert mode
			{


				// Find the existing page
				var existingPage = await _sqlDBContext.master_user.FindAsync(request.pk_user_id);
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
				

				existingPage.is_deleted = 1;
				existingPage.deleted_by = 0;
				existingPage.deleted_date = DateTime.Now;

				_sqlDBContext.master_user.Update(existingPage);
				await _sqlDBContext.SaveChangesAsync();

				return new ApiResponse
				{
					StatusCode = HttpStatusCode.OK,
					ActionResponse = "Data Deleted",
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



		public async Task<ApiResponse> StatusUserAsync(StatusUserRequestDTO request)
		{
			if (request.pk_user_id > 0) // Insert mode
			{


				// Find the existing page
				var existingPage = await _sqlDBContext.master_user.FindAsync(request.pk_user_id);
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


				existingPage.is_active = request.is_active;
				existingPage.last_updated_by = 0;
				existingPage.last_updated_date = DateTime.Now;

				_sqlDBContext.master_user.Update(existingPage);
				await _sqlDBContext.SaveChangesAsync();

				if (request.is_active == 0)
				{
					return new ApiResponse
					{

						StatusCode = HttpStatusCode.OK,
						ActionResponse = "User Deactivated",
						IsSuccess = true
					};
				}
				else {
					return new ApiResponse
					{

						StatusCode = HttpStatusCode.OK,
						ActionResponse = "User Activated",
						IsSuccess = true
					};

				}
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




		public async Task<ApiResponse> GetUserListAsync(GetUserListRequestDTO request)
		{


			List<GetUserListViewDTO> result = null;
			try
			{
				result = await (from a in _sqlDBContext.master_user

								join b in _sqlDBContext.master_department
									on a.fk_department_id equals b.pk_department_id into DeptJoin
								from b in DeptJoin.DefaultIfEmpty() // ✅ Left Join to prevent null reference issues


								join r in _sqlDBContext.master_role
									on a.fk_role_id equals r.pk_role_id into RoleJoin
								from r in RoleJoin.DefaultIfEmpty() // ✅ Left Join to prevent null reference issues



								where a.is_deleted == 0 && a.fk_department_id == request.fk_department_id  && (request.fk_role_id == 0 || a.fk_role_id == request.fk_role_id)
								orderby r.role_name
								select new GetUserListViewDTO
								{
									department_name=b.department_name,
									email=a.email,
									fk_department_id=a.fk_department_id,
									fk_role_id=a.fk_role_id,
									is_active=a.is_active,
									is_deleted=a.is_deleted,
									login_id=a.login_id,
									mobile_no=a.mobile_no,
									password=a.password,
									password_enc=a.password_enc,
									pk_user_id=a.pk_user_id,
									role_name	=r.role_name,
									user_name=a.user_name
									
									


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
