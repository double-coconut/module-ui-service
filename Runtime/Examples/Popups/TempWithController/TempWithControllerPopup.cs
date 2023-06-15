using Services.UIService.Core;
using Services.UIService.Presenter.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Services.UIService.Examples.Popups.TempWithController
{
    [RequireComponent(typeof(TempWithControllerPopupInstaller))]
    public class TempWithControllerPopup :  BasePresenterWithController<TempWithControllerPopupController>
    {
        [SerializeField] private Button someButton;
        public override void Initialize(IViewData data = null)
        {
            someButton.onClick.AddListener(OnReLoginButtonClicked);
        }

        private async void OnReLoginButtonClicked()
        {
            await Controller.Do();
        }

        public override void Dispose()
        {
        }
    }
}