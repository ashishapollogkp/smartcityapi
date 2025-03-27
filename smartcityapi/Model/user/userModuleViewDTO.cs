using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.user
{
	public class userModuleViewDTO
	{
		[Required]
		public long EmpId { get; set; }
	}
}
