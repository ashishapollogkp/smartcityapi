using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.Department
{
	public class RoleRequestDTO
	{
		[Required]
		public long Role_Id { get; set; }
		[Required]
		public long Department_Id { get; set; }
		[Required]
		public string Role_Name { get; set; }

		public int? level_value { get; set; }


	}
}
