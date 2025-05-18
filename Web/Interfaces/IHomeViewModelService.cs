using Web.ViewModels.Home;

namespace Web.Interfaces
{
    public interface IHomeViewModelService
    {
        Task<IndexViewModel> IndexCatalogItems(IndexViewModel catalogModel, int? pageId);
    }
}
