using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Data
{
	public class master_role
	{
		[Key]
		public long pk_role_id { get; set; }
		public long fk_department_id { get; set; }
		public string role_name { get; set; }
		public int is_active { get; set; }
		public int is_deleted { get; set; }
		public int? created_by { get; set; }
		public DateTime? created_date { get; set; }
		public int? last_updated_by { get; set; }
		public DateTime? last_updated_date { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? deleted_date { get; set; }
		public long parent_id { get; set; }
	}
}
