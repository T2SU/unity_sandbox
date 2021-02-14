using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyController : MonoBehaviour
{
    private static Plane _plane = new Plane(Vector3.up, 0f);

    private Vector3? _targetPosition;
    // private Vector3 velocity;

    public float Speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_plane.Raycast(ray, out var distance))
            {
                var point = ray.GetPoint(distance);
                point.y = transform.localScale.y;
                _targetPosition = point;
            }
        }
    }

    void FixedUpdate()
    {
        if (_targetPosition.HasValue)
        {
            var targetPosition = _targetPosition.Value;
            if (transform.position == targetPosition)
            {
                _targetPosition = null;
            }
            else
            {
                //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Speed);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Speed);
            }
        }
    }

    void OnGUI()
    {
        var cam = Camera.main;
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }


}
