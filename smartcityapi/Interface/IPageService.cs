using smartcityapi.Model.commanResponce;
using smartcityapi.Model.module;
using smartcityapi.Model.pages;

namespace smartcityapi.Interface
{
	public interface IPageService
	{
		
		Task<ApiResponse> GetPageListAsync(GetPagesRequestDTO request);
		Task<ApiResponse> AddEditPageAsync(PagesRequestDTO request);		
		Task<ApiResponse> DeletePageAsync(PageDeleteRequestDTO request);
	}
}
