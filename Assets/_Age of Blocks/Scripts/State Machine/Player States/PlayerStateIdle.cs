using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : IState {

    private Player player;
    private Vector3 playerVelocity;

    public PlayerStateIdle(Player player) {
        this.player = player;
    }

    public void Tick() {
        Fall();
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

    public void OnEnter() {
        //No need to do anything since Idle is our default state
    }

    public void OnExit() {
    }
}