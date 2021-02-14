using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public LayerMask affectedLayers;

    HashSet<GameObject> objects = new HashSet<GameObject>();

    private void OnTriggerStay(Collider other)
    {
        if ((affectedLayers.value & 1 << other.gameObject.layer) != 0)
        {
            objects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((affectedLayers.value & 1 << other.gameObject.layer) != 0)
        {
            objects.Remove(other.gameObject);
        }
    }

    public void MoveOnPlatform(Vector3 deltaPosition)
    {
        foreach (var obj in objects)
        {
            obj.transform.position += deltaPosition;
        }
    }
}
