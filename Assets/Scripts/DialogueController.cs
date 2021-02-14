using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public Text text;
    public float typeDelay = 0.2f;
    public Image displayerTexture2D;
    public RawImage displayerModel3D;
    public Transform parentModel3D;

    private readonly Queue<Dialogue> dialogues = new Queue<Dialogue>();

    private Coroutine coroutine;
    private Dialogue currentDialogue;
    private int layer3D;

    private void Start()
    {
        layer3D = LayerMask.NameToLayer("3DinUI");
    }

    public void Enqueue(Dialogue dialogue)
        => Enqueue(Enumerable.Repeat(dialogue, 1));

    public void Enqueue(IEnumerable<Dialogue> dialogues)
    {
        bool empty = this.dialogues.Count == 0;

        foreach (var d in dialogues)
            this.dialogues.Enqueue(d);

        if (empty)
            Next();
    }

    public void Next()
    {
        Stop();
        Activate();
    }

    public void Stop()
    {
        if (coroutine == null)
            return;

        StopCoroutine(coroutine);
        coroutine = null;
        currentDialogue = null;
        ResetFace();
        gameObject.SetActive(false); 
    }

    private void Activate()
    {
        if (coroutine != null)
            return;

        if (dialogues.Count > 0)
        {
            gameObject.SetActive(true);
            currentDialogue = dialogues.Dequeue();
            if (currentDialogue.@object != null)
            {
                if (currentDialogue.@object is GameObject go)
                {
                    displayerModel3D.gameObject.SetActive(true);
                    var g = Instantiate(go, parentModel3D);
                    g.layer = layer3D;
                }
                if (currentDialogue.@object is Texture2D t2d)
                {
                    displayerTexture2D.gameObject.SetActive(true);
                    displayerTexture2D.sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.one / 2, 100, 0, SpriteMeshType.FullRect);
                }
            }

            coroutine = StartCoroutine(DisplayDialogue());
        }
    }

    private void ResetFace()
    {
        foreach (Transform t in parentModel3D)
            Destroy(t.gameObject);
        displayerTexture2D.sprite = null;
        displayerTexture2D.gameObject.SetActive(false);
        displayerModel3D.gameObject.SetActive(false);
    }

    private IEnumerator DisplayDialogue()
    {
        var sb = new StringBuilder();
        foreach (char ch in currentDialogue.text)
        {
            sb.Append(ch);
            text.text = sb.ToString();
            yield return new WaitForSeconds(typeDelay);
        }
        text.text = currentDialogue.text;
    }

    private void OnDestroy()
    {
        Stop();
    }
}

[Serializable]
public class Dialogue
{
    public UnityEngine.Object @object;

    [TextArea(1, 5)]
    public string text;
}