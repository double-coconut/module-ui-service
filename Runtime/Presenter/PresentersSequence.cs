using System.Collections.Generic;
using Services.UIService.Presenter.Base;
using UnityEngine;

namespace Services.UIService.Presenter
{
    public class PresentersSequence
    {
        private readonly Queue<BasePresenter> _queue;

        public bool IsLast => _queue.Count == 1;
        public bool IsEmpty => _queue == null || _queue.Count == 0;


        public PresentersSequence(BasePresenter firstPresenter)
        {
            _queue = new Queue<BasePresenter>();
            _queue.Enqueue(firstPresenter);
        }

        public void AddToQueue(BasePresenter presenter)
        {
            _queue.Enqueue(presenter);
        }

        public BasePresenter PeekFromQueue()
        {
            return _queue.Peek();
        }

        public BasePresenter PopFromQueue()
        {
            return _queue.Dequeue();
        }

        public void Dispose()
        {
            foreach (var presenter in _queue)
            {
                presenter.CallBeforeHide();
                presenter.Dispose();
                Object.Destroy(presenter.gameObject);
                presenter.CallAfterHide();
            }

            _queue.Clear();
        }
    }
}