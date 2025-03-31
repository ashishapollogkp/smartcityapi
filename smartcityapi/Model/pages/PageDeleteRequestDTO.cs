using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.pages
{
	public class PageDeleteRequestDTO
	{
		[Required]
		public long Page_Id { get; set; }
		[Required]
		public long Module_Id { get; set; }
		
	}
}
