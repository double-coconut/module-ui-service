using Cysharp.Threading.Tasks;
using Services.UIService.Presenter;
using Services.UIService.Presenter.Base;

namespace Services.UIService.Examples.Popups.TempWithController
{
    public class TempWithControllerPopupController : BasePresenterController
    {
        //Inject Some services here
        public TempWithControllerPopupController(PresenterService presenterService) : base(presenterService)
        {
        }

        public async UniTask Do()
        {
           //Do Something....
        }
    }
}