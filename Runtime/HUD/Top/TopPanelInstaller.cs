using Zenject;

namespace UIService.Runtime.Hud.Top
{
    public class TopPanelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TopPanelController>().AsSingle().NonLazy();
        }
    }
}