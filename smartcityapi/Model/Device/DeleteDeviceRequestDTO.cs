using System.ComponentModel.DataAnnotations;

namespace smartcityapi.Model.Device
{
	public class DeleteDeviceRequestDTO
	{
		[Required]
		public long Device_Id { get; set; }
			
	
	}
}
