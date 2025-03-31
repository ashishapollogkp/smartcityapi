using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Data
{
	public class Master_Device
	{
		[Key]
		public long Pk_Device_Id { get; set; }
		public long Fk_Device_Type_Id { get; set; }
		public long Fk_Department_Id { get; set; }
		public string Device_Name { get; set; }
		public string Device_Model { get; set; }
		public string Device_No { get; set; }
		public string Device_IMEI { get; set; }
		public string Device_Vender { get; set; }
		public DateTime Device_Manufacture_Date { get; set; }
		public DateTime Device_Expiry_Date { get; set; }
		public string SIM_No { get; set; }
		public string SIM_Operator { get; set; }
		public int is_active { get; set; }
		public int is_deleted { get; set; }
		public int? created_by { get; set; }
		public DateTime? created_date { get; set; }
		public int? last_updated_by { get; set; }
		public DateTime? last_updated_date { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? deleted_date { get; set; }
	}
}
