using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.AssetType
{
	public class GetAssetTypeRequestDTO
	{
		[Required]
		public long Department_Id { get; set; }
	
	}
}
