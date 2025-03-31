using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.UserMgt
{
	public class StatusUserRequestDTO
	{
		[Required]
		public long pk_user_id { get; set; }
		[Required]
		public int is_active { get; set; }
	}
}
