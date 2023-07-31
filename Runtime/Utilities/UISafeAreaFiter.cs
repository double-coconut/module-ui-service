using System;
using UnityEngine;

namespace UIService.Runtime.Utilities
{
    public enum FitmentType
    {
        Left,
        Right,
        Top,
        Bottom,
        Horizontal,
        Vertical
    }

    public static class UISafeAreaFitter
    {
        public static void FitInSafeArea(this RectTransform trans)
        {
            Rect safeAreaRect = Screen.safeArea;

            Vector2 anchorMin = safeAreaRect.position;
            Vector2 anchorMax = safeAreaRect.position + safeAreaRect.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            trans.anchorMin = anchorMin;
            trans.anchorMax = anchorMax;

            Debug.LogFormat("Safe area applied to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}",
                trans.name, safeAreaRect.x, safeAreaRect.y, safeAreaRect.width, safeAreaRect.height, Screen.width,
                Screen.height);
        }


        public static void FitInSafeArea(this RectTransform trans, FitmentType fitmentType, float offset = 0)
        {
            Rect safeAreaRect = Screen.safeArea;

            if (safeAreaRect.height + 1 > Screen.height &&
                (fitmentType == FitmentType.Vertical ||
                 fitmentType == FitmentType.Top ||
                 fitmentType == FitmentType.Bottom
                )) return;
            if (safeAreaRect.width + 1 > Screen.width &&
                (fitmentType == FitmentType.Horizontal ||
                 fitmentType == FitmentType.Left ||
                 fitmentType == FitmentType.Right)) return;

            safeAreaRect.height += (Screen.height - safeAreaRect.height) / 2;
            safeAreaRect.width += (Screen.width - safeAreaRect.width) / 2;

            if (fitmentType == FitmentType.Left || fitmentType == FitmentType.Right)
                safeAreaRect.width += offset;
            if (fitmentType == FitmentType.Top || fitmentType == FitmentType.Bottom)
                safeAreaRect.height += offset;

            Vector2 anchorMin = safeAreaRect.position;
            Vector2 anchorMax = safeAreaRect.position + safeAreaRect.size;

            switch (fitmentType)
            {
                case FitmentType.Left:
                    //anchorMin.x /= Screen.width;
                    anchorMin.x = 1 - safeAreaRect.width / Screen.width;

                    anchorMin.y = trans.anchorMin.y;
                    anchorMax.x = trans.anchorMax.x;
                    anchorMax.y = trans.anchorMax.y;
                    break;
                case FitmentType.Right:
                    //anchorMax.x /= Screen.width;
                    anchorMax.x = safeAreaRect.width / Screen.width;

                    anchorMin.x = trans.anchorMin.x;
                    anchorMin.y = trans.anchorMin.y;
                    anchorMax.y = trans.anchorMax.y;
                    break;
                case FitmentType.Top:
                    //anchorMax.y /= Screen.height;
                    anchorMax.y = safeAreaRect.height / Screen.height;

                    anchorMin.x = trans.anchorMin.x;
                    anchorMin.y = trans.anchorMin.y;
                    anchorMax.x = trans.anchorMax.x;
                    break;
                case FitmentType.Bottom:
                    //anchorMin.y /= Screen.height;
                    anchorMin.y = 1 - safeAreaRect.height / Screen.height;

                    anchorMin.x = trans.anchorMin.x;
                    anchorMax.y = trans.anchorMax.y;
                    anchorMax.x = trans.anchorMax.x;
                    break;
                case FitmentType.Horizontal:
                    anchorMin.x = 1 - safeAreaRect.width / Screen.width;
                    anchorMax.x = safeAreaRect.width / Screen.width;
                    anchorMin.y = trans.anchorMin.y;
                    anchorMax.y = trans.anchorMax.y;
                    break;
                case FitmentType.Vertical:
                    anchorMin.y = 1 - safeAreaRect.height / Screen.height;
                    anchorMax.y = safeAreaRect.height / Screen.height;
                    anchorMin.x = trans.anchorMin.x;
                    anchorMax.x = trans.anchorMax.x;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fitmentType), fitmentType, null);
            }

            trans.anchorMin = anchorMin;
            trans.anchorMax = anchorMax;

            Debug.LogFormat("Safe area applied to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}",
                trans.name, safeAreaRect.x, safeAreaRect.y, safeAreaRect.width, safeAreaRect.height, Screen.width,
                Screen.height);
        }

        public static void MoveInSafeArea(this RectTransform trans, FitmentType fitmentType, float offset = 0)
        {
            Rect safeAreaRect = Screen.safeArea;

            if (safeAreaRect.height + 1 > Screen.height || safeAreaRect.width + 1 > Screen.width) return;

            safeAreaRect.height = (Screen.height - safeAreaRect.height) / 2;
            safeAreaRect.width = (Screen.width - safeAreaRect.width) / 2;

            if (fitmentType == FitmentType.Left || fitmentType == FitmentType.Right)
                safeAreaRect.width += offset;
            if (fitmentType == FitmentType.Top || fitmentType == FitmentType.Bottom)
                safeAreaRect.height += offset;

            Vector2 pos = trans.anchoredPosition;

            switch (fitmentType)
            {
                case FitmentType.Left:
                    pos.x -= safeAreaRect.width / 2 + offset;
                    break;
                case FitmentType.Right:
                    pos.x += safeAreaRect.width / 2 + offset;
                    break;
                case FitmentType.Top:
                    pos.y += safeAreaRect.height / 2 + offset;
                    break;
                case FitmentType.Bottom:
                    pos.y -= safeAreaRect.height / 2 + offset;
                    break;
            }

            trans.anchoredPosition = pos;

            Debug.LogFormat("Safe area applied to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}",
                trans.name, safeAreaRect.x, safeAreaRect.y, safeAreaRect.width, safeAreaRect.height, Screen.width,
                Screen.height);
        }
    }
}