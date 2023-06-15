using Zenject;

namespace Services.UIService.Examples.Popups.TempWithController
{
    public class TempWithControllerPopupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TempWithControllerPopupController>().AsSingle().NonLazy();
        }
    }
}