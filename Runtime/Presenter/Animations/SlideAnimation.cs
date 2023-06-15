using DG.Tweening;
using Services.UIService.Presenter.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace Services.UIService.Presenter.Animations
{
    [CreateAssetMenu(fileName = "SlideAnimation", menuName = "UI Service/Animations/Slide", order = 0)]
    public class SlideAnimation : AbstractAnimation<SlideAnimation.InputValues>
    {
        public override Sequence PlayOnShow(BasePresenter presenter)
        {
            Vector2 anchorPos = presenter.PresenterHolder.anchoredPosition;
            presenter.PresenterHolder.anchoredPosition = data.hideValue.OutsidePoint;
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0, presenter.PresenterHolder.DOAnchorPos(anchorPos, data.showDuration))
                .SetDelay(data.delay)
                .SetEase(data.ease);
            return sequence;
        }

        public override Sequence PlayOnHide(BasePresenter presenter)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0, presenter.PresenterHolder.DOAnchorPos(data.showValue.OutsidePoint, data.hideDuration))
                .SetDelay(data.delay)
                .SetEase(data.ease);
            return sequence;
        }

        [System.Serializable]
        public struct InputValues
        {
            public enum SlideDirection
            {
                LeftToRight,
                RightToLeft,
                BottomToTop,
                TopToBottom
            }

            public SlideDirection direction;
            public Vector2 anchorPos;

            public Vector2 OutsidePoint =>
                direction == SlideDirection.LeftToRight ? anchorPos + Screen.width * Vector2.left :
                direction == SlideDirection.RightToLeft ? anchorPos + Screen.width * Vector2.right :
                direction == SlideDirection.BottomToTop ? anchorPos + Screen.width * Vector2.down :
                direction == SlideDirection.TopToBottom ? anchorPos + Screen.width * Vector2.up :
                anchorPos;
        }
    }
}