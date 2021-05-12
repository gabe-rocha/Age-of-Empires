using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttack02 : IState{
    
    private Player player;
    private readonly Animator anim;
    private readonly int attack02Trg = Animator.StringToHash("Melee Right Attack 02");
    private float animStartTime, animDuration;
    private float oldPlayerMaxSpeed;

    public PlayerStateAttack02(Player player, Animator anim) {
        this.player = player;
        this.anim = anim;
    }

    public void Tick() {
    }

    public void OnEnter() {
        anim.SetTrigger(attack02Trg);
        animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
        animStartTime = Time.time;
    }
    public void OnExit() {
    }
}