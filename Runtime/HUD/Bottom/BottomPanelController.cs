using System;
using UIService.Runtime.Presenter;
using R3;

namespace UIService.Runtime.Hud.Bottom
{
    public class BottomPanelController : IDisposable
    {
        private readonly PresenterService _presenterService;
        private readonly HudController _hudController;

        private readonly CompositeDisposable _disposable;

        public BottomPanelController(PresenterService presenterService, HudController hudController)
        {
            _disposable = new CompositeDisposable();
            _presenterService = presenterService;
            _hudController = hudController;
        }


        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}