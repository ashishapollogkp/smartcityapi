using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.user
{
	public class GetModulePagesDTO
	{
		
		[Required]
		public long module_id { get; set; }
	}
}
