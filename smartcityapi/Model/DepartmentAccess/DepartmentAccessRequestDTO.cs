using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.DepartmentAccess
{
	public class DepartmentAccessRequestDTO
	{
		[Required]
		public long Department_Id { get; set; }
		[Required]
		public long Module_Id { get; set; }

		public int is_access { get; set; }
	}
}
