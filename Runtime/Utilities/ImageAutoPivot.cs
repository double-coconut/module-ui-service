using UnityEngine;
using UnityEngine.UI;

namespace UIService.Runtime.Utilities
{
    [ExecuteInEditMode]
    public class ImageAutoPivot : Image
    {
#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            SetupPivot();
        }
#endif
        private void SetupPivot()
        {
            if (sprite == null) return;

            Vector2 pivot = sprite.pivot;
            Vector2 size = sprite.rect.size;
            pivot.x /= size.x;
            pivot.y /= size.y;

            rectTransform.pivot = pivot;
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}