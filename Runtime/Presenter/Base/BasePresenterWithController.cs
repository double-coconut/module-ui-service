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
        where TData : IViewData
        where TController : IPresenterController
    {
        protected TData PresenterData;

        public override void Initialize(IViewData data = null)
        {
            var castedData = (TData) data;
            if (castedData == null)
            {
                throw new IViewData.WrongData<TData>(GetType());
            }

            PresenterData = castedData;
            InternalInit();
        }

        protected abstract void InternalInit();
    }

    public abstract class BasePresenterWithController<TData, TController, TFactory> : BasePresenter
        where TData : IViewData
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

        public override void Initialize(IViewData data = null)
        {
            var castedData = (TData) data;
            if (castedData == null)
            {
                throw new IViewData.WrongData<TData>(GetType());
            }

            PresenterData = castedData;
            InternalInit();
        }

        protected abstract void InternalInit();
    }
}