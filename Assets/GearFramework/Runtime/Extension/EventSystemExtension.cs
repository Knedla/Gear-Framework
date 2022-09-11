using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

namespace GearFramework.Runtime
{
    public static class EventSystemExtension
    {
        /// <summary>
        /// Raycast using the Event System and Vector2 position
        /// </summary>
        /// <param name="eventSystem"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static List<RaycastResult> GetRaycastResults(this EventSystem eventSystem, Vector2 position)
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = position;
            List<RaycastResult> results = new List<RaycastResult>();
            eventSystem.RaycastAll(pointerEventData, results);
            return results;
        }
    }
}
