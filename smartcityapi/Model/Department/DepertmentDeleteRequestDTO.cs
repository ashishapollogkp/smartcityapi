using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.Department
{
	public class DepertmentDeleteRequestDTO
	{
		[Required]
		public long Department_id { get; set; }
	}
}
