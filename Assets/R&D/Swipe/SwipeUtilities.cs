using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class SwipeUtilities
{
    /// <summary>
    /// Returns true if the pointer over any world object (Requires Collider).
    /// </summary>
    public static bool IsTouchBlockedbyAnyObj()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (UnityEngine.Physics.Raycast(ray, out hit))
        {
            if (hit.collider)
            {
                //----------When the target collider has a specific class---------//
                //if (hit.collider.gameObject.TryGetComponent<SomeClass>(out SomeClass someClass))
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Returns true if the pointer over any UI element.(Requires EventSystem)
    /// </summary>
    public static bool IsPointerOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else
        {
            PointerEventData pe = new PointerEventData(EventSystem.current);
            pe.position = Input.mousePosition;
            List<RaycastResult> hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pe, hits);
            return hits.Count > 0;
        }
    }
}