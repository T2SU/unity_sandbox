using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public Vector3 viewOffset;
    public Vector3 viewRotation;
    public float distance = 5.0f;

    public float pitch;

    public float rotateSensitivity = 3.0f;
    public float zoomSpeed = 3.0f;

    public float minDistance = 1.0f;
    public float maxDistance = 8.0f;

    public Transform lookTarget;

    // Update is called once per frame
    void LateUpdate()
    {
        RetrivePitch();
        RetriveDistance();
        if (lookTarget != null)
        {
            LookAtTarget();
        }
    }

    private void RetrivePitch()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetButton("Fire2"))
        {
            //pitch += Input.GetAxis("Mouse X");
            float anglePerPixel = 180.0f / Screen.width;
            pitch += Input.GetAxis("Mouse X") * anglePerPixel;
            pitch = Mathf.Repeat(pitch, 360.0f);
        }
    }

    private void RetriveDistance()
    {
        distance -= Input.mouseScrollDelta.y * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    private void LookAtTarget()
    {
        // 대상 위치 + 오프셋
        var lookPosition = lookTarget.position + viewOffset;

        // 대상과의 상대 위치
        var q = Quaternion.Euler(viewRotation + new Vector3(0, pitch * rotateSensitivity, 0));
        var relativePos = q * new Vector3(0, 0, -distance);

        transform.position = lookPosition + relativePos;
        transform.LookAt(lookPosition);

        CheckCameraObstacle(lookPosition);
    }

    private void CheckCameraObstacle(Vector3 lookPosition)
    {
        if (Physics.Linecast(lookPosition, transform.position, out var hit, LayerMask.GetMask("Ground")))
            transform.position = hit.point;
    }
}
