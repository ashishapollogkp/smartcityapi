using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.DepartmentAccess
{
	public class DepartmentAccessDeleteRequestDTO
	{
		[Required]
		public long Department_Id { get; set; }
		[Required]
		public long Module_Id { get; set; }
	}
}
