using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDialogueGameEvent : GameEvent
{
    [Tooltip("이벤트 발생 시 추가할 대화")]
    public List<Dialogue> dialogues;

    [Tooltip("대화를 추가할 컨트롤러")]
    public DialogueController dialogueController;

    public override void Execute()
    {
        dialogueController.Enqueue(dialogues);
    }
}
