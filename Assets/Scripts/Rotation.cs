using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed = 360f;

    void Update()
    {
        var e = transform.eulerAngles;
        e.y += Time.deltaTime * speed;
        transform.eulerAngles = e;
    }
}
