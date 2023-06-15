using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UIService.Runtime.Utilities
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaFitter : MonoBehaviour
    {
        [SerializeField] private RectTransform rTransform;
        [SerializeField] private FitmentType type;
        [SerializeField] private bool useType;
        async void Start()
        {
            await UniTask.DelayFrame(1);
            if (useType)
            {
                rTransform.FitInSafeArea(type);
            }
            else
            {
                rTransform.FitInSafeArea();
            }
        }
#if UNITY_EDITOR
        private void Reset()
        {
            rTransform = transform as RectTransform;
        }
#endif
    }
}