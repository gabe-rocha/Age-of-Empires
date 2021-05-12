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
    private Vector3 movementDirection, playerVelocity;
    private Camera mainCamera => Camera.main;

    public PlayerStateRun(Player player, Animator anim, float maxSpeed, float rotationSpeed) {
        this.player = player;
        this.anim = anim;
        this.maxSpeed = maxSpeed;
        this.rotationSpeed = rotationSpeed;
    }

    public void Tick() {
        Move();
        Fall();
        Rotate();
    }

    private void Move() {
        //moveTargetPosition = player.transform.position;
        
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.z = Input.GetAxisRaw("Vertical");
        
        //Align with camera forward
        movementDirection = mainCamera.transform.forward * movementDirection.z + mainCamera.transform.right * movementDirection.x;
        movementDirection.Normalize();

        movementDirection.y = 0;
        playerVelocity = movementDirection * maxSpeed * Time.deltaTime;
        player.transform.position += playerVelocity;
    }
    private void Fall() {
        if (!player.isGrounded) {
            //Apply gravity
            playerVelocity.x = 0;
            playerVelocity.y = GameData.gravityForce * player.fallSpeed * Time.deltaTime;
            playerVelocity.z = 0;
            player.transform.position += playerVelocity;
        }
    }

public void Rotate() {
        player.transform.forward = playerVelocity;
    }

    public void OnEnter() {
        anim.SetBool(runBool, true);
    }

    public void OnExit() {
        anim.SetBool(runBool, false);
    }
}