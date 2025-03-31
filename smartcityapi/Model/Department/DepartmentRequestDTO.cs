using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.Department
{
	public class DepartmentRequestDTO
	{
		[Required]
		public long Department_id { get; set; }
		[Required]
		public string Department_Name { get; set; }
		
	}
}
