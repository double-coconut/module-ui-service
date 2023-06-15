using System.IO;
using Cysharp.Threading.Tasks;
using Services.UIService.Core;
using UnityEngine;

namespace Services.UIService.Utilities
{
    public class LoaderService
    {
        public T LoadPrefab<T>(string path) where T : View
        {
            string name = typeof(T).Name;
            return Resources.Load<T>(Path.Combine(path, name));
        }

        public async UniTask<T> LoadPrefabAsync<T>(string path) where T : View
        {
            string name = typeof(T).Name;
            var asset = await Resources.LoadAsync<T>(Path.Combine(path, name));
            return (T) asset;
        }
    }
}