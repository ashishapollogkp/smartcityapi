namespace smartcityapi.Model.module
{
	public class moduleGetViewDTO
	{

		public long pk_module_id { get; set; }
		public string module_name { get; set; }
		public string module_url { get; set; }
		public string? module_icon { get; set; }
		public int? module_order { get; set; }
		public int is_active { get; set; }
		public int is_deleted { get; set; }
		

	}
}
