using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;
using smartcityapi.Model.UserMgt;

namespace smartcityapi.Interface
{
	public interface IUserMgtService
	{


		Task<ApiResponse> GetUserListAsync(GetUserListRequestDTO request);

		Task<ApiResponse> CreateUserAsync(CreateUserRequestDTO request);
		Task<ApiResponse> UpdateUserAsync(UpdateUserRequestDTO request);

		Task<ApiResponse> DeleteUserAsync(DeleteUserRequestDTO request);


		Task<ApiResponse> StatusUserAsync(StatusUserRequestDTO request);
	}



}
