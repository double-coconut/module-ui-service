using DG.Tweening;
using Services.UIService.Presenter.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace Services.UIService.Presenter.Animations
{
    
    public abstract class AbstractAnimation : ScriptableObject
    {
        public abstract Sequence PlayOnShow(BasePresenter presenter);
        public abstract Sequence PlayOnHide(BasePresenter presenter);
        
    }
    
    
    
    public abstract class AbstractAnimation<T> : AbstractAnimation
    {
        [SerializeField] protected Data data;

        [System.Serializable]
        public struct Data
        {
            public T showValue;
            public T hideValue;
            public float showDuration;
            public float hideDuration;
            public float delay;
            public Ease ease;
        }
    }
}