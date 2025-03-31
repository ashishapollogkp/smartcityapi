namespace smartcityapi.Model.DepartmentAccess
{
	public class DepartmentAccessGetViewDTO
	{

		public long pk_dept_module_id { get; set; }
		public long Department_Id { get; set; }
		public long Module_Id { get; set; }
		public int Is_Active { get; set; }
		public int Is_Deleted { get; set; }
		public int? created_by { get; set; }
		public DateTime? created_date { get; set; }
		public int? last_updated_by { get; set; }
		public DateTime? last_updated_date { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? deleted_date { get; set; }

		public string? Department_Name { get; set; }
		public string? Module_Name { get; set; }
	}
}
