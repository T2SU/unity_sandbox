using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool moving;
    private Vector3 p;

    public void OnPointerDown(PointerEventData eventData)
    {
        moving = true;
        p = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moving = false;
    }

    private void LateUpdate()
    {
        if (moving)
        {
            var delta = Input.mousePosition - p;
            p = Input.mousePosition;
            transform.position += delta;
        }
    }
}
