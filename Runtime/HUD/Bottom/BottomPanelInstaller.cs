using Zenject;

namespace UIService.Runtime.Hud.Bottom
{
    public class BottomPanelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BottomPanelController>().AsSingle().NonLazy();
        }
    }
}