using TMPro;
using UIService.Runtime.Core;
using UIService.Runtime.Presenter.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UIService.Runtime.Examples.Popups.TempWithAll
{
    [RequireComponent(typeof(TempWithAllPopupInstaller))]
    public class TempWithAllPopup : BasePresenterWithController<TempWithAllPopup.Data, TempWithAllPopupController,
        TempWithAllPopupController.Factory>
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button showNameButton;
        [SerializeField] private Button showNextButton;
        [SerializeField] private TMP_Text nameText;

        protected override async void InternalInit()
        {
            Controller = Factory.Create(PresenterData.Name);
            showNameButton.onClick.AddListener(ShowName);
            showNextButton.onClick.AddListener(ShowNext);
            closeButton.onClick.AddListener(Close);
            dimmerButton.onClick.AddListener(Close);
        }

        private void ShowNext()
        {
            PresenterData.Name += "Next";
            Controller.ShowNextPopup(PresenterData);
        }

        private void Close()
        {
            Controller.Close();
        }

        private void ShowName()
        {
            string sName = Controller.GetName();
            nameText.text = sName;
        }

        public override void Dispose()
        {
        }

        public class Data : IPresenterData
        {
            public string Name;
        }
    }
}