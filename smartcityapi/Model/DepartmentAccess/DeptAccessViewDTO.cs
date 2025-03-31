namespace smartcityapi.Model.DepartmentAccess
{
	public class DeptAccessViewDTO
	{

		public long Department_Id { get; set; }
		public long pk_module_id { get; set; }
		public string module_name { get; set; }		
		public int is_access { get; set; }
		
	}
}
