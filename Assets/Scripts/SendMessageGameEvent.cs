using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageGameEvent : GameEvent
{
    [Tooltip("이벤트 발생 시 콘솔 창에 출력될 메시지")]
    public string 메시지;

    public override void Execute()
    {
        Debug.Log(메시지);
    }
}
