namespace smartcityapi.Model.pages
{
	public class PageGetViewDTO
	{
		public long pk_page_id { get; set; }
		public long fk_module_id { get; set; }
		public string page_name { get; set; }
		public string page_url { get; set; }
		public string page_icon { get; set; }
		public int page_order { get; set; }
		public int is_active { get; set; }
		public int is_deleted { get; set; }

		public string? module_name { get; set; }

	}
}
