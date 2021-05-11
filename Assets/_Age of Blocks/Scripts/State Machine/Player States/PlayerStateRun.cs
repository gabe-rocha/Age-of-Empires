using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRun : IState
{
    private readonly Animator anim;
    private readonly int runBool = Animator.StringToHash("Run");
    private Player player;
    private readonly float maxSpeed;

    public PlayerStateRun(Player player, Animator anim, float maxSpeed) {
        this.player = player;
        this.anim = anim;
        this.maxSpeed = maxSpeed;
    }

    public void Tick() {
        var targetPosition = player.transform.position;
        targetPosition.x += Input.GetAxisRaw("Horizontal");
        targetPosition.y += Input.GetAxisRaw("Vertical");
        player.transform.position = Vector3.Lerp(player.transform.position, targetPosition, maxSpeed * Time.deltaTime); //we use FixedUpdate just when using RigidBody
    }

    public void OnEnter() {
        anim.SetBool(runBool, true);
    }

    public void OnExit() {
        anim.SetBool(runBool, false);
    }
}