using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.Department
{
	public class DepartmentGetViewDTO
	{
		public long Department_id { get; set; }
		
		public string Department_Name { get; set; }
		public int is_active { get; set; }
		public int is_deleted { get; set; }
	}
}
