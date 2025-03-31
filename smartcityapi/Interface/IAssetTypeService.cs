using smartcityapi.Model.AssetType;
using smartcityapi.Model.commanResponce;
using smartcityapi.Model.Department;

namespace smartcityapi.Interface
{
	public interface IAssetTypeService
	{
		Task<ApiResponse> GetAssetTypeList(GetAssetTypeRequestDTO request);
		Task<ApiResponse> CreateAssetType(CreateAssetTypeRequestDTO request);
		Task<ApiResponse> UpdateAssetType(UpdateAssetTypeRequestDTO request);
		Task<ApiResponse> DeleteAssetType(DeleteAssetTypeRequestDTO request);
	}
}
