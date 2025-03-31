namespace smartcityapi.Model.UserMgt
{
	public class GetUserListViewDTO
	{
		public long pk_user_id { get; set; }
		public long fk_role_id { get; set; }
		public long fk_department_id { get; set; }		
		public string user_name { get; set; }
		public string login_id { get; set; }
		public string password { get; set; }
		public string password_enc { get; set; }
		public string mobile_no { get; set; }
		public string email { get; set; }
		public int is_active { get; set; }
		public int is_deleted { get; set; }


		public string role_name { get; set; }
		public string department_name { get; set; }

	}
}
