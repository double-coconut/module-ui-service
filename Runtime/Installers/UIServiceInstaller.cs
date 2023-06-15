using Services.UIService.Presenter;
using Services.UIService.Utilities;
using Zenject;

namespace Services.UIService.Installers
{
    public class UIServiceInstaller : Installer<UIServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<LoaderService>().AsSingle().NonLazy();
            Container.Bind<PresenterService>().AsSingle().NonLazy();
        }
    }
}