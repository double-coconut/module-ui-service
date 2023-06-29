using Cysharp.Threading.Tasks;
using UIService.Runtime.Core;
using Zenject;

namespace UIService.Runtime.Presenter.Base
{
    public abstract class BasePresenterWithController<TController> : BasePresenter
        where TController : IPresenterController
    {
        protected TController Controller;

        [Inject]
        private void Inject(TController controller)
        {
            Controller = controller;
        }
    }

    public abstract class BasePresenterWithController<TData, TController> : BasePresenterWithController<TController>
        where TData : IPresenterData
        where TController : IPresenterController
    {
        protected TData PresenterData;

        public override UniTask Initialize(IPresenterData data = null)
        {
            var castedData = (TData) data;
            if (castedData == null)
            {
                throw new IPresenterData.WrongData<TData>(GetType());
            }

            PresenterData = castedData;
            InternalInit();
            return UniTask.CompletedTask;
        }

        protected abstract void InternalInit();
    }

    public abstract class BasePresenterWithController<TData, TController, TFactory> : BasePresenter
        where TData : IPresenterData
        where TController : IPresenterController
        where TFactory : PlaceholderFactoryBase<TController>
    {
        protected TData PresenterData;
        protected TController Controller;
        protected TFactory Factory;

        [Inject]
        private void Inject(TFactory factory)
        {
            Factory = factory;
        }

        public override UniTask Initialize(IPresenterData data = null)
        {
            var castedData = (TData) data;
            if (castedData == null)
            {
                throw new IPresenterData.WrongData<TData>(GetType());
            }

            PresenterData = castedData;
            InternalInit();
            return UniTask.CompletedTask;
        }

        protected abstract void InternalInit();
    }
}