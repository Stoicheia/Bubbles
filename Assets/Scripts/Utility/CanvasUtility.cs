using System.Collections.Generic;
using System.Linq;
using Bubbles.InteractableInput;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utility
{
    public static class CanvasUtility
    {
        public static T DetectUnderCursor<T>() where T : IMouseInteractor
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition 
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {
                var obj = result.gameObject.GetComponent<T>();
                if (obj != null && obj.IsActive())
                {
                    return obj;
                }
            }

            return default(T);
        }

        public static T DetectUnderRect<T>(RectTransform reference, Canvas canvas) where T : IMouseInteractor
        {
            var found = GetOverlappingUIElements(reference, canvas).Where(x =>
            {
                if (x == null) return false;
                T comp = x.GetComponent<T>();
                if (comp != null) return comp.IsActive();
                return false;
            }).FirstOrDefault();
            return found == null ? default : found.GetComponent<T>();
        }
        
        public static List<RectTransform> GetOverlappingUIElements(RectTransform targetArea, Canvas canvas)
        {
            List<RectTransform> overlappingElements = new List<RectTransform>();
            Rect targetRect = GetWorldRect(targetArea);

            foreach (RectTransform child in canvas.GetComponentsInChildren<RectTransform>(true))
            {
                if (child == targetArea) continue; // Skip the target area itself

                Rect childRect = GetWorldRect(child);

                if (targetRect.Overlaps(childRect, true)) // Check if they overlap
                {
                    overlappingElements.Add(child);
                }
            }

            return overlappingElements;
        }

        private static Rect GetWorldRect(RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            Vector2 min = corners[0];
            Vector2 max = corners[2];
            return new Rect(min, max - min);
        }
    }
}