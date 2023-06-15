using System;
using UIService.Runtime.Presenter;

namespace UIService.Runtime.Hud.Top
{
    public class TopPanelController : IDisposable
    {
        private readonly PresenterService _presenterService;
        private readonly HudController _hud;

        public TopPanelController(PresenterService presenterService, HudController hud)
        {
            _presenterService = presenterService;
            _hud = hud;
        }


        public void Dispose()
        {
        }
    }
}