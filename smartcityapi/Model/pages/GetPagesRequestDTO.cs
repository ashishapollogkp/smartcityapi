using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.pages
{
	public class GetPagesRequestDTO
	{
		[Required]
		public long Module_Id { get; set; }
	}
}
