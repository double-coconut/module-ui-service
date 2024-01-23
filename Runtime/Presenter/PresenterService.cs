using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UIService.Runtime.Core;
using UIService.Runtime.Presenter.Base;
using UIService.Runtime.Utilities;
using UniRx;
using Object = UnityEngine.Object;

namespace UIService.Runtime.Presenter
{
    public class PresenterService
    {
        private readonly string _presenterPrefabsPath = "UI/Presenters";
        private readonly PresenterLoader _asset;
        private readonly Stack<PresentersSequence> _presentersStack;
        private readonly Queue<string> _presenterShowQueue = new Queue<string>();
        private readonly ReactiveCommand<BasePresenter> _presenterToShow;
        private readonly BoolReactiveProperty _hasActivePresenter;

        public IReadOnlyReactiveProperty<bool> HasActivePresenter => _hasActivePresenter;
        public IObservable<BasePresenter> ObserveNewPresenter => _presenterToShow;


        public PresenterService(PresenterLoader asset)
        {
            _asset = asset;
            _hasActivePresenter = new BoolReactiveProperty();
            _presentersStack = new Stack<PresentersSequence>();
            _presenterToShow = new ReactiveCommand<BasePresenter>();
        }

        public async UniTask Show<T>(IPresenterData data = null, bool hidePrevious = true) where T : BasePresenter
        {
            string id = Guid.NewGuid().ToString();
            _presenterShowQueue.Enqueue(id);

            _hasActivePresenter.Value = true;
            if (hidePrevious)
            {
                await HideLast();
            }

            T mockup = await _asset.LoadPrefabAsync<T>(_presenterPrefabsPath);
            
            while (_presenterShowQueue.Peek() != id)
            {
                await UniTask.DelayFrame(1);
            }
            _presenterShowQueue.Dequeue();
            T presenter = Object.Instantiate(mockup);
            presenter.Id = id;
            presenter.Disable();
            PresentersSequence sequence = new PresentersSequence(presenter);
            _presentersStack.Push(sequence);
            presenter.Initialize(data);
            _presenterToShow.Execute(presenter);
            await Show(presenter);
        }
        

        public async UniTask ShowInCurrentSequence<T>(IPresenterData data = null) where T : BasePresenter
        {
            string id = Guid.NewGuid().ToString();
            _presenterShowQueue.Enqueue(id);
            _hasActivePresenter.Value = true;
            T mockup = await _asset.LoadPrefabAsync<T>(_presenterPrefabsPath);
            
            while (_presenterShowQueue.Peek() != id)
            {
                await UniTask.DelayFrame(1);
            }
            _presenterShowQueue.Dequeue();
            T presenter = Object.Instantiate(mockup);
            presenter.Id = id;
            presenter.Disable();
            presenter.Initialize(data);
            if (_presentersStack.Count == 0)
            {
                _presentersStack.Push(new PresentersSequence(presenter));
                _presenterToShow.Execute(presenter);
                await Show(presenter);
                return;
            }

            PresentersSequence sequence = _presentersStack.Peek();
            sequence.AddToQueue(presenter);
            _presenterToShow.Execute(presenter);
        }

        public async UniTask Hide()
        {
            PresentersSequence sequence = await HideLast();
            if (sequence == null || sequence.IsEmpty)
            {
                _hasActivePresenter.Value = false;
                return;
            }

            BasePresenter presenter = sequence.PeekFromQueue();
            _presenterToShow.Execute(presenter);
            await Show(presenter);
        }

        private async UniTask Show<T>(T presenter) where T : BasePresenter
        {
            BasePresenter currentFocused = GetFocused();
            currentFocused.SetFocused(false);
            presenter.CallBeforeShow();
            presenter.Enable();
            await presenter.Show();
            presenter.SetFocused(true);
            presenter.CallAfterShow();
        }

        private BasePresenter GetFocused()
        {
            if (_presentersStack.Count == 0)
            {
                return null;
            }

            PresentersSequence sequence = _presentersStack.Peek();
            return sequence.PeekFromQueue();
        }

        private async UniTask<PresentersSequence> HideLast()
        {
            if (_presentersStack.Count == 0)
            {
                // Debug.LogError("No presenter to hide");
                return null;
            }

            var sequence = _presentersStack.Peek();
            BasePresenter presenter;
            if (sequence.IsLast)
            {
                sequence = _presentersStack.Pop();
                presenter = sequence.PopFromQueue();
                presenter.CallBeforeHide();
                await presenter.Hide();
                presenter.Dispose();
                Object.Destroy(presenter.gameObject);
                presenter.CallAfterHide();
                return sequence;
            }

            presenter = sequence.PopFromQueue();
            presenter.CallBeforeHide();
            await presenter.Hide();
            presenter.Dispose();
            Object.Destroy(presenter.gameObject);
            presenter.CallAfterHide();
            return sequence;
        }


        public void Reset()
        {
            foreach (var sequence in _presentersStack)
            {
                sequence.Dispose();
            }

            _presentersStack.Clear();
        }
    }
}