using smartcityapi.Model.AssetType;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Device;

namespace smartcityapi.Interface
{
	public interface IDeviceService
	{
		Task<ApiResponse> GetDeviceList(GetDeviceRequestDTO request);
		Task<ApiResponse> CreateDeviceType(CreateDeviceRequestDTO request);
		Task<ApiResponse> UpdateDeviceType(UpdateDeviceRequestDTO request);
		Task<ApiResponse> DeleteDeviceType(DeleteDeviceRequestDTO request);
	}
}
