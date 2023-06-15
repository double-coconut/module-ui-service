using System;
using Services.UIService.Presenter.Base;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Services.UIService.Presenter
{
    public class PresenterView : MonoBehaviour, IInitializable, IDisposable
    {
        private PresenterService _presenterService;
        private CompositeDisposable _disposable;

        [Inject]
        private void Inject(PresenterService presenterService)
        {
            _presenterService = presenterService;
        }

        protected virtual void Awake() => DontDestroyOnLoad(gameObject);


        public void Initialize()
        {
            _disposable = new CompositeDisposable();
            _presenterService.ObserveNewPresenter().Subscribe(OnShowNewPopup).AddTo(_disposable);
        }

        private void OnShowNewPopup(BasePresenter presenter)
        {
            //presenter.transform.SetParent(transform, false);
            var presenterRect = presenter.GetComponent<RectTransform>();
            presenterRect.SetParent(transform, false);
            presenterRect.anchorMin = new Vector2(0f, 0f);
            presenterRect.anchorMax = new Vector2(1f, 1f);
            presenterRect.offsetMin = new Vector2(0f, 0f);
            presenterRect.offsetMax = new Vector2(0f, 0f);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}