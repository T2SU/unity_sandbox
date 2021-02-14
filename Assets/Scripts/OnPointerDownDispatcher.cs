using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnPointerDownDispather : MonoBehaviour, IPointerDownHandler
{
    public BaseEventDataEvent OnDown = new BaseEventDataEvent();

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnDown.Invoke(eventData);
        }
    }
}



[System.Serializable]
public class BaseEventDataEvent : UnityEvent<BaseEventData>{}