using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Services.UIService.Core
{
    public abstract class View : MonoBehaviour
    {
        protected Subject<View> BeforeShowSubject = new Subject<View>();
        protected Subject<View> AfterShowSubject = new Subject<View>();
        protected Subject<View> BeforeHideSubject = new Subject<View>();
        protected Subject<View> AfterHideSubject = new Subject<View>();
        private Subject<bool> FocusSubject = new Subject<bool>();


        public IObservable<View> OnBeforeShow => BeforeShowSubject;
        public IObservable<View> OnAfterShow => AfterShowSubject;
        public IObservable<View> OnBeforeHide => BeforeHideSubject;
        public IObservable<View> OnAfterHide => AfterHideSubject;
        public IObservable<bool> OnFocused => FocusSubject;
        public bool IsFocused { get; private set; }

        public abstract UniTask Show();
        public abstract UniTask Hide();
        public abstract void Disable();
        public abstract void Enable();

        public void SetFocused(bool focused)
        {
            if(IsFocused == focused)
            {
                return;
            }

            IsFocused = focused;
            FocusSubject?.OnNext(focused);
        }
        public void CallBeforeShow() => BeforeShowSubject?.OnNext(this);
        public void CallAfterShow() => AfterShowSubject?.OnNext(this);
        public void CallBeforeHide() => BeforeHideSubject?.OnNext(this);
        public void CallAfterHide() => AfterHideSubject?.OnNext(this);
    }
}