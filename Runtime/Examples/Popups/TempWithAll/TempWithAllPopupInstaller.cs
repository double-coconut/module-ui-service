using Zenject;

namespace UIService.Runtime.Examples.Popups.TempWithAll
{
    public class TempWithAllPopupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<string, TempWithAllPopupController, TempWithAllPopupController.Factory>().AsSingle()
                .NonLazy();
        }
    }
}