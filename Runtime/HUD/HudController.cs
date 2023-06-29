using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UIService.Runtime.Core;
using UIService.Runtime.Utilities;
using UniRx;
using Object = UnityEngine.Object;

namespace UIService.Runtime.Hud
{
    public class HudController
    {
        private readonly string _panelPrefabsPath = "UI/Panels";
        private readonly PresenterLoader _asset;
        private readonly ReactiveCommand<InitializablePresenter> _panelShowObservable;
        private readonly List<InitializablePresenter> _activePanels;
        public IObservable<InitializablePresenter> PanelShowObservable => _panelShowObservable;
        public HudController(PresenterLoader asset)
        {
            _asset = asset;
            _activePanels = new List<InitializablePresenter>();
            _panelShowObservable = new ReactiveCommand<InitializablePresenter>();
        }
        
        public async UniTask<T> Show<T>() where T : InitializablePresenter
        {
            T panelPrefab = await _asset.LoadPrefabAsync<T>(_panelPrefabsPath);
            T panel = Object.Instantiate(panelPrefab);
            _panelShowObservable.Execute(panel);
            _activePanels.Add(panel);
            panel.Initialize();
            panel.Show();
            return panel;
        }
        
        public T Get<T>() where T : InitializablePresenter
        {
            return _activePanels.FirstOrDefault(panel=>panel is T) as T;
        }
        public void Hide<T>() where T : InitializablePresenter
        {
            InitializablePresenter panel = _activePanels.FirstOrDefault(panel=>panel is T);
            if (panel!=null)
            {
                panel.Hide();
                _activePanels.Remove(panel);
                Object.Destroy(panel.gameObject);
            }
        }
        
        public void HideAll()
        {
            foreach (InitializablePresenter panel in _activePanels)
            {
                panel.Hide();
                Object.Destroy(panel.gameObject);
            }
            _activePanels.Clear();
        }
    }
}