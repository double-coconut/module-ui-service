using DG.Tweening;
using UIService.Runtime.Presenter.Base;
using UnityEngine;

namespace UIService.Runtime.Presenter.Animations
{
    [CreateAssetMenu(fileName = "ScaleAnimation", menuName = "UI Service/Animations/Scale", order = 0)]
    public class ScaleAnimation : AbstractAnimation<Vector3>
    {
        public override Sequence PlayOnShow(BasePresenter presenter)
        {
            presenter.PresenterHolder.localScale = data.hideValue;
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0, presenter.PresenterHolder.DOScale(data.showValue, data.showDuration)).SetDelay(data.delay)
                .SetEase(data.ease);
            return sequence;
        }
        
        public override Sequence PlayOnHide(BasePresenter presenter)
        {
            presenter.PresenterHolder.localScale = data.showValue;
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0, presenter.PresenterHolder.DOScale(data.hideValue, data.hideDuration)).SetDelay(data.delay)
                .SetEase(data.ease);
            return sequence;
        }
    }
}