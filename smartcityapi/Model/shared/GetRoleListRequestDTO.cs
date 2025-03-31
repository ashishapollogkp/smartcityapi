using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.shared
{
	public class GetRoleListRequestDTO
	{
		[Required]
		public long Department_Id { get; set; }
	}
}
