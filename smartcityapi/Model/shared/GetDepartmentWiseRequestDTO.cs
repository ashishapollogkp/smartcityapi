using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.shared
{
	public class GetDepartmentWiseRequestDTO
	{
		[Required]
		public long Department_Id { get; set; }
	}
}
