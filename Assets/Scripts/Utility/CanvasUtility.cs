using System.Collections.Generic;
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
    }
}