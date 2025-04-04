using smartcityapi.Model.AssetType;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Device;

namespace smartcityapi.Interface
{
	public interface IDeviceService
	{
		Task<ApiResponse> GetDeviceList(GetDeviceRequestDTO request);
		Task<ApiResponse> CreateDevice(CreateDeviceRequestDTO request);
		Task<ApiResponse> UpdateDevice(UpdateDeviceRequestDTO request);
		Task<ApiResponse> DeleteDevice(DeleteDeviceRequestDTO request);
	}
}
