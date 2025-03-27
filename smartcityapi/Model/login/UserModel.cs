namespace smartcityapi.Model.login
{
	public class UserModel
	{
		public long EmpId { get; set; }
		public string Name { get; set; }
		public string? Email { get; set; }
		public string Role { get; set; }
		public long DeptId { get; set; }
		public string UserName { get; set; }
		public string Result { get; set; }
		public string HomePage { get; set; }
		public string UserType { get; set; }
	}
}
