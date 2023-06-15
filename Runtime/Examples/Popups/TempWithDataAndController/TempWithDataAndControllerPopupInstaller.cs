using Zenject;

namespace UIService.Runtime.Examples.Popups.TempWithDataAndController
{
    public class TempWithDataAndControllerPopupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TempWithDataAndControllerPopupController>().AsSingle().NonLazy();
        }
    }
}