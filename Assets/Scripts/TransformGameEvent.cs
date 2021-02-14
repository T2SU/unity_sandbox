using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransformGameEvent : GameEvent
{
    public Vector3 시작위치;
    public Vector3 끝위치;
    public AnimationCurve 진행그래프;
    public float 소요시간 = 1;
    public bool 활성화;
    [Range(0,1)]
    public float 위치;
    public List<Transform> 이동대상;

    [System.NonSerialized]
    public float elapsed;

    float direction = 1;
    Platform platform;

    public override void Execute()
    {
        활성화 = true;
    }

    private void FixedUpdate()
    {
        if (활성화)
        {
            elapsed += direction * Time.fixedDeltaTime / 소요시간;
            DoTransform(elapsed);
        }
    }

    public void DoTransform(float elapsed)
    {
        위치 = Mathf.PingPong(elapsed, 1f);

        var curvedPos = 진행그래프.Evaluate(위치);
        var pos = transform.TransformPoint(Vector3.Lerp(시작위치, 끝위치, curvedPos));
        var delta = pos - 이동대상.First().position;
        foreach (var t in 이동대상)
            t.position = pos;

        if (platform != null)
            platform.MoveOnPlatform(delta);
    }

    private void Start()
    {
        elapsed = 위치;
    }

    protected override void Awake()
    {
        base.Awake();
        platform = GetComponentInChildren<Platform>();
    }
}
