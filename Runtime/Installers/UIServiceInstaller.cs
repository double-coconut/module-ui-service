using UIService.Runtime.Presenter;
using UIService.Runtime.Utilities;
using Zenject;

namespace UIService.Runtime.Installers
{
    public class UIServiceInstaller : Installer<UIServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PresenterLoader>().AsSingle().NonLazy();
            Container.Bind<PresenterService>().AsSingle().NonLazy();
        }
    }
}