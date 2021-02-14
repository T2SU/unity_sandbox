using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerExitDispatcher : MonoBehaviour, IPointerExitHandler
{
    public BaseEventDataEvent OnExit = new BaseEventDataEvent();

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExit.Invoke(eventData);
    }
}
