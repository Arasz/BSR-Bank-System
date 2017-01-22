using Autofac;
using GalaSoft.MvvmLight.Views;
using System.Windows.Controls;

namespace Client.LightClient.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IComponentContext _componentContext;
        private readonly NavigationWindow _navigationWindow;
        private Frame _navigationFrame;

        public string CurrentPageKey { get; private set; }

        public NavigationService(NavigationWindow navigationWindow, IComponentContext componentContext)
        {
            _navigationWindow = navigationWindow;
            _componentContext = componentContext;
            _navigationFrame = _navigationWindow.NavigationFrame;
        }

        public void GoBack()
        {
            _navigationFrame.NavigationService.GoBack();
        }

        public void NavigateTo(string pageKey)
        {
            _navigationFrame.NavigationService.Navigate(ResolvePage(pageKey));
            CurrentPageKey = pageKey;
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            throw new System.NotImplementedException();
        }

        private object ResolvePage(string pageTypeName) => _componentContext.ResolveNamed<Page>(pageTypeName);
    }
}