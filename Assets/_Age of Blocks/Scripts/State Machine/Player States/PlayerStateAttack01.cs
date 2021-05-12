using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStateAttack01 : IState{


    public bool isActive = false;

    private Player player;
    private readonly Animator anim;
    private static readonly string animName = "Melee Right Attack 01";
    private readonly int attack01Trg = Animator.StringToHash("Attack01");
    private float animStartTime, animDuration;
    private float oldPlayerMaxSpeed;

    public PlayerStateAttack01(Player player, Animator anim) {
        this.player = player;
        this.anim = anim;

        foreach (var clip in anim.runtimeAnimatorController.animationClips) {
            if (clip.name == animName) {
                animDuration = clip.length;
                break;
            }
        }
    }

    public void Tick() {
        if (Time.time >= animStartTime + animDuration) {
            isActive = false;
        }
    }

    public void OnEnter() {
        anim.SetTrigger(attack01Trg);
        animStartTime = Time.time;

        //oldPlayerMaxSpeed = player.maxSpeed;
        //player.maxSpeed *= 0.5f;
        isActive = true;
    }
    public void OnExit() {
        //player.maxSpeed = oldPlayerMaxSpeed;
        isActive = false;
    }
}