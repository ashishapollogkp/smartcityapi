namespace smartcityapi.Model.RoleAccess
{
	public class RoleAccessRequestDTO
	{
		public long Role_Id { get; set; }
		public long Module_Id { get; set; }
		public long Page_Id { get; set; }
		public int Is_View { get; set; }
		public int Is_Add { get; set; }
		public int Is_Update { get; set; }
		public int Is_Delete { get; set; }
		public int Is_Export { get; set; }
		
	}
}
