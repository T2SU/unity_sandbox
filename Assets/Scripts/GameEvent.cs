using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventHandler))]
public abstract class GameEvent : MonoBehaviour
{
    [Tooltip("이벤트 발생 후 실행까지 대기할 지연시간")]
    public float 지연시간;
    [Tooltip("지정된 종류의 이벤트가 발생될 때 실행")]
    public EventMethod 이벤트종류;

    public abstract void Execute();

    public void DoEvent()
    {
        if (지연시간 > 0)
            Invoke(nameof(Execute), 지연시간);
        else
            Execute();
    }

    protected virtual void Awake()
    {
        gameObject.GetComponent<GameEventHandler>().Register(이벤트종류, this);
    }
}
