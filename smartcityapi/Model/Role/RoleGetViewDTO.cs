using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.Department
{
	public class RoleGetViewDTO
	{
	
		public long Role_Id { get; set; }
		public long Department_Id { get; set; }
		public string Role_Name { get; set; }
		public string Department_Name { get; set; }
		public int is_active { get; set; }
		public int is_deleted { get; set; }
	}
}
