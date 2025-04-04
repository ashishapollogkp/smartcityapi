using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.Device
{
	public class GetDeviceRequestDTO
	{
		[Required]
		public long Department_Id { get; set; }

		[Required]
		public long Device_Type_Id { get; set; }
	}
}
