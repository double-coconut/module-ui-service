using System;
using Cysharp.Threading.Tasks;
using UniRx;
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


        public IObservable<Presenter> OnBeforeShow => BeforeShowSubject;
        public IObservable<Presenter> OnAfterShow => AfterShowSubject;
        public IObservable<Presenter> OnBeforeHide => BeforeHideSubject;
        public IObservable<Presenter> OnAfterHide => AfterHideSubject;
        public IObservable<bool> OnFocused => FocusSubject;
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