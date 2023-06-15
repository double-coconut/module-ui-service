using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UIService.Runtime.Core;
using UIService.Runtime.Presenter.Animations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UIService.Runtime.Presenter.Base
{
    [RequireComponent(typeof(GameObjectContext))]
    [RequireComponent(typeof(ZenAutoInjecter))]
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BasePresenter : InitializableView, IDisposable
    {
        [Header("Animations")] [SerializeField]
        protected AnimationSequence[] animationSequence;

        [Space, Header("Parameters")] [SerializeField]
        protected CanvasGroup group;

        [SerializeField] protected RectTransform presenterHolder;
        [SerializeField] protected Button dimmerButton;

        protected Sequence AnimSequence;


        public CanvasGroup Group => group;
        public RectTransform PresenterHolder => presenterHolder;


        public override UniTask Show()
        {
            AnimSequence?.Kill();
            AnimSequence = BuildAnimationSequence(true);

            return UniTask.WhenAny(
                AnimSequence.AsyncWaitForCompletion().AsUniTask(),
                AnimSequence.AsyncWaitForKill().AsUniTask());
        }

        public override UniTask Hide()
        {
            AnimSequence?.Kill();
            AnimSequence = BuildAnimationSequence(false);

            return UniTask.WhenAny(
                AnimSequence.AsyncWaitForCompletion().AsUniTask(),
                AnimSequence.AsyncWaitForKill().AsUniTask());
        }

        public override void Disable()
        {
            AnimSequence?.Kill();
            gameObject.SetActive(false);
        }

        public override void Enable()
        {
            AnimSequence?.Kill();
            gameObject.SetActive(true);
        }


        public abstract void Dispose();


        protected Sequence BuildAnimationSequence(bool show)
        {
            Sequence animSequence = DOTween.Sequence();
            for (int i = 0; i < animationSequence.Length; i++)
            {
                AnimationSequence sequence = animationSequence[i];
                Sequence innerSequence = DOTween.Sequence();
                foreach (AbstractAnimation anim in sequence.animations)
                {
                    innerSequence.Insert(i, show ? anim.PlayOnShow(this) : anim.PlayOnHide(this));
                }

                animSequence.Append(innerSequence);
            }

            return animSequence;
        }

#if UNITY_EDITOR
        private void Reset()
        {
            gameObject.name = $"{GetType().Name}";
        }
#endif
    }
}