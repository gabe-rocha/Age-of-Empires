using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttack02 : IState{

    private readonly Animator anim;
    private readonly int attack02Trg = Animator.StringToHash("Melee Right Attack 02");

    public PlayerStateAttack02(Animator anim) {
        this.anim = anim;
    }

    public void Tick() {

    }

    public void OnEnter() {
        anim.SetTrigger(attack02Trg);
    }

    public void OnExit() {
    }
}