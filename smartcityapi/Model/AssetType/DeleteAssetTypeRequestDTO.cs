using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.AssetType
{
	public class DeleteAssetTypeRequestDTO
	{
		[Required]
		public long Asset_Type_Id { get; set; }
		[Required]
		public long Department_Id { get; set; }
		
	}
}
