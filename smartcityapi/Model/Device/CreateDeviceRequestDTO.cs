namespace smartcityapi.Model.Device
{
	public class CreateDeviceRequestDTO
	{
		//public long Pk_Device_Id { get; set; }
		public long Device_Type_Id { get; set; }
		public string Device_Name { get; set; }
		public string Device_Model { get; set; }
		public string Device_No { get; set; }
		public string Device_IMEI { get; set; }
		public string Device_Vender { get; set; }
		public DateTime Device_Manufacture_Date { get; set; }
		public DateTime Device_Expiry_Date { get; set; }
		public string SIM_No { get; set; }
		public string SIM_Operator { get; set; }

		public long Department_Id { get; set; }

	}
}
