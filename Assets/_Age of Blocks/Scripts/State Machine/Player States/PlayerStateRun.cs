using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRun : IState
{
    private Player player;
    private readonly Animator anim;
    private readonly float maxSpeed;
    private readonly float rotationSpeed;
    private readonly int runBool = Animator.StringToHash("Run");
    private Vector3 moveTargetPosition;

    public PlayerStateRun(Player player, Animator anim, float maxSpeed, float rotationSpeed) {
        this.player = player;
        this.anim = anim;
        this.maxSpeed = maxSpeed;
        this.rotationSpeed = rotationSpeed;
    }

    public void Tick() {
        Move();
        Rotate();
    }

    private void Move() {
        moveTargetPosition = player.transform.position;
        moveTargetPosition.x += Input.GetAxisRaw("Horizontal");
        moveTargetPosition.y += Input.GetAxisRaw("Vertical");
        player.transform.position = Vector3.Lerp(player.transform.position, moveTargetPosition, maxSpeed * Time.deltaTime); //we use FixedUpdate just when using RigidBody
    }

    public void Rotate() {
        float angle = Mathf.Atan2(moveTargetPosition.y - player.transform.position.y, moveTargetPosition.x - player.transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -angle, 0));
        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OnEnter() {
        anim.SetBool(runBool, true);
    }

    public void OnExit() {
        anim.SetBool(runBool, false);
    }
}