namespace smartcityapi.Model.AssetType
{
	public class GetAssetTypeViewDTO
	{
		public long Asset_Type_Id { get; set; }
		public long Department_Id { get; set; }
		public string Asset_Name { get; set; }
		public string Asset_Description { get; set; }
		public int is_active { get; set; }
		public int is_deleted { get; set; }
		
	}
}
