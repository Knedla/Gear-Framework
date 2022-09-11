using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

namespace GearFramework.Runtime
{
    public static class GraphicRaycasterExtension
    {
        /// <summary>
        /// Raycast using the Graphics Raycaster and Vector2 position
        /// </summary>
        /// <param name="raycaster"></param>
        /// <param name="eventSystem"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static List<RaycastResult> GetRaycastResults(this GraphicRaycaster raycaster, EventSystem eventSystem, Vector2 position)
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = position;
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);
            return results;
        }
    }
}
