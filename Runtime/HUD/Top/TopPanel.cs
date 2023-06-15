using Cysharp.Threading.Tasks;
using DG.Tweening;
using Services.UIService.Core;
using Services.UIService.Utilities;
using UnityEngine;
using Zenject;

namespace Services.UIService.HUD.Top
{
    public class TopPanel : InitializableView, IViewData
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform baseHolder;


        private TopPanelController _controller;
        private Sequence _displaySequence;


        [Inject]
        private void Inject(TopPanelController controller)
        {
            _controller = controller;
        }

        public override void Initialize(IViewData data = null)
        {
            baseHolder.FitInSafeArea(FitmentType.Horizontal);
        }

        public override UniTask Show()
        {
            var scaleSource = new UniTaskCompletionSource();
            baseHolder.anchoredPosition = new Vector2(0, (baseHolder.rect.height / 2 + 50) * canvas.scaleFactor);
            _displaySequence?.Kill();
            _displaySequence = DOTween.Sequence();
            _displaySequence.Append(baseHolder.DOAnchorPosY(0, 0.7f).SetEase(Ease.OutCirc));
            _displaySequence.OnComplete(() =>
            {
                scaleSource?.TrySetResult();
                _displaySequence = null;
            });
            _displaySequence.OnKill(() =>
            {
                scaleSource?.TrySetResult();
                _displaySequence = null;
            });
            return scaleSource.Task;
        }

        public override UniTask Hide()
        {
            var scaleSource = new UniTaskCompletionSource();
            baseHolder.anchoredPosition = Vector2.zero;
            float posY = (baseHolder.rect.height / 2 + 50) * canvas.scaleFactor;

            _displaySequence?.Kill();
            _displaySequence = DOTween.Sequence();
            _displaySequence.Append(baseHolder.DOAnchorPosY(posY, 0.3f).SetEase(Ease.OutCirc));
            _displaySequence.OnComplete(() =>
            {
                scaleSource?.TrySetResult();
                _displaySequence = null;
            });
            _displaySequence.OnKill(() =>
            {
                scaleSource?.TrySetResult();
                _displaySequence = null;
            });
            return scaleSource.Task;
        }
        
        public override void Disable()
        {
            gameObject.SetActive(false);
        }
        
        public override void Enable()
        {
            gameObject.SetActive(true);
        }
    }
}