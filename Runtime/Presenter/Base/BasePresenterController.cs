using System;

namespace UIService.Runtime.Presenter.Base
{
    public class BasePresenterController : IPresenterController, IDisposable
    {
        protected readonly PresenterService PresenterService;

        public BasePresenterController(PresenterService presenterService)
        {
            PresenterService = presenterService;
        }

        public virtual void Close()
        {
            PresenterService.Hide();
        }

        public virtual void Dispose()
        {
        }
    }
}