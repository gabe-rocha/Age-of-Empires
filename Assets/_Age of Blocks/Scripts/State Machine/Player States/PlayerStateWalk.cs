using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWalk : IState
{
    private Player player;
    private readonly Animator anim;
    private readonly float maxSpeed;
    private readonly int walkBool = Animator.StringToHash("Walk");
    
    public PlayerStateWalk(Player player, Animator anim, float maxSpeed) {
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
        anim.SetBool(walkBool, true);
    }

    public void OnExit() {
        anim.SetBool(walkBool, false);
    }
}