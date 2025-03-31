using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.pages
{
	public class PagesRequestDTO
	{
		[Required]
		public long Page_Id { get; set; }
		[Required]
		public long Module_Id { get; set; }
		[Required]
		public string Page_Name { get; set; }
		[Required]
		public string Page_Url { get; set; }
		public string Page_Icon { get; set; }
		
		
	}
}
