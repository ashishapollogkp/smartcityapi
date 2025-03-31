using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.AssetType
{
	public class CreateAssetTypeRequestDTO
	{

		[Required]
		public long Department_Id { get; set; }
		[Required]
		public string Asset_Name { get; set; }
		public string Asset_Description { get; set; }
		
	}
}
