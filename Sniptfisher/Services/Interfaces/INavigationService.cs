
namespace Sniptfisher.Services.Interfaces
{
    public interface INavigationService
    {
        void NavigateTo<TDestinationViewModel>();
        void NavigateTo<TDestinationViewModel>(object navigationContext);
        void NavigateBack();
        void NavigateBack(object navigationContext);
        void ClearNavigationHistory();
    }
}
