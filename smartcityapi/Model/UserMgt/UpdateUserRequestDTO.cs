namespace smartcityapi.Model.UserMgt
{
	public class UpdateUserRequestDTO
	{
		public long pk_user_id { get; set; }
		public long fk_role_id { get; set; }
		public long fk_department_id { get; set; }
		public string user_name { get; set; }		
		public string password { get; set; }
		public string mobile_no { get; set; }
		public string email { get; set; }
	}
}
