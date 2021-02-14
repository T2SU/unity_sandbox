using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float zoomSpeed = 5.0f;
    public float rotateSpeed = 10.0f;

    public float minFieldOfView = 15f;
    public float maxFieldOfView = 90f;

    public Vector3 viewRotation;
    public Vector3 viewDistance;

    private Vector3 positionGap;

    void Start()
    {
        transform.rotation = Quaternion.Euler(viewRotation);
        UpdatePosition();
    }

    void Update()
    {
        UpdateZoom();
        UpdatePitch();
        UpdatePosition();
    }

    private void UpdatePitch()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetButton("Fire2"))
        {
            GetPositionGap(() => CameraAround());
        }
    }

    private void UpdateZoom()
    {
        var fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        fov = Mathf.Clamp(fov, minFieldOfView, maxFieldOfView);
        Camera.main.fieldOfView = fov;
    }

    private void CameraAround()
    {
        transform.RotateAround(
            target.transform.position,
            new Vector3(0.0f, 1.0f, 0.0f),
            Input.GetAxis("Mouse X") * rotateSpeed);

        if (Physics.Linecast(target.transform.position, transform.position, out var hit, LayerMask.GetMask("Ground")))
            transform.position = hit.point;

        //var ray = Camera.main.ScreenPointToRay(targetObject.transform.position);
        
    }

    private void UpdatePosition()
    {
        transform.position = target.transform.position + viewDistance + positionGap;
    }

    private void GetPositionGap(Action action)
    {
        var before = transform.position;
        action();
        positionGap += transform.position - before;
    }

    /*private GameObject _playerObject;
    private Vector3 _dPosition;
    private Quaternion _dRotation;

    private readonly Vector3 _defaultPosition = new Vector3(0.0f, 0.5f, -1.0f);
    private readonly Vector3 _defaultRotation = new Vector3(25.0f, 0.0f, 0.0f);

    public float MouseSensitive = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        EnsurePlayerObject();
    }

    void Update()
    {
        EnsurePlayerObject();


        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetMouseButton(1))
        {
            var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            var target = _playerObject.transform;

            var angle = mouseMovement.x * MouseSensitive;
            var axis = Vector3.up;
            var point = target.transform.position;

            //transform.RotateAround(target.position, Vector3.up, mouseMovement.x * MouseSensitive);
            {
                Vector3 worldPos = transform.position;
                Quaternion q = Quaternion.AngleAxis(angle, axis);
                Vector3 dif = worldPos - point;
                dif = q * dif;
                _dPosition = point + dif;
                //RotateAroundInternal(axis, angle * Mathf.Deg2Rad);
                {
                    _dRotation = q;//Quaternion.Euler(q * _defaultRotation);
                }
            }



            //Debug.Log($"Position={transform.position} Rotation={transform.rotation.eulerAngles}");
            
            
            //transform.Translate(Vector3.forward * mouseMovement.y * MouseSensitive);

            

            //MyRotateAround(target.position, Vector3.up, mouseMovement.x * MouseSensitive);
            //transform.RotateAround(target.position, Vector3.up, mouseMovement.x * MouseSensitive);
            //transform.RotateAround(target.position, Vector3.left, mouseMovement.y * MouseSensitive);
        }
    }

    public static Vector3 Rotate(Vector3 aVec, Vector3 aAngles)
    {
        aAngles *= Mathf.Deg2Rad;
        float sx = Mathf.Sin(aAngles.x);
        float cx = Mathf.Cos(aAngles.x);
        float sy = Mathf.Sin(aAngles.y);
        float cy = Mathf.Cos(aAngles.y);
        float sz = Mathf.Sin(aAngles.z);
        float cz = Mathf.Cos(aAngles.z);
        aVec = new Vector3(aVec.x * cz - aVec.y * sz, aVec.x * sz + aVec.y * cz, aVec.z);
        aVec = new Vector3(aVec.x, aVec.y * cx - aVec.z * sx, aVec.y * sx + aVec.z * cx);
        aVec = new Vector3(aVec.x * cy + aVec.z * sy, aVec.y, -aVec.x * sy + aVec.z * cy);
        return aVec;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnsurePlayerObject();
        var target = _playerObject.transform;
        transform.position = _dPosition;
        transform.rotation = _dRotation;
    }

    void MyRotateAround(Vector3 pos, Vector3 up, float degree)
    {
        Quaternion q = Quaternion.AngleAxis(degree, up);
        this.transform.position = q * (this.transform.position - pos) + pos;
        this.transform.rotation *= q;
    }

    private void EnsurePlayerObject()
    {
        if (_playerObject == null)
        {
            _playerObject = GameObject.FindGameObjectWithTag("Player");
            if (_playerObject == null)
            {
                throw new NullReferenceException();
            }
            _playerObject.transform.position = _defaultPosition;
        }
    }*/
}
