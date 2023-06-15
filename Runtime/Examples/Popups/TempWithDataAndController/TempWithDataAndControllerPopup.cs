using System;
using UIService.Runtime.Core;
using UIService.Runtime.Presenter.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UIService.Runtime.Examples.Popups.TempWithDataAndController
{
    [RequireComponent(typeof(TempWithDataAndControllerPopupInstaller))]
    public class TempWithDataAndControllerPopup : BasePresenterWithController<
        TempWithDataAndControllerPopup.Data, TempWithDataAndControllerPopupController>
    {
        [SerializeField] private Button okButton;

        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();


        protected override void InternalInit()
        {
            okButton.OnClickAsObservable().Subscribe(OnOkButtonClicked).AddTo(_compositeDisposable);
        }

        private void OnOkButtonClicked(Unit unit)
        {
            PresenterData?.OnClose?.Invoke();
            Controller.Do();
        }

        public override void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public class Data : IViewData
        {
            public Action OnClose;
        }
    }
}