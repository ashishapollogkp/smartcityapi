using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.Department
{
	public class RoleDeleteRequestDTO
	{
		[Required]
		public long Role_Id { get; set; }
		[Required]
		public long Department_Id { get; set; }
		
	}
}
