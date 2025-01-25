using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utility
{
    public static class CanvasUtility
    {
        public static T DetectUnderCursor<T>() where T : UnityEngine.Component
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
                if (obj != null)
                {
                    Debug.Log($"Image detected: {result.gameObject.name}");
                    return obj;
                }
            }

            Debug.Log("No image detected under the cursor.");
            return null;
        }
    }
}