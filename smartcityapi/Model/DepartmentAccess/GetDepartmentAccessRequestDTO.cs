using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.DepartmentAccess
{
	public class GetDepartmentAccessRequestDTO
	{
		[Required]
		public long Department_Id { get; set; }
	}
}
