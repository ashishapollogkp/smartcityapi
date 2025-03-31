using System.Security.Claims;

namespace smartcityapi.helperServices
{
	public class UserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public long GetUserId()
		{
			var user = _httpContextAccessor.HttpContext?.User;
			var userIdString = user?.FindFirst("Id")?.Value;
			return long.TryParse(userIdString, out long userId) ? userId : 0;
		}

		public string GetUserName()
		{
			return _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value;
		}

		public string GetUserEmail()
		{
			return _httpContextAccessor.HttpContext?.User?.FindFirst("Email")?.Value;
		}

		public long GetUserRoleId()
		{
			var roleIdString = _httpContextAccessor.HttpContext?.User?.FindFirst("RoleId")?.Value;
			return long.TryParse(roleIdString, out long roleId) ? roleId : 0;
		}

		public long GetUserDeptId()
		{
			var DeptIdString = _httpContextAccessor.HttpContext?.User?.FindFirst("DeptId")?.Value;
			return long.TryParse(DeptIdString, out long DeptId) ? DeptId : 0;
		}
	}
}
