using DG.Tweening;
using Services.UIService.Presenter.Base;
using UnityEngine;

namespace Services.UIService.Presenter.Animations
{
    [CreateAssetMenu(fileName = "FadeInAnimation", menuName = "UI Service/Animations/Fade", order = 0)]
    public class FadeInAnimation : AbstractAnimation<float>
    {
        public override Sequence PlayOnShow(BasePresenter presenter)
        {
            presenter.Group.alpha = data.hideValue;
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0, presenter.Group.DOFade(data.showValue, data.showDuration)).SetDelay(data.delay)
                .SetEase(data.ease);
            return sequence;
        }
        
        public override Sequence PlayOnHide(BasePresenter presenter)
        {
            presenter.Group.alpha = data.showValue;
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0, presenter.Group.DOFade(data.hideValue, data.hideDuration)).SetDelay(data.delay)
                .SetEase(data.ease);
            return sequence;
        }
    }
}