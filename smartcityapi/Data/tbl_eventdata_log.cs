using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Data
{
	public class tbl_eventdata_log
	{
		[Key]
		public long fk_device_id { get; set; }
		public DateTime time_stamp { get; set; }
		public int gps_status { get; set; }
		public double latitude { get; set; }
		public double longitude { get; set; }
		public string location { get; set; }
		public double heading { get; set; }
		public double speed { get; set; }
		public string vendor_code { get; set; }
		public int sat_count { get; set; }
		public double altitude { get; set; }
		public double pdop { get; set; }
		public double hdop { get; set; }
		public double vdop { get; set; }
		public string operator_name { get; set; }
		public int oil { get; set; }
		public int di1 { get; set; }
		public int di2 { get; set; }
		public int di3 { get; set; }
		public int di4 { get; set; }
		public int di5 { get; set; }
		public int di6 { get; set; }
		public int do1 { get; set; }
		public int do2 { get; set; }
		public int do3 { get; set; }
		public int do4 { get; set; }
		public int do5 { get; set; }
		public int do6 { get; set; }
		public double al1 { get; set; }
		public double al2 { get; set; }
		public double al3 { get; set; }
		public double al4 { get; set; }
		public int epc { get; set; }
		public double epv { get; set; }
		public double ipv { get; set; }
		public int sos { get; set; }
		public decimal temp_1 { get; set; }
		public decimal temp_2 { get; set; }
		public decimal temp_3 { get; set; }
		public decimal temp_4 { get; set; }
		public int tampor { get; set; }
		public string lbs_1 { get; set; }
		public int strength_1 { get; set; }
		public string lbs_2 { get; set; }
		public int strength_2 { get; set; }
		public string lbs_3 { get; set; }
		public int strength_3 { get; set; }
		public string lbs_4 { get; set; }
		public int strength_4 { get; set; }
		public string lbs_5 { get; set; }
		public int strength_5 { get; set; }
		public double odometer { get; set; }
		public string client_socket { get; set; }
		public string server_socket { get; set; }
		public double harsh_b { get; set; }
		public double hasrh_a { get; set; }
		public double sharp_turn { get; set; }
		public int packet_type { get; set; }
		public int packet_mode { get; set; }
		public string fw_version { get; set; }
		public DateTime creation_time { get; set; }
		public DateTime last_update_on { get; set; }
		public long time_stamp_unix { get; set; }

		public string voltage_RY { get; set; }
		public string voltage_YB { get; set; }
		public string voltage_BR { get; set; }
		public string current_R { get; set; }
		public string current_Y { get; set; }
		public string current_B { get; set; }
		public string energy_R { get; set; }
		public string energy_Y { get; set; }
		public string energy_B { get; set; }
		public string frequency { get; set; }
		public string winding_temp { get; set; }
		public string bushing_temp { get; set; }
		public string tap_changer_temp { get; set; }
		public string ambient_temp { get; set; }
		public string oil_temp { get; set; }
		public string oil_level { get; set; }
		public string power { get; set; }
	}
}
