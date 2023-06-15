using Zenject;

namespace Services.UIService.Examples.Popups.TempWithDataAndController
{
    public class TempWithDataAndControllerPopupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TempWithDataAndControllerPopupController>().AsSingle().NonLazy();
        }
    }
}