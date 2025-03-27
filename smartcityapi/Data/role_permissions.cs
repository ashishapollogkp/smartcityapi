namespace smartcityapi.Data
{
	public class role_permissions
	{
		public long fk_role_id { get; set; }
		public long fk_module_id { get; set; }
		public long fk_page_id { get; set; }
		public int is_view { get; set; }
		public int is_add { get; set; }
		public int is_update { get; set; }
		public int is_delete { get; set; }
		public int is_export { get; set; }
		public int created_by { get; set; }
		public DateTime created_date { get; set; }
	}
}
