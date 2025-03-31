using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.Department
{
	public class GetRoleRequestDTO
	{
		

		[Required]
		public long Department_Id { get; set; }

		

	}
}
