using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Data
{
	public class master_module_pages
	{
		[Key]
		public long pk_page_id { get; set; }
		public long fk_module_id { get; set; }
		public string page_name { get; set; }
		public string page_url { get; set; }
		public string page_icon { get; set; }
		public string page_order { get; set; }
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
