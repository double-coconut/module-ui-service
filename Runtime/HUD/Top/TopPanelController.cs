using System;
using Services.UIService.Presenter;

namespace Services.UIService.HUD.Top
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