using Cysharp.Threading.Tasks;

namespace UIService.Runtime.Core
{
    public abstract class InitializablePresenter : Presenter
    {
        public abstract UniTask Initialize(IPresenterData data = null);
    }
}