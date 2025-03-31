using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.Department
{
	public class GetDepartmentRequestDTO
	{
		[Required]
		public long Department_id { get; set; }
	}
}
