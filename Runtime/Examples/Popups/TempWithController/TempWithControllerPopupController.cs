using Cysharp.Threading.Tasks;
using UIService.Runtime.Presenter;
using UIService.Runtime.Presenter.Base;

namespace UIService.Runtime.Examples.Popups.TempWithController
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