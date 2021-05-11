using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TopDown3DCharacterOn2DWorldControllerVector : MonoBehaviour {

    [Header("NOTE: Model must be facing RIGHT")]
    [SerializeField] bool handleWalk = false;
    [SerializeField] float runSpeedStartsAt = 2f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float rotationSpeed = 360f;
    [SerializeField] private float movementDeadzone = 0.1f;

    private float horizontal, vertical;
    private bool isWalking, isRunning;
    private bool attack01, attack02, jump, roll, interact;
    private Vector3 heading = new Vector3();
    private Vector3 targetPosition = new Vector3();

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        attack01 = Input.GetButtonDown("Fire1");
        jump = Input.GetButtonDown("Jump");

        if (Mathf.Abs(horizontal) > movementDeadzone ||
            Mathf.Abs(vertical) > movementDeadzone) {
            Move();
            UpdateRotationY();
            //UpdateRotation8Directions();
        }
        else {
            isWalking = false;
            isRunning = false;
        }

        if (jump) {
            Jump();
        }

        if (attack01) {
            Attack01();
        }
    }

    private void Move() {
        targetPosition = transform.position;
        targetPosition.x += horizontal;
        targetPosition.y += vertical;
        transform.position = Vector3.Lerp(transform.position, targetPosition, maxSpeed * Time.deltaTime); //we use FixedUpdate just when using RigidBody
        
        if (handleWalk) {
            var highestSpeedHV = Mathf.Abs(horizontal) >= Mathf.Abs(vertical) ? Mathf.Abs(horizontal) : Mathf.Abs(vertical);
            isRunning = maxSpeed * highestSpeedHV >= runSpeedStartsAt;
        }
        else {
            isRunning = true;
        }
        isWalking = !isRunning;
    }

    private void UpdateRotationY() {
        float angle = Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -angle, 0));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void UpdateRotation8Directions() {
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical)) {
            //moving horizontally
            if (horizontal > 0) {
                //heading right -> Y = 90
                heading = transform.localEulerAngles;
                heading.y = 90f;
            }
            else if (horizontal < 0) {
                //heading left -> Y = -90 //not 270, because of the way the character is going to rotate
                heading = transform.localEulerAngles;
                heading.y = 270;
            }
        }
        else if (Mathf.Abs(vertical) > Mathf.Abs(horizontal)) {
            //moving vertically
            if (vertical > 0) {
                //heading up -> Y = 0
                heading = transform.localEulerAngles;
                heading.y = 0f;
            }
            else if (vertical < 0) {
                //heading down -> Y = 180
                heading = transform.localEulerAngles;
                heading.y = 180;
            }
        }
        else if (Mathf.Abs(horizontal) == Mathf.Abs(vertical)) {
            //moving diagonally
            if (Mathf.Sign(horizontal) == 1 && Mathf.Sign(vertical) == 1) {
                //right/up => Y = 45
                heading = transform.localEulerAngles;
                heading.y = 45;
            }
            else if (Mathf.Sign(horizontal) == 1 && Mathf.Sign(vertical) == -1) {
                //right/down => Y = 135
                heading = transform.localEulerAngles;
                heading.y = 135;
            }
            else if (Mathf.Sign(horizontal) == -1 && Mathf.Sign(vertical) == 1) {
                //left/up => Y = 315
                heading = transform.localEulerAngles;
                heading.y = 315;
            }
            else if (Mathf.Sign(horizontal) == -1 && Mathf.Sign(vertical) == -1) {
                //left/down => Y = 225
                heading = transform.localEulerAngles;
                heading.y = 225;
            }
        }
        else {
            //being handled at Update();
        }
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, heading, rotationSpeed * Time.deltaTime);
    }

    private void Jump() {
        EventManager.PlayerEvents.PlayerPressedJump();
    }

    private void Attack01() {
        EventManager.PlayerEvents.PlayerPressedAttack01();
    }

    public bool GetIsWalking() {
        return isWalking;
    }
    public bool GetIsRunning() {
        return isRunning;
    }

}
