using Zenject;

namespace Services.UIService.HUD.Bottom
{
    public class BottomPanelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BottomPanelController>().AsSingle().NonLazy();
        }
    }
}