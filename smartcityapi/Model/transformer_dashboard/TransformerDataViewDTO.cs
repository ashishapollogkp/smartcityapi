namespace smartcityapi.Model.transformer_dashboard
{
	public class TransformerDataViewDTO
	{
		
			public List<Transformer_Shoftlist> ShortList { get; set; }
			public List<Transformer_maplist> MapList { get; set; }
			public List<Transformer_Longlist> LongList { get; set; }
		
	}


	public class Transformer_Shoftlist
	{
		public long Id { get; set; }
		public string Device_Name { get; set; }
		public string Device_Model { get; set; }
		public string Power { get; set; }

	}

	public class Transformer_maplist
	{
		public long Id { get; set; }
		public string Device_Name { get; set; }
		public string Device_Model { get; set; }
		public double? latitude { get; set; }
		public double? longitude { get; set; }

	}


	public class Transformer_Longlist
	{
		public long Id { get; set; }

		public string Device_Name { get; set; }
		public string Device_Model { get; set; }
		public double? latitude { get; set; }
		public double? longitude { get; set; }
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
