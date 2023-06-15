namespace UIService.Runtime.Core
{
    public abstract class InitializableView : View
    {
        public abstract void Initialize(IViewData data = null);
    }
}