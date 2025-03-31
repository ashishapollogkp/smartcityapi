using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.UserMgt
{
	public class DeleteUserRequestDTO
	{
		[Required]
		public long pk_user_id { get; set; }
	}
}
