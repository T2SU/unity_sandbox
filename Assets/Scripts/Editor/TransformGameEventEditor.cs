using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformGameEvent), true)]
public class TransformGameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        using (var cc = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (cc.changed)
            {
                if (target is TransformGameEvent tge)
                {
                    tge.DoTransform(tge.elapsed = tge.위치);
                }
            }
        }
    }

    private void OnSceneGUI()
    {
        if (target is TransformGameEvent t)
        {
            var start = t.transform.TransformPoint(t.시작위치);
            var end = t.transform.TransformPoint(t.끝위치);
            using (var cc = new EditorGUI.ChangeCheckScope())
            {
                start = Handles.PositionHandle(start, Quaternion.AngleAxis(180, t.transform.up) * t.transform.rotation);
                Handles.Label(start, nameof(t.시작위치), GUI.skin.button);
                Handles.Label(end, nameof(t.끝위치), GUI.skin.button);
                end = Handles.PositionHandle(end, t.transform.rotation);
                if (cc.changed)
                {
                    Undo.RecordObject(t, "Move Handles");
                    t.시작위치 = t.transform.InverseTransformPoint(start);
                    t.끝위치 = t.transform.InverseTransformPoint(end);
                    t.DoTransform(t.위치);
                }
            }
            Handles.color = Color.yellow;
            Handles.DrawDottedLine(start, end, 5);
            Handles.Label(Vector3.Lerp(start, end, 0.5f), "거리: " + (end - start).magnitude);
        }
    }
}
