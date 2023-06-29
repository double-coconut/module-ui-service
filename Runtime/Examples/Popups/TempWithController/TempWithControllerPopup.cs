using Cysharp.Threading.Tasks;
using UIService.Runtime.Core;
using UIService.Runtime.Presenter.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UIService.Runtime.Examples.Popups.TempWithController
{
    [RequireComponent(typeof(TempWithControllerPopupInstaller))]
    public class TempWithControllerPopup :  BasePresenterWithController<TempWithControllerPopupController>
    {
        [SerializeField] private Button someButton;
        public override UniTask Initialize(IPresenterData data = null)
        {
            someButton.onClick.AddListener(OnReLoginButtonClicked);
            return UniTask.CompletedTask;
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