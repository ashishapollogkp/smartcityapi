using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.user
{
	public class GetModuleDTO
	{
		[Required]
		public long EmpId { get; set; }
	}
}
