using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.UserMgt
{
	public class GetUserListRequestDTO
	{
		[Required]
		public long fk_department_id { get; set; }
		[Required]
		public long fk_role_id { get; set; }
		
	}
}
