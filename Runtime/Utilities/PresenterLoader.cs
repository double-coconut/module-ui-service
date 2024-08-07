using System.IO;
using Cysharp.Threading.Tasks;
using UIService.Runtime.Core;
using UnityEngine;

namespace UIService.Runtime.Utilities
{
    public class PresenterLoader
    {
        public T LoadPrefab<T>(string path) where T : Core.Presenter
        {
            string name = typeof(T).Name;
            return Resources.Load<T>(Path.Combine(path, name));
        }

        public async UniTask<T> LoadPrefabAsync<T>(string path) where T : Core.Presenter
        {
            string name = typeof(T).Name;
            ResourceRequest request = Resources.LoadAsync<T>(Path.Combine(path, name));
            await UniTask.WaitUntil(() => request.isDone);
            return (T)request.asset;
        }
    }
}