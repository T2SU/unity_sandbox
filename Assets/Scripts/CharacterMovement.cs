using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    const float GravityPower = 9.8f;
    const float StoppingDistance = 0.2f;

    private Vector3 velocity;
    private CharacterController characterController;
    private Vector3 _destination;
    private Vector3? forceRotate;

    public bool arrived;
    public Vector3 destination
    {
        set
        {
            _destination = value;
            arrived = false;
        }
        get => _destination;
    }

    public Vector3 direction
    {
        set
        {
            var d = value;
            d.y = 0;
            // 방향만 들어가도록 벡터 정규화
            d.Normalize();
            forceRotate = d;
        }
        get => forceRotate.GetValueOrDefault();
    }

    public float walkSpeed = 6.0f;
    public float rotationSpeed = 360.0f;
    public float velocityLerp = 5.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        destination = transform.position;
    }

    void Update()
    {
        var pos = transform.position;

        // 땅에 닿아 있어야 이동 속도 및 방향 전환 갱신
        if (characterController.isGrounded)
        {
            // 수평면 = XZ
            var destXZ = destination;
            destXZ.y = pos.y;
            float distance = Vector3.Distance(pos, destXZ);

            // 이동 목표와 거리가 일정 미만일 경우 도착으로 간주
            if (distance < StoppingDistance)
                arrived = true;

            // 방향 벡터
            var direction = (destXZ - pos).normalized;

            Vector3 newVelocity;
            if (arrived)
                newVelocity = Vector3.zero;
            else
                newVelocity = direction * walkSpeed;

            // 부드럽게 보간 처리
            velocity = Vector3.Lerp(velocity, newVelocity, Mathf.Min(Time.deltaTime * velocityLerp, 1.0f));

            if (!forceRotate.HasValue)
            {
                if (velocity.magnitude > 0.1f && !arrived)
                {
                    RotateCharacter(direction);
                }
            }
            else
            {
                RotateCharacter(forceRotate.Value);
            }
        }

        // 중력
        velocity += Vector3.down * GravityPower * Time.deltaTime;

        // 땅 누르기
        var snapGround = Vector3.zero;
        if (characterController.isGrounded)
            snapGround = Vector3.down;

        // 이동
        characterController.Move(velocity * Time.deltaTime + snapGround);

        if (characterController.velocity.magnitude < 0.1f)
            arrived = true;

        // 강제 방향 변경 해제
        if (forceRotate.HasValue)
        {
            if (Vector3.Dot(transform.forward, forceRotate.Value) > 0.99f)
                forceRotate = null;
        }
    }

    private void RotateCharacter(Vector3 forward)
    {
        var targetRotation = Quaternion.LookRotation(forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //transform.rotation = targetRotation;
    }

    public void StopMove()
    {
        // 현재 지점을 목적지로 설정
        destination = transform.position;
    }
}
