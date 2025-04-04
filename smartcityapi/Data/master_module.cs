using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Data
{
	public class master_module
	{
		[Key]
		public long pk_module_id { get; set; }
		public string module_name { get; set; }
		public string module_url { get; set; }
		public string? module_icon { get; set; }
		public int? module_order { get; set; }
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
