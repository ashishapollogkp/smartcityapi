using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.AssetType
{
	public class UpdateAssetTypeRequestDTO
	{
		[Required]
		public long Asset_Type_Id { get; set; }
		[Required]
		public long Department_Id { get; set; }
		[Required]
		public string Asset_Name { get; set; }
		public string Asset_Description { get; set; }
		
	}
}
