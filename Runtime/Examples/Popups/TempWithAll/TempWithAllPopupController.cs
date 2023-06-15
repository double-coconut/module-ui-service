using UIService.Runtime.Presenter;
using UIService.Runtime.Presenter.Base;
using Zenject;

namespace UIService.Runtime.Examples.Popups.TempWithAll
{
    public class TempWithAllPopupController : BasePresenterController
    {
        private readonly PresenterService _presenterService;

        public string Name { get; }

        public TempWithAllPopupController(string name, PresenterService presenterService) : base(presenterService)
        {
            _presenterService = presenterService;
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }

        public void ShowNextPopup(TempWithAllPopup.Data data)
        {
            _presenterService.ShowInCurrentSequence<TempWithAllPopup>(
                new TempWithAllPopup.Data
                {
                    Name = $"NEXT : {data.Name}"
                });
            Close();
        }


        public class Factory : PlaceholderFactory<string, TempWithAllPopupController>
        {
        }
    }
}