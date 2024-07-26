using System;
using UIService.Runtime.Presenter.Base;
using R3;
using UnityEngine;
using Zenject;

namespace UIService.Runtime.Presenter
{
    [RequireComponent(typeof(Canvas))]
    public class PresenterView : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private Canvas canvas;

        private PresenterService _presenterService;
        private CompositeDisposable _disposable;

        public Canvas Canvas => canvas;


        [Inject]
        private void Inject(PresenterService presenterService)
        {
            _presenterService = presenterService;
        }

        protected virtual void Awake() => DontDestroyOnLoad(gameObject);


        public void Initialize()
        {
            _disposable = new CompositeDisposable();
            _presenterService.ObserveNewPresenter.Subscribe(OnShowNewPopup).AddTo(_disposable);
        }

        private void OnShowNewPopup(BasePresenter presenter)
        {
            Debug.Log($"Show presenter: {presenter.name}");
            var presenterRect = presenter.GetComponent<RectTransform>();
            DontDestroyOnLoad(presenter.gameObject);
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


#if UNITY_EDITOR

        private void Reset()
        {
            canvas = GetComponent<Canvas>();
        }

#endif
    }
}