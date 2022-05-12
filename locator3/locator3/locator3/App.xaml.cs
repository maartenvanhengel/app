using locator3.Models;
using locator3.Repositories;
using locator3.ViewModels;
using locator3.Views;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace locator3
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterForNavigation<BattlePage, BattlePageViewModel>();
            containerRegistry.RegisterForNavigation<mapsPage, mapsPageViewModel>();
            containerRegistry.RegisterForNavigation<BluetoothPage, BluetoothPageViewModel>();
            containerRegistry.RegisterForNavigation<UpgradePage, UpgradePageViewModel>();
            containerRegistry.RegisterForNavigation<SetLocationPage, SetLocationPageViewModel>();

            containerRegistry.Register<IGameRepository<Game>, fireBaseRepository>();
            containerRegistry.RegisterSingleton<GamesContext>();
            containerRegistry.RegisterForNavigation<ChooseDragon, ChooseDragonViewModel>();
        }
    }
}
