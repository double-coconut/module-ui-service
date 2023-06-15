using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Services.UIService.Core;
using Services.UIService.Utilities;
using UniRx;
using Object = UnityEngine.Object;

namespace Services.UIService.HUD
{
    public class HudController
    {
        private readonly string _panelPrefabsPath = "UI/Panels";
        private readonly LoaderService _assetService;
        private readonly ReactiveCommand<InitializableView> _panelShowObservable;
        private readonly List<InitializableView> _activePanels;
        public IObservable<InitializableView> PanelShowObservable => _panelShowObservable;
        public HudController(LoaderService assetService)
        {
            _assetService = assetService;
            _activePanels = new List<InitializableView>();
            _panelShowObservable = new ReactiveCommand<InitializableView>();
        }
        
        public async UniTask<T> Show<T>() where T : InitializableView
        {
            T panelPrefab = await _assetService.LoadPrefabAsync<T>(_panelPrefabsPath);
            T panel = Object.Instantiate(panelPrefab);
            _panelShowObservable.Execute(panel);
            _activePanels.Add(panel);
            panel.Initialize();
            panel.Show();
            return panel;
        }
        
        public T Get<T>() where T : InitializableView
        {
            return _activePanels.FirstOrDefault(panel=>panel is T) as T;
        }
        public void Hide<T>() where T : InitializableView
        {
            InitializableView panel = _activePanels.FirstOrDefault(panel=>panel is T);
            if (panel!=null)
            {
                panel.Hide();
                _activePanels.Remove(panel);
                Object.Destroy(panel.gameObject);
            }
        }
        
        public void HideAll()
        {
            foreach (InitializableView panel in _activePanels)
            {
                panel.Hide();
                Object.Destroy(panel.gameObject);
            }
            _activePanels.Clear();
        }
    }
}