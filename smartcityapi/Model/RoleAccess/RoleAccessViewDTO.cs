namespace smartcityapi.Model.RoleAccess
{
	public class RoleAccessViewDTO
	{
		public long fk_role_id { get; set; }
		public long fk_module_id { get; set; }
		public long fk_page_id { get; set; }
		public int is_view { get; set; }
		public int is_add { get; set; }
		public int is_update { get; set; }
		public int is_delete { get; set; }
		public int is_export { get; set; }	
		public string Role_Name { get; set; }
		public string Module_Name { get; set; }
		public string Page_Name { get; set; }



	}
}
