using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventOnTrigger : MonoBehaviour
{
    [Tooltip("이벤트를 발생 시킬 수 있는 레이어")]
    public LayerMask 레이어;

    [Tooltip("이벤트를 받을 대상 객체")]
    public GameObject 대상;

    GameEventHandler target;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & 레이어.value) != 0)
            target.Handle(EventMethod.TriggerEnter);
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & 레이어.value) != 0)
            target.Handle(EventMethod.TriggerExit);
    }

    private void Awake()
    {
        target = 대상.GetComponent<GameEventHandler>();
    }
}
