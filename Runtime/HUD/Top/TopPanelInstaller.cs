using Zenject;

namespace Services.UIService.HUD.Top
{
    public class TopPanelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TopPanelController>().AsSingle().NonLazy();
        }
    }
}