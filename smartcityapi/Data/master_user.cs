using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Data
{
	public class master_user
	{
		[Key]
		public long pk_user_id { get; set; }
		public long fk_role_id { get; set; }
		public long fk_department_id { get; set; }
		public int parent_id { get; set; }
		public string user_name { get; set; }
		public string login_id { get; set; }
		public string password { get; set; }
		public string password_enc { get; set; }
		public string mobile_no { get; set; }
		public string email { get; set; }
		public int is_active { get; set; }
		public int is_deleted { get; set; }
		public int? created_by { get; set; }
		public DateTime? created_date { get; set; }
		public int? last_updated_by { get; set; }
		public DateTime? last_updated_date { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? deleted_date { get; set; }
	}
}
