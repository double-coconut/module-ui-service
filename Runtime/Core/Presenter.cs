using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace UIService.Runtime.Core
{
    public abstract class Presenter : MonoBehaviour
    {
        protected Subject<Presenter> BeforeShowSubject = new Subject<Presenter>();
        protected Subject<Presenter> AfterShowSubject = new Subject<Presenter>();
        protected Subject<Presenter> BeforeHideSubject = new Subject<Presenter>();
        protected Subject<Presenter> AfterHideSubject = new Subject<Presenter>();
        private Subject<bool> FocusSubject = new Subject<bool>();


        public Observable<Presenter> OnBeforeShow => BeforeShowSubject;
        public Observable<Presenter> OnAfterShow => AfterShowSubject;
        public Observable<Presenter> OnBeforeHide => BeforeHideSubject;
        public Observable<Presenter> OnAfterHide => AfterHideSubject;
        public Observable<bool> OnFocused => FocusSubject;
        public bool IsFocused { get; private set; }
        public string Id { get; internal set; } 
        

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