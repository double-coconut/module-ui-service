using System;
using UIService.Runtime.Core;
using R3;
using UnityEngine;
using Zenject;

namespace UIService.Runtime.Hud
{
    public class PresenterHud : MonoBehaviour, IInitializable, IDisposable
    {
        private CompositeDisposable _disposable;
        private HudController _hudController;

        [Inject]
        private void Inject(HudController hudController)
        {
            _hudController = hudController;
        }

        protected virtual void Awake() => DontDestroyOnLoad(gameObject);

        protected void OnDestroy() => Dispose();


        public void Initialize()
        {
            _disposable = new CompositeDisposable();
            _hudController.PanelShowObservable.Subscribe(OnPanelShow).AddTo(_disposable);
        }

        private void OnPanelShow(InitializablePresenter panel)
        {
            panel.transform.SetParent(transform, false);
        }
        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}